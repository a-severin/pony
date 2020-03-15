using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace pony.Storage
{
    public interface IPonyStorage : IDisposable
    {
        Task<bool> DeleteAsync(string requestPath, Stream stream);
        Task<JArray> ReadAsync(string requestPath);
        Task<JObject> StoreAsync(string requestPath, Stream stream);
        Task<bool> UpdateAsync(string requestPath, Stream stream);
    }
}