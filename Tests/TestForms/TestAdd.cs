using System.Text.Json;
using JukeBoxd.Forms;
using JukeBoxd.Models;
using SpotifyAPI.Web;
using Tests.TestBusinessLayer;

namespace Tests.TestForms
{
    [TestClass]
    public class TestAdd
    {
        private TestDBContext? _inMemoryDbContext;
        private Add? _addForm;
        private User? _currentUser;
        private SpotifyClient? _spotify;

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
                .WithAuthenticator(new ClientCredentialsAuthenticator(clientSecrets!.ClientID!, clientSecrets.clientSecret!));

            // Initialize the Add form
            _spotify = new SpotifyClient(config);
            _addForm = new Add(_currentUser, _spotify, _inMemoryDbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clear the in-memory database after each test
            _inMemoryDbContext!.Users.RemoveRange(_inMemoryDbContext.Users);
            _inMemoryDbContext.Database.EnsureDeleted();
            _inMemoryDbContext.Dispose();
        }

        [TestMethod]
        public void AddButton_Click_SavesEntryAndClosesForm()
        {
            // Arrange
            var track = new FullTrack { Name = "Test Song", Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Test Artist" } } };
            _addForm!.SongComboBox.Items.Add(track);
            _addForm.SongComboBox.SelectedItem = track;
            _addForm.EntryDateTimePicker.Value = DateTime.Now;
            _addForm.ReviewTextBox.Text = "Great song!";
            _addForm.rating = 4.5f;

            bool songAddedEventTriggered = false;
            _addForm.SongAdded += (s, e) => songAddedEventTriggered = true;

            // Act
            _addForm.AddButton1_Click(null!, null!);

            // Assert
            var savedEntry = _inMemoryDbContext!.Entries.FirstOrDefault();
            Assert.IsNotNull(savedEntry);
            Assert.AreEqual(track.Name, savedEntry.Title);
            Assert.AreEqual("Test Artist", savedEntry.Author);
            Assert.AreEqual(4.5f, savedEntry.Rating);
            Assert.AreEqual("Great song!", savedEntry.Review);
            Assert.IsTrue(songAddedEventTriggered);
        }

        [TestMethod]
        public void PerformSearch_UpdatesComboBoxItems()
        {
            // Arrange
            _addForm!.SongComboBox.Text = "Chandelier Will Paquin";

            // Act
            _addForm.PerformSearch();

            // Assert
            Assert.AreEqual("Chandelier"!, ((FullTrack)_addForm.SongComboBox.Items[0]!).Name!);
        }

        [TestMethod]
        public void PerformSearch_DoesNotUpdateComboBox_WhenTextIsEmpty()
        {
            // Arrange
            _addForm!.SongComboBox.Text = string.Empty;

            // Act
            _addForm.PerformSearch();

            // Assert
            Assert.AreEqual(0, _addForm.SongComboBox.Items.Count);
        }

        [TestMethod]
        public void AddButton_Click_SavesEntryWithCorrectDate()
        {
            // Arrange
            var track = new FullTrack { Name = "Test Song", Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Test Artist" } } };
            _addForm!.SongComboBox.Items.Add(track);
            _addForm.SongComboBox.SelectedItem = track;
            var expectedDate = new DateTime(2023, 10, 1);
            _addForm.EntryDateTimePicker.Value = expectedDate;
            _addForm.ReviewTextBox.Text = "Great song!";
            _addForm.rating = 4.0f;

            // Act
            _addForm.AddButton1_Click(null!, null!);

            // Assert
            var savedEntry = _inMemoryDbContext!.Entries.FirstOrDefault();
            Assert.IsNotNull(savedEntry);
            Assert.AreEqual(expectedDate, savedEntry.EntryDate.ToDateTime(TimeOnly.MinValue));
        }

        [TestMethod]
        public void AddButton_Click_DoesSaveEntry_WhenNoRatingIsSet()
        {
            // Arrange
            var track = new FullTrack { Name = "Test Song", Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Test Artist" } } };
            _addForm!.SongComboBox.Items.Add(track);
            _addForm.SongComboBox.SelectedItem = track;
            _addForm.EntryDateTimePicker.Value = DateTime.Now;
            _addForm.ReviewTextBox.Text = "Great song!";
            _addForm.rating = 0.0f; // No rating set

            // Act
            _addForm.AddButton1_Click(null!, null!);

            // Assert
            var savedEntry = _inMemoryDbContext!.Entries.FirstOrDefault();
            Assert.IsNotNull(savedEntry);
        }

    }
}
