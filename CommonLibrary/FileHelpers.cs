using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary;

public class FileHelpers
{
    /// <summary>
    /// Determines whether the specified path corresponds to a file or a folder.
    /// </summary>
    /// <param name="path">The path of the item to check.</param>
    /// <returns>
    /// A tuple containing two values:
    /// <list type="bullet">
    /// <item>
    /// <description><c>isFolder</c>: <c>true</c> if the path corresponds to a folder; <c>false</c> if it corresponds to a file.</description>
    /// </item>
    /// <item>
    /// <description><c>success</c>: <c>true</c> if the path exists and was successfully evaluated; <c>false</c> otherwise.</description>
    /// </item>
    /// </list>
    /// </returns>
    public static (bool isFolder, bool success) IsFileOrFolder(string path)
    {
        try
        {
            var attr = File.GetAttributes(path);
            return attr.HasFlag(FileAttributes.Directory) ? (true, true)! : (false, true)!;
        }
        catch (FileNotFoundException)
        {
            return (false, false);
        }
    }
}
