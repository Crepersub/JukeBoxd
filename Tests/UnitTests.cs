using JukeBoxd.Models;
using Microsoft.EntityFrameworkCore;
using JukeBoxd;
using System.Text.Json;

namespace Tests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
            // nope
        }

        [Test]
        public void ClientSecretTest()
        {
            // Arrange
            var filepath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName + "/ClientSecrets.json";
            var fullJSON = File.ReadAllText(filepath);
            var clientSecret = JsonSerializer.Deserialize<ClientSecret>(fullJSON);

            // Act
            var clientID = clientSecret.clientID;
            var clientSecretValue = clientSecret.clientSecret;

            // Assert
            Assert.That(clientID, Is.Not.Null);
            Assert.That(clientSecretValue, Is.Not.Null);
        }

        [Test]
        public void AddUserTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DiaryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DiaryDbContext(options))
            {
                var username = "TestUser";

                // Act
                UserMid.AddUser(username);

                // Assert
                var savedUser = context.Users.FirstOrDefault(u => u.Username == username);
                Assert.That(savedUser, Is.Not.Null);
                Assert.That(savedUser.Username, Is.EqualTo(username));
            }
        }


        [Test]
        public void AddEntryTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DiaryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DiaryDbContext(options))
            {
                var entry = new Entry
                {
                    Id = 1,
                    userId = 1,
                    Title = "Test Track",
                    Author = "Test Artist",
                    Length = TimeSpan.FromMinutes(3),
                    EntryDate = DateOnly.FromDateTime(DateTime.Now),
                    Rating = 4.5f,
                    Review = "Great track!",
                    SpotifyToken = null,
                    User = null
                };

                // Act
                EntryMid.AddEntry(entry);

                // Assert
                var savedEntry = context.Entries.Find(1);
                Assert.Multiple(() =>
                {
                    Assert.That(savedEntry, Is.Not.Null); 
                    Assert.That(savedEntry.Title, Is.EqualTo(entry.Title));
                    Assert.That(savedEntry.Author, Is.EqualTo(entry.Author));
                    Assert.That(savedEntry.Length, Is.EqualTo(entry.Length));
                    Assert.That(savedEntry.EntryDate, Is.EqualTo(entry.EntryDate));
                    Assert.That(savedEntry.Rating, Is.EqualTo(entry.Rating));
                    Assert.That(savedEntry.Review, Is.EqualTo(entry.Review));
                });
            }
        }
    }
}
