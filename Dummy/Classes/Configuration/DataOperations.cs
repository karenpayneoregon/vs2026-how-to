using ConsoleConfigurationLibrary.Classes;

namespace Dummy.Classes.Configuration;
/// <summary>
/// Performs data operations.
/// </summary>
internal class DataOperations
{
    
    /// <summary>
    /// Retrieves and displays application settings, such as connection details and entity creation settings.
    /// </summary>
    /// <remarks>
    /// This method accesses singleton instances of <see cref="AppConnections"/> and <see cref="EntitySettings"/> 
    /// to retrieve and output their respective properties.
    /// </remarks>
    public static void GetSettings()
    {
        
        Console.WriteLine(AppConnections.Instance.MainConnection);
        Console.WriteLine(EntitySettings.Instance.CreateNew); 
        
    }
    
}
