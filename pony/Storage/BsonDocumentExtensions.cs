using LiteDB;
using Newtonsoft.Json.Linq;

namespace pony.Storage
{
    public static class BsonDocumentExtensions
    {
        public static void Set(this BsonDocument bsonDocument, string key, JToken value)
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

        public static JObject ToJson(this BsonDocument bsonDocument)
        {
            var json = new JObject();
            foreach (var (key, value) in bsonDocument.GetElements())
            {
                switch (value.Type)
                {
                    case BsonType.String:
                        json[key] = value.AsString;
                        break;
                    case BsonType.Boolean:
                        json[key] = value.AsBoolean;
                        break;
                    case BsonType.Int64:
                        json[key] = value.AsInt64;
                        break;
                    case BsonType.Double:
                        json[key] = value.AsDouble;
                        break;
                    case BsonType.ObjectId:
                        json[key] = value.AsObjectId.ToString();
                        break;
                }
            }

            return json;
        }
    }
}