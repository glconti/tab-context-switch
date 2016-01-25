#region using

using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.Git.Controls.Extensibility;

#endregion

namespace TabContextSwitch.Core.Impl
{
    internal class VsGitSourceControlService : ISourceControlService
    {
        private readonly ITeamExplorer _teamExplorer;

        public VsGitSourceControlService(ITeamExplorer teamExplorer)
        {
            _teamExplorer = teamExplorer;
        }

        public string GetBranchName(string _)
            => (_teamExplorer.CurrentPage.GetExtensibilityService(typeof(IBranchesExt)) as IBranchesExt)?.CurrentBranch;
    }
}