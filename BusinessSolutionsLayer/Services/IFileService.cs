using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutionsLayer.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(Stream stream);

        Task<IReadOnlyCollection<T>> ParseFileAsync<T>(string path);

        void DeleteFile(string path);
    }
}
