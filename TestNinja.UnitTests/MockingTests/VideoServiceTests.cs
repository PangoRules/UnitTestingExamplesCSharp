using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.Mocking.Repositories;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void SetUp()
        {
            _mockFileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_mockFileReader.Object, _videoRepository.Object);
        }

        #region[ReadVideoTitle method test]
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();
            
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
        #endregion

        #region[GetUnprocessedVideosAsCsv method tests]
        [Test]
        public void GetUnprocessedVideosAsCsv_NoUnprocessedVideos_EmptyString()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideos_StringWithIdsOfUnprocessedVideos()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video> { 
                new Video(){Id = 1, IsProcessed = false, Title = "Video 1"},
                new Video(){Id = 2, IsProcessed = false, Title = "Video 2"},
                new Video(){Id = 3, IsProcessed = false, Title = "Video 3"},
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
        #endregion
    }
}
