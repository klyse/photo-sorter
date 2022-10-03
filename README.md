# photo-sorter

This is a small helper to filter through my vacation pictures. My camera stores pictures in `.jpg` and `.rw2`. Since I'm manually sorting the pictures out I want to delete all versions (`.jpg` or `.rw2`) of the corresponding picture.

## Requirements

dotnet 7

Tested on macOS Monterey

## Usage

Run the program and point it to a working folder:

```
dotnet run --folder-path "/Users/klaus/Pictures/Test Pics"
```

Open the pictures in any app you like and start deleting the bad ones. All pictures with the same name but different file extension will be deleted. When you are done just stop the program with `Enter` or `CTRL-C`

### Output:

```
[07:59:19 INF] Sorting /Users/klaus/Pictures/Test Pics
[07:59:19 INF] Found 17 files
[07:59:19 INF] Found 4 extensions: [".DS_Store", ".RW2", ".JPG", ".MP4"]
[07:59:19 INF] Deleting all files with the same name but different extension
[08:00:40 INF] File Deleted /Users/klaus/Pictures/Test Pics/P1030106.JPG
[08:00:40 INF] Deleting /Users/klaus/Pictures/Test Pics/P1030106.RW2
[08:00:40 INF] File Deleted /Users/klaus/Pictures/Test Pics/P1030106.RW2
```