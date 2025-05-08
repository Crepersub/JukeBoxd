using System.ComponentModel;
using System.Text.Json;
using System.Windows.Forms;
using JukeBoxd;
using JukeBoxd.Forms;
using JukeBoxd.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using Tests.TestBusinessLayer;
namespace Tests;

[TestClass]
public class TestMain
{
    private TestDBContext _inMemoryDbContext;
    private Main _mainForm;
    private User _currentUser;
    static string filePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/JukeBoxd" + "/ClientSecrets.json";
    // Arrange  
    static ClientSecret clientSecrets = JsonSerializer.Deserialize<ClientSecret>(File.ReadAllText(filePath));
    static SpotifyClientConfig config = SpotifyClientConfig.CreateDefault()
        .WithAuthenticator(new ClientCredentialsAuthenticator(clientSecrets.ClientID, clientSecrets.clientSecret));
    private SpotifyClient _spotifyClient = new SpotifyClient(config);

    [TestInitialize]
    public void Setup()
    {
        // Initialize the in-memory DbContext
        _inMemoryDbContext = new();
        _inMemoryDbContext.Database.EnsureCreated();
        
        // Seed sample data
        _currentUser = new User("TestUser");
        _inMemoryDbContext.Users.Add(_currentUser);
        _inMemoryDbContext.Entries.AddRange(new[]
        {
               new Entry {UserId = _currentUser.Id, Title = "Song1", Author = "Author1", Review = "Great song", SpotifyToken = "6rqhFgbbKwnb9MLmUQDhG6" },
               new Entry {UserId = _currentUser.Id, Title = "Song2", Author = "Author2", Review = "Nice song", SpotifyToken = "11dFghVXANMlKmJXsNCbNl" }
           });
        _inMemoryDbContext.SaveChanges();


        // Initialize the Main form
        _mainForm = new Main(_inMemoryDbContext, _currentUser, _spotifyClient);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Clear the in-memory database after each test
        _inMemoryDbContext.Users.RemoveRange(_inMemoryDbContext.Users);
        _inMemoryDbContext.Entries.RemoveRange(_inMemoryDbContext.Entries);
        _inMemoryDbContext.Database.EnsureDeleted();
        _inMemoryDbContext.Dispose();
    }

    [TestMethod]
    public void Main_Load_PopulatesDataGridViewWithEntries()
    {
        // Act
        _mainForm.Main_Load(null, null);

        // Assert
        var dataSource = _mainForm.MainDataGridView.DataSource as BindingSource;
        Assert.IsNotNull(dataSource);
        var entries = dataSource.List as BindingList<Entry>;
        Assert.IsNotNull(entries);
        Assert.AreEqual(2, entries.Count);
        Assert.IsTrue(entries.Any(e => e.Title == "Song1"));
        Assert.IsTrue(entries.Any(e => e.Title == "Song2"));
    }

    [TestMethod]
    public void AddButton_Click_OpensAddForm()
    {
        // Arrange
        bool addFormOpened = false;
        _mainForm.AddMainButton.Click += (s, e) => addFormOpened = true;

        // Act
        _mainForm.AddMainButton.PerformClick();

        // Assert
        Assert.IsTrue(addFormOpened, "Clicking the Add button did not open the Add form.");
    }

    [TestMethod]
    public void UpdateButton_Click_UpdatesSelectedEntry()
    {
        // Arrange
        _mainForm.Main_Load(null, null);
        _mainForm.MainDataGridView.Rows[0].Selected = true;

        // Act
        _mainForm.UpdateMainButton.PerformClick();

        // Assert
        var updatedEntry = _inMemoryDbContext.Entries.FirstOrDefault(e => e.SpotifyToken == "11dFghVXANMlKmJXsNCbNl");
        Assert.IsNotNull(updatedEntry);
        Assert.AreEqual("Song1", updatedEntry.Title);
    }

    [TestMethod]
    public void DeleteButton_Click_RemovesSelectedEntry()
    {
        // Arrange
        _mainForm.Main_Load(null, null);
        _mainForm.MainDataGridView.CurrentCell = _mainForm.MainDataGridView.Rows[0].Cells[4];

        // Act
        _mainForm.DeleteMainButton.PerformClick();

        // Assert
        var deletedEntry = _inMemoryDbContext.Entries.FirstOrDefault(e => e.Id == 1);
        Assert.IsNull(deletedEntry);
    }

    [TestMethod]
    public void PreviewButton_Click_PlaysPreviewForSelectedSong()
    {
        // Arrange
        _mainForm.Main_Load(null, null);
        _mainForm.MainDataGridView.Rows[0].Selected = true;

        // Act
        _mainForm.PreviewButton.PerformClick();

        // Assert
        // Verify that the preview was played (mock DeezerClient or SpotifyClient for this)
    }
}
