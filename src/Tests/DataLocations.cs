using System;
using System.IO;

public static class DataLocations
{
    static DataLocations()
    {
        var currentDirectory = Environment.CurrentDirectory;
        SlnPath = Path.GetFullPath(Path.Combine(currentDirectory, "../../../../"));
        TempPath = Path.GetFullPath(Path.Combine(SlnPath, "temp"));
        Directory.CreateDirectory(TempPath);
        DataPath = Path.GetFullPath(Path.Combine(SlnPath, "Data"));
        CountryDataProjectPath = Path.GetFullPath(Path.Combine(SlnPath, "CountryData"));
        BogusProjectPath = Path.GetFullPath(Path.Combine(SlnPath, "CountryData.Bogus"));
        PostCodesPath = Path.GetFullPath(Path.Combine(DataPath, "PostCodes"));
    }

    public static string CountryDataProjectPath;

    public static string BogusProjectPath;

    public static string SlnPath;

    public static string PostCodesPath;

    public static string DataPath;

    public static string TempPath;
}