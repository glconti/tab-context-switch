#region using

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace TabContextSwitch.Core.Impl
{
    internal class JsonSerializationService : ISerializationService
    {
        public string Serialize(IEnumerable<VsDocument> documents)
        {
            return JsonConvert.SerializeObject(documents);
        }

        public IEnumerable<VsDocument> Deserialize(string textFileContent)
        {
            return JsonConvert.DeserializeObject<IEnumerable<VsDocument>>(textFileContent);
        }
    }
}