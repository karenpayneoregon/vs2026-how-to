
#nullable disable

namespace HasConversionDictionary.Models;

/// <summary>
/// Represents a dictionary entity with an identifier and associated data.
/// </summary>
/// <remarks>
/// This class is part of the <c>HasConversionDictionary.Models</c> namespace and is used
/// in conjunction with Entity Framework Core to map to a database table.
/// The <see cref="Data"/> property is configured to be stored as JSON in the database.
/// </remarks>
public partial class DictionaryItem
{
    public int Id { get; set; }

    public DataEntity Data { get; set; }
}

/// <summary>
/// Represents a data entity with a key-value pair structure.
/// </summary>
/// <remarks>
/// This class is used as part of the <see cref="DictionaryItem"/> entity in the 
/// <c>HasConversionDictionary.Models</c> namespace. It is configured to be stored 
/// as JSON in the database when used with Entity Framework Core.
/// </remarks>
public class DataEntity
{
    public string Key { get; set; }
    public string Value { get; set; }
}