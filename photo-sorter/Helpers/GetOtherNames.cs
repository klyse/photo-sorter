namespace photo_sorter.Helpers;

public class GetOtherNames
{
    public static IEnumerable<string> FromFileName(string folderPath, string fileName)
    {
        var extension = Path.GetFileNameWithoutExtension(fileName);
        foreach (var f in Directory.EnumerateFiles(folderPath, $"{extension}.*")) yield return f;
    }
}