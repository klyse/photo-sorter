using JetBrains.Annotations;
using photo_sorter.Helpers;
using Serilog;

namespace photo_sorter.Verbs;

public class DeleteVerb
{
    [UsedImplicitly]
    public void DeleteDifferentExtensions(string folderPath)
    {
        Log.Information("Sorting {FolderPath}", folderPath);

        var directoryInfo = new DirectoryInfo(folderPath);

        var allFiles = directoryInfo.GetFiles();

        Log.Information("Found {Count} files", allFiles.Length);

        var allExtensions = allFiles.Select(c => c.Extension).Distinct().ToList();

        Log.Information("Found {Count} extensions: {Extensions}", allExtensions.Count, allExtensions);

        Log.Information("Deleting all files with the same name but different extension");

        using var fileSystemWatcher = new FileSystemWatcher(folderPath);
        fileSystemWatcher.EnableRaisingEvents = true;
        fileSystemWatcher.IncludeSubdirectories = true;
        fileSystemWatcher.Deleted += (_, args) =>
        {
            Log.Information("File Deleted {FileName}", args.FullPath);
            foreach (var f in GetOtherNames.FromFileName(folderPath, args.FullPath))
            {
                Log.Information("Deleting {FileName}", f);
                File.Delete(f);
            }
        };
        Console.ReadLine();
    }
}