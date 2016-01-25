namespace TabContextSwitch.Core
{
    public interface ISourceControlService
    {
        string GetBranchName(string solutionPath);
    }
}