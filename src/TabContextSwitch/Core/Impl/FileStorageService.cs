#region using

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace TabContextSwitch.Core.Impl
{
    internal class FileStorageService : IStorageService
    {
        private readonly ISerializationService _serializationService;

        public FileStorageService(ISerializationService serializationService)
        {
            _serializationService = serializationService;
        }

        private static string DestinationPath => Path.GetTempPath();

        public void Save(string solution, string branch, IEnumerable<VsDocument> documents)
        {
            var workFolder = DestinationPath;

            var newFileName = Path.Combine(workFolder, $"{solution}_{branch}.txt");

            File.WriteAllText(newFileName, _serializationService.Serialize(documents));
        }

        public IEnumerable<VsDocument> Load(string solution, string branch)
        {
            var workFolder = DestinationPath;

            var fileName = Path.Combine(workFolder, $"{solution}_{branch}.txt");

            try
            {
                return File.Exists(fileName)
                           ? _serializationService.Deserialize(File.ReadAllText(fileName))
                           : Enumerable.Empty<VsDocument>();
            }
            catch
            {
                return Enumerable.Empty<VsDocument>();
            }
        }
    }
}