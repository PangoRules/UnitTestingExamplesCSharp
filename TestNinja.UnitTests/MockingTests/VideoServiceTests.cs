using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.UnitTests.MockingTests.FakeObjects;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    class VideoServiceTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var service = new VideoService();

            var result = service.ReadVideoTitle(new FakeFileReader());

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}
