using MarkdownSnippets;
using Xunit;

public class DocoUpdater
{
    [Fact]
    public void Run()
    {
        var root = GitRepoDirectoryFinder.Find();
        GitHubMarkdownProcessor.Run(root);
    }
}