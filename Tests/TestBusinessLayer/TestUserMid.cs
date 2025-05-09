using JukeBoxd;
using JukeBoxd.Models;
namespace Tests.TestBusinessLayer;

[TestClass]
public class TestUserMid
{
    public required TestDBContext _inMemoryDbContext;

    [TestInitialize]
    public void Setup()
    {
        // Initialize the in-memory DbContext
        _inMemoryDbContext = new();
        _inMemoryDbContext.Database.EnsureCreated();

        // Seed sample data
        _inMemoryDbContext.Users.Add(new User("Ivan"));
        _inMemoryDbContext.SaveChanges();
        _inMemoryDbContext.Entries.AddRange(new List<Entry>
        {
            new Entry { UserId = _inMemoryDbContext.Users.FirstOrDefault(x=>x.Username=="Ivan")!.Id, Title = "Chandelier", Author = "Will Paquin", Length = TimeSpan.FromMinutes(3), EntryDate = DateOnly.FromDateTime(DateTime.Now), Rating = 4.0f, Review = "Great song!", SpotifyToken = "abc" }
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
    public void AddUser_AddsUserToDatabase()
    {
        // Act
        UserMid.AddUser("John", _inMemoryDbContext);

        // Assert
        Assert.AreEqual(2, _inMemoryDbContext.Users.Count());
        var addedUser = _inMemoryDbContext.Users.FirstOrDefault(u => u.Username == "John");
        Assert.IsNotNull(addedUser);
    }

    [TestMethod]
    public void RemoveUser_RemovesUserAndEntriesFromDatabase()
    {
        // Act
        UserMid.RemoveUser("Ivan", _inMemoryDbContext);

        // Assert
        Assert.AreEqual(0, _inMemoryDbContext.Users.Count());
        Assert.AreEqual(0, _inMemoryDbContext.Entries.Count());
    }

    [TestMethod]
    public void ChangeUsername_UpdatesUsernameInDatabase()
    {
        // Act
        UserMid.ChangeUsername("Ivan", "IvanUpdated", _inMemoryDbContext);

        // Assert
        var updatedUser = _inMemoryDbContext.Users.FirstOrDefault(u => u.Username == "IvanUpdated");
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual("IvanUpdated", updatedUser.Username);
    }

    [TestMethod]
    public void GetUsersEntries_ReturnsCorrectEntries()
    {
        // Act
        var entries = UserMid.GetUsersEntries(_inMemoryDbContext.Users.FirstOrDefault(x => x.Username == "Ivan")!.Id, _inMemoryDbContext);

        // Assert
        Assert.AreEqual(1, entries.Count);
        Assert.AreEqual("Chandelier", entries[0].Title);
    }
}
