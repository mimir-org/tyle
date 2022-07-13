using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories.Application
{
    public class JsonFileRepository : IFileRepository
    {
        private static string CatalogName = "Data";
        private readonly string _rootPath;

        public JsonFileRepository()
        {
            var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;

            if (string.IsNullOrEmpty(executingAssembly))
                return;

            _rootPath = Path.GetDirectoryName(executingAssembly);

            if (!string.IsNullOrEmpty(_rootPath))
                _rootPath = _rootPath.TrimEnd('/');

            _rootPath = $"{_rootPath}/{CatalogName}";
        }

        public IEnumerable<T> ReadFile<T>(string filename) where T : class, new()
        {
            if (string.IsNullOrEmpty(_rootPath) || string.IsNullOrEmpty(filename))
                return new List<T>();

            var extension = Path.GetExtension(filename);
            var fileNameWithoutExtension = filename.Substring(0, filename.Length - extension.Length);

            var filepath = $"{_rootPath}/{fileNameWithoutExtension}.json";
            using var r = new StreamReader(filepath);
            var json = r.ReadToEnd();
            var items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            return items;
        }

        public IEnumerable<string> ReadJsonFileList()
        {
            var files = Directory.EnumerateFiles(_rootPath, "*.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileName = file.Substring(_rootPath.Length + 1);
                yield return fileName.Split('.').FirstOrDefault();
            }
        }

        public IEnumerable<T> ReadAllFiles<T>(IEnumerable<string> fileNames) where T : class, new()
        {
            return fileNames.Select(ReadFile<T>).SelectMany(data => data);
        }
    }
}