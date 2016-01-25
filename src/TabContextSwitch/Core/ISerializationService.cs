#region using

using System.Collections.Generic;

#endregion

namespace TabContextSwitch.Core
{
    public interface ISerializationService
    {
        string Serialize(IEnumerable<VsDocument> documents);

        IEnumerable<VsDocument> Deserialize(string textFileContent);
    }
}