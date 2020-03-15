using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Newtonsoft.Json.Linq;

namespace pony.Storage
{
    public class PonyStorage : IPonyStorage
    {
        private readonly LiteDatabase _db;

        public PonyStorage(string path)
        {
            _db = new LiteDatabase(path);
        }

        public PonyStorage() : this(@".\database.db")
        {
        }

        public async Task<JObject> StoreAsync(string requestPath, Stream stream)
        {
            var collection = _getCollection(requestPath);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var jsonString = await reader.ReadToEndAsync();
            var bsonDocument = await Task.Run(() => PonyWriteEntity.Parse(jsonString).Save(collection));
            return bsonDocument.ToJson();
        }

        public async Task<bool> DeleteAsync(string requestPath, Stream stream)
        {
            var collection = _getCollection(requestPath);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var jsonString = await reader.ReadToEndAsync();
            return await Task.Run(() => PonyWriteEntity.Parse(jsonString).Delete(collection));
        }

        public async Task<JArray> ReadAsync(string requestPath)
        {
            var collection = _getCollection(requestPath);
            return await Task.Run(() => PonyReadEntity.Parse(requestPath).Read(collection));
        }

        public async Task<bool> UpdateAsync(string requestPath, Stream stream)
        {
            var collection = _getCollection(requestPath);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var jsonString = await reader.ReadToEndAsync();
            return await Task.Run(() => PonyWriteEntity.Parse(jsonString).Update(collection));
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        private ILiteCollection<BsonDocument> _getCollection(string requestPath)
        {
            return _db.GetCollection(_getCollectionName(requestPath));
        }

        private static string _getCollectionName(string requestPath)
        {
            var pathSegments = requestPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (pathSegments.Length == 0)
            {
                throw new PonyIllegalCollectionPath();
            }

            return pathSegments[0];
        }
    }
}