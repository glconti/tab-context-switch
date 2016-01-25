#region using

using System;
using System.Collections.Generic;

#endregion

namespace TabContextSwitch.Core.Impl
{
    internal class TabManager : ITabManager
    {
        private readonly IStorageService _storageService;

        public TabManager(IEventProvider eventProvider, IStorageService storageService)
        {
            _storageService = storageService;
            eventProvider.SolutionOpened += EventProvider_SolutionOpened;
            eventProvider.SolutionClosed += EventProvider_SolutionClosed;
        }

        private void EventProvider_SolutionClosed(string solutionName, string branchName)
        {
            _storageService.Save(solutionName, branchName, GetOpenedDocuments());
        }

        private void EventProvider_SolutionOpened(string solutionName, string branchName)
        {
            SetOpenedDocuments(_storageService.Load(solutionName, branchName));
        }

        private void SetOpenedDocuments(IEnumerable<VsDocument> documents)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<VsDocument> GetOpenedDocuments()
        {
            throw new NotImplementedException();
        }
    }
}