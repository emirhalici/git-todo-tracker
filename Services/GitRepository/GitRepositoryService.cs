using System.Text.RegularExpressions;
using LibGit2Sharp;

public class GitRepositoryService
{
    private Signature signature = new Signature("Your Name", "your.email@example.com", DateTimeOffset.Now);

    public void CloneOrPullRepository(GitRepository gitRepository)
    {
        var repositoryPath = GetRepositoryPath(gitRepository);
        if (Repository.IsValid(repositoryPath))
        {
            // Repository exists, pull changes
            using var repo = new Repository(repositoryPath);
            if (HasRecentCommits(gitRepository))
            {

                // TODO: Get proper signature information
                Commands.Pull(repo, signature, new PullOptions());
            }
        }
        else
        {
            // Repository doesn't exist, clone it
            Repository.Clone(gitRepository.FullGitLink, repositoryPath);
        }
    }

    public string ReadReadmeFile(GitRepository gitRepository)
    {
        var repositoryPath = GetRepositoryPath(gitRepository);

        if (Repository.IsValid(repositoryPath))
        {
            var readmeFilePath = Path.Combine(repositoryPath, "README.md");

            if (File.Exists(readmeFilePath))
            {
                return File.ReadAllText(readmeFilePath);
            }
            else
            {
                return "README.md not found in the repository.";
            }
        }
        else
        {
            return "Repository does not exist.";
        }
    }

    public bool HasRecentCommits(GitRepository gitRepository)
    {
        var repositoryPath = GetRepositoryPath(gitRepository);
        if (!Repository.IsValid(repositoryPath))
        {
            return false;
        }

        var repo = new Repository(repositoryPath);
        Commands.Fetch(repo, "origin", new string[0], new FetchOptions(), null);

        var localBranch = repo.Head;
        var remoteBranch = repo.Branches[$"origin/{localBranch.FriendlyName}"];

        // Compare the commits to check for any new commits in the remote
        var compareResult = repo.Diff.Compare<Patch>(localBranch.Tip.Tree, remoteBranch.Tip.Tree);
        return compareResult.Any();
    }

    public List<Todo> ScanForTodos(GitRepository gitRepository)
    {
        var repositoryPath = GetRepositoryPath(gitRepository);
        CloneOrPullRepository(gitRepository);

        var todoList = new List<Todo>();
        var files = Directory.GetFiles(repositoryPath, "*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var fileContent = File.ReadAllText(file);
            var todoExpression = @"\/\/\s*TODO:\s*(.*)";
            var todoMatches = Regex.Matches(fileContent, todoExpression);

            foreach (Match match in todoMatches)
            {
                var todoMessage = match.Groups[1].Value.Trim();
                var todoItem = new Todo
                {
                    Message = todoMessage,
                    FileName = Path.GetFileName(file),
                    FilePath = file
                };

                todoList.Add(todoItem);
            }
        }

        return todoList;
    }

    private string GetRepositoryPath(GitRepository gitRepository) => $"./GitRepositories/{gitRepository.Id}";
}
