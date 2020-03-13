using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public static PonyWriteEntity Parse(string text)
        {
            var bsonDocument = new BsonDocument {["_id"] = ObjectId.NewObjectId()};

            var json = JObject.Parse(text);
            foreach (var (key, value) in json)
            {
                switch (value.Type)
                {
                    case JTokenType.String:
                        bsonDocument[key] = new BsonValue(value.Value<string>());
                        break;
                    case JTokenType.Boolean:
                        bsonDocument[key] = new BsonValue(value.Value<bool>());
                        break;
                    case JTokenType.Integer:
                        bsonDocument[key] = new BsonValue(value.Value<long>());
                        break;
                    case JTokenType.Float:
                        bsonDocument[key] = new BsonValue(value.Value<double>());
                        break;
                }
            }

            return new PonyWriteEntity(bsonDocument);
        }

        public void Save(ILiteCollection<BsonDocument> collection)
        {
            collection.Insert(_bsonDocument);
        }

        public BsonValue this[string key] => _bsonDocument[key];
    }
}