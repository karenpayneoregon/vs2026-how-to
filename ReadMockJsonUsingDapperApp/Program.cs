using ConsoleConfigurationLibrary.Classes;
using Microsoft.Data.SqlClient;
using ReadMockJsonUsingDapperApp.Classes;
using ReadMockJsonUsingDapperApp.Classes.Core;
using Spectre.Console;

namespace ReadMockJsonUsingDapperApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        const string json = """
                            {
                                "FirstName": "Karen",
                                "LastName": "Payne",
                                "Age": 69,
                                "Skills": ["SQL Server 2020", "C#", "AI", "CSS", "HTML"]
                            }
                            """;

        await using var connection = new SqlConnection(AppConnections.Instance.MainConnection);

        PersonDto? person = await Operations.ReadPersonFromJsonAsync(connection, json);

        if (person is { })
        {

            SpectreConsoleHelpers.InfoPill(Justify.Left, "Person Information");
            AnsiConsole.WriteLine();

            var personTable = CreatePersonTable();

            personTable.AddRow(person.FirstName, person.LastName, person.Age.ToString());

            AnsiConsole.Write(personTable);
            AnsiConsole.WriteLine();

            SpectreConsoleHelpers.InfoPill(Justify.Left, "Skills");
            AnsiConsole.WriteLine();

            var skillsTable = new Table().Border(TableBorder.Rounded).BorderColor(Color.Blue);

            skillsTable.AddColumn("Skill Index");
            skillsTable.AddColumn("Skill Name");

            foreach (var skill in person.Skills)
            {
                skillsTable.AddRow(skill.SkillIndex.ToString(), skill.SkillName);
            }

            AnsiConsole.Write(skillsTable);
            AnsiConsole.WriteLine();
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static Table CreatePersonTable()
    {
        var personTable = new Table().Border(TableBorder.Rounded).BorderColor(Color.Green);

        personTable.AddColumn("First Name");
        personTable.AddColumn("Last Name");
        personTable.AddColumn("Age");
        return personTable;
    }
}