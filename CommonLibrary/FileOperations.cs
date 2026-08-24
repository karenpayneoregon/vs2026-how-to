using Serilog;

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

            try
            {
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
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while copying file {SourceFile} to {DestinationFile}", sourceFile, destinationFile);
            }
            
        }
    }

    /// <summary>
    /// Sets the file timestamps for a specified destination file to match those of a source file.
    /// </summary>
    /// <param name="sourceFile">
    /// The path of the source file from which the timestamps will be copied. 
    /// This parameter cannot be null, empty, or consist only of white-space characters.
    /// </param>
    /// <param name="destinationFile">
    /// The path of the destination file to which the timestamps will be applied. 
    /// This parameter cannot be null, empty, or consist only of white-space characters.
    /// </param>
    /// <returns>
    /// <c>true</c> if the file timestamps were successfully updated; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="sourceFile"/> or <paramref name="destinationFile"/> 
    /// is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the <paramref name="sourceFile"/> or <paramref name="destinationFile"/> does not exist.
    /// </exception>
    /// <remarks>
    /// This method updates the creation, last write, and last access timestamps of the 
    /// destination file to match those of the source file. Additionally, it copies the 
    /// file attributes from the source file to the destination file.
    /// </remarks>
    public static bool SetFileDateTime(string sourceFile, string destinationFile)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceFile);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationFile);

        if (!File.Exists(sourceFile))
        {
            throw new FileNotFoundException("Source file does not exist.", sourceFile);
        }

        if (!File.Exists(destinationFile))
        {
            throw new FileNotFoundException("Destination file does not exist.", destinationFile);
        }

        var sourceInfo = new FileInfo(sourceFile);

        try
        {
            File.SetCreationTimeUtc(destinationFile, sourceInfo.CreationTimeUtc);
            File.SetLastWriteTimeUtc(destinationFile, sourceInfo.LastWriteTimeUtc);
            File.SetLastAccessTimeUtc(destinationFile, sourceInfo.LastAccessTimeUtc);

            File.SetAttributes(destinationFile, File.GetAttributes(sourceFile));
            
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "An error occurred while setting file timestamps for {DestinationFile}", destinationFile);
            return false;
        }

    }
}