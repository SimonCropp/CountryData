using System.IO;
using CaptureSnippets;
using Xunit;

public class DirectorySourceMarkdownProcessorTests
{
    [Fact]
    public void Run()
    {
        var root = GitRepoDirectoryFinder.Find();

        var files = Directory.EnumerateFiles(Path.Combine(root, "Tests"), "Snippets.cs");
        DirectorySourceMarkdownProcessor.Run(root,files);
    }
}