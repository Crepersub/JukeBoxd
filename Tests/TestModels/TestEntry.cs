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
    public class TestEntry
    {
        [TestMethod]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var track = new FullTrack
            {
                Name = "Test Song",
                Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Test Artist" } },
                DurationMs = 180000,
                Id = "testSpotifyId"
            };
            int userId = 1;
            float rating = 4.5f;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            string review = "Great song!";

            // Act
            var entry = new Entry(track, userId, rating, date, review);

            // Assert
            Assert.AreEqual(userId, entry.UserId);
            Assert.AreEqual("Test Song", entry.Title);
            Assert.AreEqual("Test Artist", entry.Author);
            Assert.AreEqual(TimeSpan.FromMinutes(3), entry.Length);
            Assert.AreEqual(date, entry.EntryDate);
            Assert.AreEqual("testSpotifyId", entry.SpotifyToken);
            Assert.AreEqual(review, entry.Review);
            Assert.AreEqual(rating, entry.Rating);
        }

        [TestMethod]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaults()
        {
            // Act
            var entry = new Entry();

            // Assert
            Assert.AreEqual(0, entry.Id);
            Assert.AreEqual(0, entry.UserId);
            Assert.IsNull(entry.Title);
            Assert.IsNull(entry.Author);
            Assert.AreEqual(TimeSpan.Zero, entry.Length);
            Assert.AreEqual(default, entry.EntryDate);
            Assert.AreEqual(0f, entry.Rating);
            Assert.AreEqual(string.Empty, entry.Review);
            Assert.AreEqual(string.Empty, entry.SpotifyToken);
            Assert.IsNull(entry.User);
        }

        [TestMethod]
        public void ToString_ShouldReturnTitleAndAuthor()
        {
            // Arrange
            var entry = new Entry
            {
                Title = "Test Song",
                Author = "Test Artist"
            };

            // Act
            var result = entry.ToString();

            // Assert
            Assert.AreEqual("Test Song - Test Artist", result);
        }
    }
}
