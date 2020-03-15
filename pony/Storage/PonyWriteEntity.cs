using System;
using LiteDB;
using Newtonsoft.Json.Linq;

namespace pony.Storage
{
    public class PonyWriteEntity
    {
        private readonly BsonDocument _bsonDocument;

        private PonyWriteEntity(BsonDocument bsonDocument)
        {
            _bsonDocument = bsonDocument;
        }

        public BsonValue this[string key] => _bsonDocument[key];

        public static PonyWriteEntity Parse(string text)
        {
            var bsonDocument = new BsonDocument {["_id"] = ObjectId.NewObjectId()};

            var json = JObject.Parse(text);
            foreach (var (key, value) in json)
            {
                if (key.Equals("_id", StringComparison.InvariantCultureIgnoreCase))
                {
                    bsonDocument["_id"] = new ObjectId(value.Value<string>());
                    continue;
                }

                bsonDocument.Set(key, value);
            }

            return new PonyWriteEntity(bsonDocument);
        }

        public BsonDocument Save(ILiteCollection<BsonDocument> collection)
        {
            var id = collection.Insert(_bsonDocument);
            _bsonDocument["_id"] = id;
            return _bsonDocument;
        }

        public bool Delete(ILiteCollection<BsonDocument> collection)
        {
            return collection.Delete(_bsonDocument["_id"]);
        }

        public bool Update(ILiteCollection<BsonDocument> collection)
        {
            return collection.Update(_bsonDocument);
        }
    }
}