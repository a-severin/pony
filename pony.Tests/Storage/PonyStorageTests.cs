using System.IO;
using System.Text;
using System.Threading.Tasks;
using pony.Storage;
using Xunit;

namespace pony.Tests.Storage
{
    public class PonyStorageTests
    {
        [Fact]
        public async Task StoreData()
        {
            var storage = new PonyStorage(":memory:");
            var buffer = Encoding.UTF8.GetBytes("{\"test_key\":\"test_value\"}");
            var stream = new MemoryStream(buffer);
            var jObject = await storage.StoreAsync("/test", stream);
            var response = jObject.ToString();
            Assert.Contains("_id", response);
            Assert.Contains("test_key", response);
            Assert.Contains("test_value", response);
        }

        [Fact]
        public async Task WriteDeleteData()
        {
            var storage = new PonyStorage(":memory:");
            var text = "{\"test_key\":\"test_value\"}";
            var buffer = Encoding.UTF8.GetBytes(text);
            var stream = new MemoryStream(buffer);
            await storage.StoreAsync("/test", stream);
            var jArray = await storage.ReadAsync("/test");

            var jToken = jArray[0];
            text = jToken.ToString();
            buffer = Encoding.UTF8.GetBytes(text);
            stream = new MemoryStream(buffer);
            var success = await storage.DeleteAsync("/test", stream);
            Assert.True(success);
        }

        [Fact]
        public async Task WriteReadData()
        {
            var storage = new PonyStorage(":memory:");
            var buffer = Encoding.UTF8.GetBytes("{\"test_key\":\"test_value\"}");
            var stream = new MemoryStream(buffer);
            await storage.StoreAsync("/test", stream);
            var jArray = await storage.ReadAsync("/test");
            var response = jArray.ToString();
            Assert.Contains("_id", response);
            Assert.Contains("test_key", response);
            Assert.Contains("test_value", response);
        }

        [Fact]
        public async Task WriteUpdateData()
        {
            var storage = new PonyStorage(":memory:");
            var text = "{\"test_key\":\"test_value\"}";
            var buffer = Encoding.UTF8.GetBytes(text);
            var stream = new MemoryStream(buffer);
            await storage.StoreAsync("/test", stream);
            var jArray = await storage.ReadAsync("/test");

            var jToken = jArray[0];
            jToken["test_key"] = "new_vaule";
            text = jToken.ToString();
            buffer = Encoding.UTF8.GetBytes(text);
            stream = new MemoryStream(buffer);
            var success = await storage.UpdateAsync("/test", stream);
            Assert.True(success);
            jArray = await storage.ReadAsync("/test");
            var response = jArray.ToString();
            Assert.Contains("new_vaule", response);
        }
    }
}