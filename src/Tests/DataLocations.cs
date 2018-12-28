using System.IO;

static class DataLocations
{
    static DataLocations()
    {
        RootDir = GitRepoDirectoryFinder.Find();
        TempPath = Path.GetFullPath(Path.Combine(RootDir, "temp"));
        Directory.CreateDirectory(TempPath);
        DataPath = Path.GetFullPath(Path.Combine(RootDir, "Data"));
        CountryDataProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/CountryData"));
        BogusProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/CountryData.Bogus"));
        PostCodesPath = Path.GetFullPath(Path.Combine(DataPath, "PostCodes"));
    }

    public static string CountryDataProjectPath;

    public static string BogusProjectPath;

    public static string RootDir;

    public static string PostCodesPath;

    public static string DataPath;

    public static string TempPath;
}