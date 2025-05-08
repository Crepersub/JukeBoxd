using JukeBoxd;
using JukeBoxd.Forms;
using JukeBoxd.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JukeBoxd.BusinessLayer;
using Tests.TestBusinessLayer;
using System.Text.Json;
using JukeBoxd.Forms;

namespace Tests
{
    [TestClass]
    public class TestAdd
    {
        private TestDBContext _inMemoryDbContext;
        private Add _addForm;
        private User _currentUser;
        private SpotifyClient _spotify;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the in-memory DbContext
            _inMemoryDbContext = new();
            _inMemoryDbContext.Database.EnsureCreated();

            // Seed sample data
            _currentUser = new User("TestUser");
            _inMemoryDbContext.Users.Add(_currentUser);
            _inMemoryDbContext.SaveChanges();

            var filePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/JukeBoxd" + "/ClientSecrets.json";
            // Arrange  
            var clientSecrets = JsonSerializer.Deserialize<ClientSecret>(File.ReadAllText(filePath));
            var config = SpotifyClientConfig.CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(clientSecrets.ClientID, clientSecrets.clientSecret));

            // Initialize the Add form
            _spotify = new SpotifyClient(config);
            _addForm = new Add(_currentUser,_spotify,_inMemoryDbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clear the in-memory database after each test
            _inMemoryDbContext.Users.RemoveRange(_inMemoryDbContext.Users);
            _inMemoryDbContext.Database.EnsureDeleted();
            _inMemoryDbContext.Dispose();
        }

        [TestMethod]
        public void HighlightStars_UpdatesStarImagesCorrectly()
        {
            // Arrange
            int count = 5; // Represents 2.5 stars since each PictureBox is half a star.

            // Act
            _addForm.HighlightStars(count);

            // Assert
            Assert.AreEqual(_addForm.stars.Length, 10, "The stars array should contain 10 elements (5 full stars represented as 10 half-stars).");
            for (int i = 0; i < _addForm.stars.Length; i++)
            {
                if (i < count)
                {
                    Assert.IsNotNull(_addForm.stars[i].Image, $"Star at index {i} should have an image.");
                    Assert.IsTrue(
                        _addForm.stars[i].Image == JukeBoxd.Properties.Resources.newFStar1 ||
                        _addForm.stars[i].Image == JukeBoxd.Properties.Resources.newFStar2,
                        $"Star at index {i} should be filled."
                    );
                }
                else
                {
                    Assert.IsNotNull(_addForm.stars[i].Image, $"Star at index {i} should have an image.");
                    Assert.IsTrue(
                        _addForm.stars[i].Image == JukeBoxd.Properties.Resources.newEStar1 ||
                        _addForm.stars[i].Image == JukeBoxd.Properties.Resources.newEStar2,
                        $"Star at index {i} should be empty."
                    );
                }
            }
        }

        [TestMethod]
        public void AddButton_Click_SavesEntryAndClosesForm()
        {
            // Arrange
            var track = new FullTrack { Name = "Test Song", Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Test Artist" } } };
            _addForm.SongComboBox.Items.Add(track);
            _addForm.SongComboBox.SelectedItem = track;
            _addForm.EntryDateTimePicker.Value = DateTime.Now;
            _addForm.ReviewTextBox.Text = "Great song!";
            _addForm.rating = 4.5f;

            bool songAddedEventTriggered = false;
            _addForm.SongAdded += (s, e) => songAddedEventTriggered = true;

            // Act
            _addForm.AddButton1_Click(null, null);

            // Assert
            var savedEntry = _inMemoryDbContext.Entries.FirstOrDefault();
            Assert.IsNotNull(savedEntry);
            Assert.AreEqual(track.Name, savedEntry.Title);
            Assert.AreEqual("Test Artist", savedEntry.Author);
            Assert.AreEqual(4.5f, savedEntry.Rating);
            Assert.AreEqual("Great song!", savedEntry.Review);
            Assert.IsTrue(songAddedEventTriggered);
        }
    }
}
