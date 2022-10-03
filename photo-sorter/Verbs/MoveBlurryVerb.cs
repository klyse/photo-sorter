using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using photo_sorter.Helpers;
using Serilog;

namespace photo_sorter.Verbs;

public class MoveBlurryVerb
{
    [UsedImplicitly]
    public void MoveBlurry(string folderPath, string blurryResultFile)
    {
        var blurryPath = Path.Combine(folderPath, "blurry");
        if (!Directory.Exists(blurryPath)) Directory.CreateDirectory(blurryPath);
        Log.Information("Move blurry pictures from {FolderPath} to {BlurryPath}", folderPath, blurryPath);

        using var fileStream = new StreamReader(blurryResultFile);
        var blurryResults = JsonSerializer.Deserialize<BlurryResults>(fileStream.BaseStream);

        if (blurryResults is null || blurryResults.Results.Count <= 0)
        {
            Log.Warning("No blurry images found");
            return;
        }

        foreach (var blurryResultsResult in blurryResults.Results.Where(c => c.Blurry))
        {
            Log.Information("Photo {FileName} is blurry", blurryResultsResult.InputPath);

            foreach (var file in GetOtherNames.FromFileName(folderPath, blurryResultsResult.InputPath))
            {
                var fileName = Path.GetFileName(file);
                File.Move(file, Path.Combine(blurryPath, fileName));
            }
        }

        Log.Information("Done");
    }

    [UsedImplicitly]
    private class Picture
    {
        [JsonPropertyName("input_path")] public string InputPath { get; set; }

        [JsonPropertyName("score")] public double Score { get; set; }

        [JsonPropertyName("blurry")] public bool Blurry { get; set; }
    }

    [UsedImplicitly]
    private class BlurryResults
    {
        [JsonPropertyName("images")] public List<string> Images { get; set; } = null!;

        [JsonPropertyName("threshold")] public double Threshold { get; set; }

        [JsonPropertyName("fix_size")] public bool FixSize { get; set; }

        [JsonPropertyName("results")] public List<Picture> Results { get; } = null!;
    }
}