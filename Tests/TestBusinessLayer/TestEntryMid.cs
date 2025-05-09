using System.Text.Json;
using JukeBoxd;
using JukeBoxd.BusinessLayer;
using JukeBoxd.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
namespace Tests.TestBusinessLayer;

public class TestDBContext : DiaryDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseInMemoryDatabase($"{Guid.NewGuid().ToString()}");
    }


}
[TestClass]
public class TestEntryMid
{
    public required TestDBContext _inMemoryDbContext;

    [TestInitialize]
    public void Setup()
    {

        // Initialize the in-memory DbContext
        _inMemoryDbContext = new();
        _inMemoryDbContext.Database.EnsureCreated();

        _inMemoryDbContext.Users.Add(new User("Ivan"));
        _inMemoryDbContext.SaveChanges();
        // Seed sample data
        _inMemoryDbContext.Entries.AddRange(new List<Entry>
        {
            new Entry(){UserId=_inMemoryDbContext.Users.FirstOrDefault(x=>x.Username=="Ivan")!.Id,Title="Chandelier",Author = "Will Paquin", Length = TimeSpan.FromMinutes(3), EntryDate = DateOnly.FromDateTime(DateTime.Now), Rating = 4.0f, Review = "Great song!", SpotifyToken = "abc"},
        });
        _inMemoryDbContext.SaveChanges();
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Clear the in-memory database after each test
        _inMemoryDbContext.Entries.RemoveRange(_inMemoryDbContext.Entries);
        _inMemoryDbContext.Users.RemoveRange(_inMemoryDbContext.Users);
        _inMemoryDbContext.Database.EnsureDeleted();
        _inMemoryDbContext.Dispose();
    }

    [TestMethod]
    public void SpotifySearch_ReturnsTracks_WhenTracksExist()
    {
        var filePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/JukeBoxd" + "/ClientSecrets.json";
        // Arrange  
        var clientSecrets = JsonSerializer.Deserialize<ClientSecret>(File.ReadAllText(filePath));
        var config = SpotifyClientConfig.CreateDefault()
            .WithAuthenticator(new ClientCredentialsAuthenticator(clientSecrets!.ClientID!, clientSecrets.clientSecret!));
        var spotifyClient = new SpotifyClient(config);

        var tracks = new List<FullTrack>
       {
           new FullTrack
           {
               Id = "1",
               Name = "Track1",
               Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Artist1" } }
           },
           new FullTrack
           {
               Id = "2",
               Name = "Track2",
               Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Artist2" } }
           }
       };

        // Simulate Spotify API response  
        var searchResponse = new SearchResponse
        {
            Tracks = new Paging<FullTrack, SearchResponse>
            {
                Items = tracks
            }
        };

        // Act  
        var result = EntryMid.SpotifySearch("Chandelier Will Paquin", spotifyClient);
        Assert.AreEqual("Chandelier", result[0].Name);
        Assert.AreEqual("Will Paquin", result[0].Artists[0].Name);
    }
    [TestMethod]
    public void AddEntry_WithEntryObject_AddsToDb()
    {
        // Arrange
        Entry newEntry = new Entry
        {
            UserId = 0,
            Title = "Elastic Heart",
            Author = "Sia",
            Length = TimeSpan.FromMinutes(4),
            EntryDate = DateOnly.FromDateTime(DateTime.Now),
            Rating = 5.0f,
            Review = "Amazing song!",
            SpotifyToken = "xyz"
        };

        // Act
        EntryMid.AddEntry(newEntry, _inMemoryDbContext);

        // Assert
        Assert.AreEqual(2, _inMemoryDbContext.Entries.Count());
        var addedEntry = _inMemoryDbContext.Entries.FirstOrDefault(x => x.Title == "Elastic Heart");
        Assert.IsNotNull(addedEntry);
        Assert.AreEqual("Sia", addedEntry.Author);
        Assert.AreEqual(5.0f, addedEntry.Rating);
        Assert.AreEqual("Amazing song!", addedEntry.Review);
    }
    [TestMethod]
    public void AddEntry_WithTrack_AddsToDb()
    {
        List<SimpleArtist> artists = new() { new SimpleArtist() { Name = "Ivan" } };
        FullTrack spotifyTrack = new FullTrack()
        {
            Name = "Chandelier",
            Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Sia" } },
            ExternalIds = new Dictionary<string, string> { { "isrc", "QM24S2009107" } }
        };

        EntryMid.AddEntry(spotifyTrack, 0, 4.5f, DateOnly.FromDateTime(DateTime.Now), "Great!", _inMemoryDbContext);

        Assert.AreEqual(2, _inMemoryDbContext.Entries.Count());
    }

    [TestMethod]
    public void UpdateEntry_UpdatesEntryInDatabase()
    {
        // Arrange
        var entry = _inMemoryDbContext.Entries.First(x => x.Title == "Chandelier");

        // Act
        EntryMid.UpdateEntry(_inMemoryDbContext.Entries.First(x => x.Title == "Chandelier").Id, 4.5f, DateOnly.FromDateTime(DateTime.Now), "Updated review", _inMemoryDbContext);

        // Assert
        Assert.AreEqual(_inMemoryDbContext.Entries.First(x => x.Title == "Chandelier").Rating, entry.Rating);
        Assert.AreEqual("Updated review", entry.Review);
    }

    [TestMethod]
    public void RemoveEntry_RemovesEntryFromDatabase()
    {
        // Act
        EntryMid.RemoveEntry(1, _inMemoryDbContext);

        // Assert
        Assert.AreEqual(0, _inMemoryDbContext.Entries.Count());
        Assert.IsNull(_inMemoryDbContext.Entries.Find(1));
    }

    [TestMethod]
    public void GetSongId_ReturnsCorrectSongId()
    {
        // Act
        var result = EntryMid.GetSongId(1, _inMemoryDbContext);

        // Assert
        Assert.AreEqual(1, result);
    }
}
