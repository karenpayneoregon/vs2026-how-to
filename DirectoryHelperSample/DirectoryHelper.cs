
namespace DirectoryHelperSample;

/// <summary>
/// Provides utility methods for working with directories, such as retrieving
/// project and solution folder paths or project file names.
/// </summary>
/// <remarks>
/// This static class includes methods to traverse directory structures and locate
/// specific files or folders related to the current application's base directory.
/// </remarks>
public static class DirectoryHelper
{
    extension(string? folderName)
    {
        /// <summary>
        /// Retrieves the folder path at a specified level above the given folder in the directory hierarchy.
        /// </summary>
        /// <param name="level">
        /// The number of levels to traverse up the directory structure. 
        /// Must be greater than 0.
        /// </param>
        /// <returns>
        /// The full path to the folder at the specified level as a string, or <c>null</c> 
        /// if the folder path cannot be determined or the level is invalid.
        /// </returns>
        /// <remarks>
        /// This method iteratively traverses the directory hierarchy starting from the given folder name 
        /// and collects the paths of parent folders. It then returns the folder path at the specified level 
        /// if it exists; otherwise, it returns the original folder name or <c>null</c>.
        /// </remarks>
        public string? UpperFolder(int level)
        {
            var folderList = new List<string>();

            while (!string.IsNullOrWhiteSpace(folderName))
            {
                var parentFolder = Directory.GetParent(folderName);
                if (parentFolder == null)
                {
                    break;
                }

                folderName = Directory.GetParent(folderName)?.FullName;
                if (!string.IsNullOrWhiteSpace(folderName))
                {
                    folderList.Add(folderName);
                }
            }

            return folderList.Count > 0 && level > 0
                ? level - 1 <= folderList.Count - 1 ? folderList[level - 1] : folderName
                : folderName;
        }
    }

    /// <summary>
    /// Retrieves the folder path of the current project by traversing the directory structure
    /// from the application's base directory.
    /// </summary>
    /// <returns>
    /// The full path to the project's folder as a string, or <c>null</c> if the path cannot be determined.
    /// </returns>
    /// <remarks>
    /// This method calculates the project's folder path by moving up the directory structure
    /// from the application's base directory by four levels.
    /// </remarks>
    public static string? ProjectFolder() 
        => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4);

    /// <summary>
    /// Retrieves the name of the project by locating the first C# project file (*.csproj) 
    /// in the project's folder.
    /// </summary>
    /// <returns>
    /// The full path to the first C# project file (*.csproj) as a string, or <c>null</c> 
    /// if no project file is found.
    /// </returns>
    /// <remarks>
    /// This method determines the project's folder by traversing the directory structure 
    /// from the application's base directory and then searches for a C# project file 
    /// (*.csproj) within that folder.
    /// </remarks>
    public static string? ProjectName()
    {
        var projectFolder = ProjectFolder();
        return string.IsNullOrWhiteSpace(projectFolder) ? 
            null :
            Directory.GetFiles(projectFolder, "*.csproj").FirstOrDefault();
    }

    /// <summary>
    /// Retrieves the solution folder path based on the current application's base directory.
    /// </summary>
    /// <returns>
    /// The full path to the solution folder as a string, or <c>null</c> if the path cannot be determined.
    /// </returns>
    /// <remarks>
    /// This method calculates the solution folder path by traversing up the directory structure
    /// from the application's base directory.
    /// </remarks>
    public static string SolutionFolder() 
        => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(5)!;
}