using System.Data;
using Dapper;

namespace ReadMockJsonUsingDapperApp.Classes;

/// <summary>
/// Provides operations for processing JSON data and mapping it to domain-specific objects.
/// </summary>
/// <remarks>
/// This static class contains methods for handling JSON input, querying data using Dapper,
/// and transforming the results into structured objects such as <see cref="PersonDto"/>.
/// It is designed to facilitate seamless integration between JSON data and application logic.
/// </remarks>
public static class Operations
{
    /// <summary>
    /// Reads and parses a JSON string to extract person-related data and maps it to a <see cref="PersonDto"/> object.
    /// </summary>
    /// <param name="connection">
    /// An open database connection implementing <see cref="IDbConnection"/> used for executing the SQL query.
    /// </param>
    /// <param name="json">
    /// A JSON string containing person details, including their first name, last name, age, and skills.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="PersonDto"/> object
    /// populated with the parsed data, or <c>null</c> if no data could be extracted.
    /// </returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that extracts data from the provided JSON string.
    /// The extracted data is then transformed into a <see cref="PersonDto"/> object, which includes
    /// personal details and a list of skills.
    /// </remarks>
    public static async Task<PersonDto?> ReadPersonFromJsonAsync(IDbConnection connection, string json)
    {
        const string sql = """

                           SELECT
                               JSON_VALUE(@json, '$.FirstName') AS FirstName,
                               JSON_VALUE(@json, '$.LastName') AS LastName,
                               CAST(JSON_VALUE(@json, '$.Age') AS INT) AS Age,
                               CAST(s.[key] AS INT) AS SkillIndex,
                               s.[value] AS SkillName
                           FROM OPENJSON(@json, '$.Skills') s;

                           """;

        var rows = (await connection.QueryAsync<PersonSkillRow>(sql, new { json })).ToList();

        if (rows.Count == 0)
        {
            return null;
        }

        var first = rows[0];

        return new PersonDto
        {
            FirstName = first.FirstName,
            LastName = first.LastName,
            Age = first.Age,
            Skills = rows
                .OrderBy(r => r.SkillIndex)
                .Select(r => new SkillDto
                {
                    SkillIndex = r.SkillIndex,
                    SkillName = r.SkillName
                })
                .ToList()
        };
    }
}