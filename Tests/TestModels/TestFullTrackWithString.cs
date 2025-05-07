using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JukeBoxd.Models;
using SpotifyAPI.Web;

namespace Tests.TestModels
{
    [TestClass]
    public class TestFullTrackWithString 
    {
        [TestMethod]
        public void ToString_ReturnsCorrectFormat_WithSingleArtist() 
        {
            // Arrange
            var track = new FullTrackWithString
            {
                Name = "Test Track",
                Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist { Name = "Artist1" }
                    }
            };

            // Act
            var result = track.ToString();

            // Assert
            Assert.AreEqual("Test Track by Artist1", result);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormat_WithMultipleArtists() 
        {
            // Arrange
            var track = new FullTrackWithString
            {
                Name = "Test Track",
                Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist { Name = "Artist1" },
                        new SimpleArtist { Name = "Artist2" }
                    }
            };

            // Act
            var result = track.ToString();

            // Assert
            Assert.AreEqual("Test Track by Artist1, Artist2", result);
        }

        [TestMethod]
        public void ToString_ReturnsEmptyString_WhenNameIsNull() 
        {
            // Arrange
            var track = new FullTrackWithString
            {
                Name = string.Empty, 
                Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist { Name = "Artist1" }
                    }
            };

            // Act
            var result = track.ToString();

            // Assert
            Assert.AreEqual(" by Artist1", result);
        }

        [TestMethod]
        public void ToString_ReturnsEmptyString_WhenArtistsListIsEmpty() 
        {
            // Arrange
            var track = new FullTrackWithString
            {
                Name = "Test Track",
                Artists = new List<SimpleArtist>()
            };

            // Act
            var result = track.ToString();

            // Assert
            Assert.AreEqual("Test Track by ", result);
        }
    }
}
