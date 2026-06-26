using CommonLibrary.Models;
using Microsoft.Extensions.FileSystemGlobbing;

namespace CommonLibrary;

public class GlobbingOperations
{

    /// <summary>
    /// Finds files in the specified directory that match the given include and exclude glob patterns.
    /// </summary>
    /// <param name="path">The directory path to search for matching files.</param>
    /// <param name="includePatterns">An array of glob patterns to include in the search.</param>
    /// <param name="excludePatterns">
    /// An array of glob patterns to exclude from the search. If <c>null</c>, no files are excluded.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of 
    /// <see cref="CommonLibrary.Models.FileMatchItem"/> objects representing the matched files.
    /// </returns>
    /// <remarks>
    /// This method performs file system globbing using the <see cref="Microsoft.Extensions.FileSystemGlobbing.Matcher"/>.
    /// It allows for flexible file matching based on patterns.
    /// </remarks>
    public static Task<List<FileMatchItem>> FindAsync(string path, string[] includePatterns, string[] excludePatterns)
        => Task.Run(() => GetMatchingItems(path, includePatterns, excludePatterns).ToList());


    /// <summary>
    /// Retrieves a collection of file match items based on the specified path and glob patterns.
    /// </summary>
    /// <param name="path">The root directory to search for matching files.</param>
    /// <param name="includePatterns">An array of glob patterns to include in the matching process.</param>
    /// <param name="excludePatterns">
    /// An optional array of glob patterns to exclude from the matching process. If <c>null</c>, no exclusion patterns are applied.
    /// </param>
    /// <returns>
    /// An enumerable collection of <see cref="FileMatchItem"/> objects representing the files that match the specified patterns.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="Microsoft.Extensions.FileSystemGlobbing.Matcher"/> to perform file system globbing operations.
    /// </remarks>
    private static IEnumerable<FileMatchItem> GetMatchingItems(string path, string[] includePatterns,
        string[]? excludePatterns)
    {
        Matcher matcher = CreateMatcher(includePatterns, excludePatterns);

        foreach (string file in matcher.GetResultsInFullPath(path))
        {
            yield return new FileMatchItem(file);
        }
    }


    /// <summary>
    /// Creates a <see cref="Microsoft.Extensions.FileSystemGlobbing.Matcher"/> instance configured with the specified include and exclude patterns.
    /// </summary>
    /// <param name="includePatterns">An array of glob patterns to include in the matching process.</param>
    /// <param name="excludePatterns">
    /// An optional array of glob patterns to exclude from the matching process. If <c>null</c> or empty, no exclusion patterns are applied.
    /// </param>
    /// <returns>
    /// A <see cref="Microsoft.Extensions.FileSystemGlobbing.Matcher"/> instance configured with the provided patterns.
    /// </returns>
    /// <remarks>
    /// This method is used internally to configure a matcher for file system globbing operations.
    /// </remarks>
    private static Matcher CreateMatcher(string[] includePatterns, string[]? excludePatterns = null)
    {
        Matcher matcher = new();
        matcher.AddIncludePatterns(includePatterns);

        if (excludePatterns is { Length: > 0 })
        {
            matcher.AddExcludePatterns(excludePatterns);
        }

        return matcher;
    }



}