using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutionsLayer.Services
{
    public class FileService : IFileService
    {
        private readonly string directory;

        public FileService(string directory)
        {
            this.directory = directory;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public async Task<IReadOnlyCollection<T>> ParseFileAsync<T>(string path)
        {
            string json;
            using (var streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                json  = await streamReader.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<IReadOnlyCollection<T>>(json);
        }

        public async Task<string> SaveFileAsync(Stream stream)
        {
            var path = Path.Combine(directory, Guid.NewGuid().ToString());
            using(var fileStream = new FileStream(path, FileMode.Create))
            {
               await stream.CopyToAsync(fileStream);
            }
            return path;
        }
    }
}
