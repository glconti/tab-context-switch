#region using

using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;

#endregion

namespace TabContextSwitch.Core.Impl
{
    internal class VsEventProvider : IEventProvider
    {
        // must be kept to not be GCed
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly SolutionEvents _solutionEvents;
        private readonly ISourceControlService _sourceControlService;
        private readonly DTE2 _vsEnvironment;

        public VsEventProvider( ISourceControlService sourceControlService)
        {
            _vsEnvironment = Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SDTE)) as DTE2;
            _sourceControlService = sourceControlService;
            _solutionEvents = _vsEnvironment.Events.SolutionEvents;
            _solutionEvents.AfterClosing += OnSolutionClosing;
            _solutionEvents.Opened += OnSolutionOpened;
        }

        public event Action<string, string> SolutionOpened;
        public event Action<string, string> SolutionClosed;

        private void OnSolutionOpened()
        {
            var solutionName = _vsEnvironment.Solution?.FullName;

            var branchName = _sourceControlService.GetBranchName(solutionName);

            SolutionOpened?.Invoke(solutionName, branchName);
        }

        private void OnSolutionClosing()
        {
            var solutionName = _vsEnvironment.Solution?.FullName;

            SolutionClosed?.Invoke(solutionName, _sourceControlService.GetBranchName(solutionName));
        }
    }
}