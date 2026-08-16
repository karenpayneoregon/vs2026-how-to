using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ExceptionDispatchInfoApp.Classes;

public class FileOperations
{
    public static (string[] lines, ExceptionDispatchInfo exceptionDispatchInfo) ReadAllLines()
    {
        string[] lines = null;
        ExceptionDispatchInfo exceptionDispatchInfo = null;
        try
        {
            lines = File.ReadAllLines("NonExistingFile.txt");
        }
        catch (Exception localException)
        {
            exceptionDispatchInfo = ExceptionDispatchInfo.Capture(localException);
        }
        return (lines, exceptionDispatchInfo);
    }

    
    /// <summary>
    /// Attempts to read all lines from a file and returns the result along with any encountered exception.
    /// </summary>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item>
    /// <description><c>success</c>: A boolean indicating whether the file was successfully read.</description>
    /// </item>
    /// <item>
    /// <description><c>lines</c>: An array of strings representing the lines read from the file. Returns an empty array if the operation fails.</description>
    /// </item>
    /// <item>
    /// <description><c>exception</c>: An <see cref="System.Exception"/> instance representing the exception encountered during the operation, or <c>null</c> if no exception occurred.</description>
    /// </item>
    /// </list>
    /// </returns>
    public static (bool success, string[] lines, Exception exception) ReadFile()
    {
        try
        {
            var lines = File.ReadAllLines("NonExistingFile.txt");
            return (true, lines, null);
        }
        catch (Exception e)
        {
            Log.Error(e, "An error occurred while reading the file.");
            return (false, Array.Empty<string>(), e);
        }
        
    }
}
