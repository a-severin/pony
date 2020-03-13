using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using pony.Storage;
using Xunit;

namespace pony.Tests.Storage
{
    public class PonyWriteEntityTests
    {
        [Fact]
        public void ParseBoolean()
        {
            string text = "{\"key\":true}";
            PonyWriteEntity.Parse(text);
        }

        [Fact]
        public void ParseString()
        {
            string text = "{\"key\":\"value\"}";
            Assert.Equal(BsonType.String, PonyWriteEntity.Parse(text)["key"].Type);
        }

        [Fact]
        public void ParseInteger()
        {
            string text = "{\"key\":123}";
            PonyWriteEntity.Parse(text);
            Assert.Equal(BsonType.Int64, PonyWriteEntity.Parse(text)["key"].Type);
        }

        [Fact]
        public void ParseFloat()
        {
            string text = "{\"key\":123.456}";
            PonyWriteEntity.Parse(text);
            Assert.Equal(BsonType.Double, PonyWriteEntity.Parse(text)["key"].Type);
        }
    }
}