#region using

using System.IO;
using LibGit2Sharp;

#endregion

namespace TabContextSwitch.Core.Impl
{
    internal class LibGit2SharpSourceControlService : ISourceControlService
    {
        public string GetBranchName(string solutionPath)
        {
            try
            {
                using (var repo = new Repository(GetGitRepository(Path.GetFullPath(solutionPath))))
                {
                    return repo.Head.CanonicalName;
                }
            }
            catch
            {
                return null;
            }
        }

        private static string GetGitRepository(string path)
        {
            if (!Directory.Exists(path) && !File.Exists(path))
                return null;

            var discoveredPath = Repository.Discover(Path.GetFullPath(path));
            // https://github.com/libgit2/libgit2sharp/issues/818#issuecomment-54760613
            return discoveredPath;
        }
    }
}