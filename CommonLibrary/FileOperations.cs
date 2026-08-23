namespace CommonLibrary;

public static class FileOperations
{
    /// <summary>
    /// Copies all files from the specified source folder to the destination folder.
    /// </summary>
    /// <param name="sourceFolder">
    /// The path of the source folder from which files will be copied. 
    /// This parameter cannot be null, empty, or consist only of white-space characters.
    /// </param>
    /// <param name="destinationFolder">
    /// The path of the destination folder where files will be copied. 
    /// This parameter cannot be null, empty, or consist only of white-space characters.
    /// </param>
    /// <param name="searchPattern">
    /// The search string to match against the names of files in the source folder. 
    /// Defaults to "*.*", which matches all files.
    /// </param>
    /// <param name="overwrite">
    /// A boolean value indicating whether to overwrite existing files in the destination folder. 
    /// Defaults to <c>true</c>.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="sourceFolder"/>, <paramref name="destinationFolder"/>, 
    /// or <paramref name="searchPattern"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown if the <paramref name="sourceFolder"/> does not exist.
    /// </exception>
    /// <remarks>
    /// * This method preserves file attributes and timestamps during the copy process. 
    ///   Subdirectories within the source folder are also processed recursively.
    /// * Recommend adding error handling when calling this method to manage potential
    ///   exceptions that may arise during file operations.
    /// </remarks>
    public static void CopyFolder(string sourceFolder, string destinationFolder, string searchPattern = "*.*", bool overwrite = true)
    {
        
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceFolder);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationFolder);
        ArgumentException.ThrowIfNullOrWhiteSpace(searchPattern);

        if (!Directory.Exists(sourceFolder))
            throw new DirectoryNotFoundException($"Source folder does not exist: {sourceFolder}");

        Directory.CreateDirectory(destinationFolder);

        foreach (var sourceFile in Directory.EnumerateFiles(sourceFolder, searchPattern, SearchOption.AllDirectories))
        {
            var relativePath = Path.GetRelativePath(sourceFolder, sourceFile);
            var destinationFile = Path.Combine(destinationFolder, relativePath);

            var destinationDirectory = Path.GetDirectoryName(destinationFile);

            if (destinationDirectory is not null)
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            File.Copy(sourceFile, destinationFile, overwrite);

            var sourceInfo = new FileInfo(sourceFile);
            File.SetCreationTime(destinationFile, sourceInfo.CreationTime);
            File.SetLastWriteTime(destinationFile, sourceInfo.LastWriteTime);
            File.SetLastAccessTime(destinationFile, sourceInfo.LastAccessTime);
            File.SetAttributes(destinationFile, sourceInfo.Attributes);
            
        }
    }
}