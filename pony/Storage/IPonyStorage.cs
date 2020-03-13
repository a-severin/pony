using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace pony.Storage
{
    public interface IPonyStorage : IDisposable
    {
        Task<JArray> ReadAsync(string requestPath);
        Task StoreAsync(string requestPath, Stream stream);
    }
}