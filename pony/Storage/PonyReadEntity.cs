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
                var jObject = bsonDocument.ToJson();
                jArray.Add(jObject);
            }

            return jArray;
        }
    }
}