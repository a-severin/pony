using System;
using Xunit;

namespace pony.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(2 + 2 == 4);
        }

        [Fact]
        public void Test2()
        {
            Assert.True(3 + 3 == 6);
        }

        [Fact]
        public void Test3()
        {
            Assert.True(4 + 4 == 8);
        }
    }
}
