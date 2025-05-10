using System.Text.Json;
using JukeBoxd.Forms;
using JukeBoxd.Models;
using SpotifyAPI.Web;
using Tests.TestBusinessLayer;
namespace Tests.TestForms;

[TestClass]
public class TestUsers
{
    public required TestDBContext _inMemoryDbContext;
    public required Users _usersForm;
    static string filePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/JukeBoxd" + "/ClientSecrets.json";
    static ClientSecret clientSecrets = JsonSerializer.Deserialize<ClientSecret>(File.ReadAllText(filePath))!;
    static SpotifyClientConfig config = SpotifyClientConfig.CreateDefault()
        .WithAuthenticator(new ClientCredentialsAuthenticator(clientSecrets.ClientID!, clientSecrets.clientSecret!));
    private SpotifyClient _spotifyClient = new SpotifyClient(config);

    [TestInitialize]
    public void Setup()
    {
        // Initialize the in-memory DbContext
        _inMemoryDbContext = new();
        _inMemoryDbContext.Database.EnsureCreated();

        // Seed sample data
        _inMemoryDbContext.Users.AddRange(new[]
        {
            new User("Alice"),
            new User("Bob")
        });
        _inMemoryDbContext.SaveChanges();

        // Initialize the Users form with a mock Login object
        var mockCurrentUser = new User("TestUser"); // Mock current user
        var mockLoginForm = new Login(_inMemoryDbContext, mockCurrentUser, _spotifyClient);
        _usersForm = new Users(_inMemoryDbContext, mockLoginForm);
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
    public void Users_Load_PopulatesListBoxWithUsernames()
    {
        // Act
        _usersForm.Users_Load(null!, null!);

        // Assert
        Assert.AreEqual(2, _usersForm.UsersListBox.Items.Count);
        Assert.IsTrue(_usersForm.UsersListBox.Items.Contains("Alice"));
        Assert.IsTrue(_usersForm.UsersListBox.Items.Contains("Bob"));
    }

    [TestMethod]
    public void AddUserButton_Click_AddsNewUserToDatabase()
    {
        // Arrange
        _usersForm.UsernameTextBox.Text = "Charlie";

        // Act
        _usersForm.AddUserButton_Click(null!, null!);

        // Assert
        Assert.AreEqual(3, _inMemoryDbContext.Users.Count());
        Assert.IsTrue(_inMemoryDbContext.Users.Any(u => u.Username == "Charlie"));
    }

    [TestMethod]
    public void Reload_UpdatesListBoxWithNewUsernames()
    {
        // Arrange
        _usersForm.Users_Load(null!, null!);
        _inMemoryDbContext.Users.Add(new User("David"));
        _inMemoryDbContext.SaveChanges();

        // Act
        _usersForm.Reload();

        // Assert
        Assert.AreEqual(3, _usersForm.UsersListBox.Items.Count);
        Assert.IsTrue(_usersForm.UsersListBox.Items.Contains("David"));
    }
}
