/// <summary>
/// https://database.guide/how-to-delete-duplicate-rows-that-have-a-primary-key-in-sql/
/// </summary>
internal class Operations
{
    private IDbConnection _cn;

    public Operations()
    {
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
        _cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Examples;Integrated Security=True;Encrypt=False");
    }
    public List<PersonWithDuplicates> GetAll()
        => _cn.Query<PersonWithDuplicates>(
            """
            SELECT Id, FirstName, LastName, BirthDay FROM Examples.dbo.PersonWithDuplicates;
            """).ToList();

    public void Populate()
    {
        Reset();
        _cn.Execute(
            """
            INSERT INTO PersonWithDuplicates ([FirstName], [LastName], [BirthDay])
            VALUES
            (N'Bill', N'Smith', N'1976-09-01' ),
            (N'Mary', N'Robinson', N'1945-12-12' ),
            (N'Bill', N'Smith', N'1976-09-01' ),
            (N'Bill', N'Smith', N'1976-09-01' ),
            (N'Nancy', N'Jones', N'2000-02-23' ),
            (N'Nancy', N'Johnson', N'2005-08-12' ),
            (N'Nancy', N'Jones', N'2000-02-23' ),
            (N'Karen', N'Payne', N'1956-09-09' ),
            (N'Kim', N'Adams', N'1989-07-12' ),
            (N'Karen', N'Payne', N'1956-09-09' )
            """);
    }
    public void Reset()
    {
        _cn.Execute($"DELETE FROM dbo.{nameof(PersonWithDuplicates)}");
        _cn.Execute($"DBCC CHECKIDENT ({nameof(PersonWithDuplicates)}, RESEED, 0)");
    }

    public void RemoveDuplicates()
    {
        _cn.Execute(
            """
            DELETE FROM dbo.PersonWithDuplicates 
            WHERE Id IN (
                SELECT Id FROM dbo.PersonWithDuplicates 
                EXCEPT SELECT MIN(Id) FROM dbo.PersonWithDuplicates 
                GROUP BY FirstName, LastName
                );
            """);
    }

}
public class PersonWithDuplicates
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthDay { get; set; }
}