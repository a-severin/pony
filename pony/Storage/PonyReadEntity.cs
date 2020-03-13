using LiteDB;
using Newtonsoft.Json.Linq;

namespace pony.Storage
{
    public class PonyReadEntity
    {
        private PonyReadEntity()
        {
        }

        public static PonyReadEntity Parse(string requestPath)
        {
            return new PonyReadEntity();
        }

        public JArray Read(ILiteCollection<BsonDocument> collection)
        {
            var jArray = new JArray();
            foreach (var bsonDocument in collection.FindAll())
            {
                var jObject = new JObject();
                foreach (var (key, value) in bsonDocument.GetElements())
                {
                    switch (value.Type)
                    {
                        case BsonType.String:
                            jObject[key] = value.AsString;
                            break;
                        case BsonType.Boolean:
                            jObject[key] = value.AsBoolean;
                            break;
                        case BsonType.Int64:
                            jObject[key] = value.AsInt64;
                            break;
                        case BsonType.Double:
                            jObject[key] = value.AsDouble;
                            break;
                        case BsonType.ObjectId:
                            jObject[key] = value.AsObjectId.ToString();
                            break;
                    }
                }

                jArray.Add(jObject);
            }

            return jArray;
        }
    }
}