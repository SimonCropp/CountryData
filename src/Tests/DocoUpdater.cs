using System.IO;
using CaptureSnippets;
using Xunit;

public class DocoUpdater
{
    [Fact]
    public void Run()
    {
        var root = GitRepoDirectoryFinder.Find();

        var files = Directory.EnumerateFiles(Path.Combine(root, "src/Tests"), "Snippets.cs");
        DirectorySourceMarkdownProcessor.Run(root,files);
    }
}