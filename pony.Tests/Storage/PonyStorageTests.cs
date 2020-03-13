using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using pony.Storage;
using Xunit;

namespace pony.Tests.Storage
{
    public class PonyStorageTests
    {
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
    }
}