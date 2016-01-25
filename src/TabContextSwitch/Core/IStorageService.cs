#region using

using System.Collections.Generic;

#endregion

namespace TabContextSwitch.Core
{
    public interface IStorageService
    {
        void Save(string solution, string branch, IEnumerable<VsDocument> documents);

        IEnumerable<VsDocument> Load(string solution, string branch);
    }
}