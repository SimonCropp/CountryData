using System.IO;

static class GitRepoDirectoryFinder
{
    public static string Find()
    {
        if (!TryFind(out var rootDirectory))
        {
            throw new("Could not find root git directory");
        }

        return rootDirectory;
    }

    public static bool TryFind(out string path)
    {
        var currentDirectory = AssemblyLocation.CurrentDirectory;
        do
        {
            if (Directory.Exists(Path.Combine(currentDirectory, ".git")))
            {
                path = currentDirectory;
                return true;
            }

            var parent = Directory.GetParent(currentDirectory);
            if (parent == null)
            {
                path = string.Empty;
                return false;
            }

            currentDirectory = parent.FullName;
        } while (true);
    }
}