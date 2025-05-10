using System.ComponentModel;
using System.Text.Json;
using System.Windows.Forms;
using JukeBoxd.Forms;
using JukeBoxd.Models;
using SpotifyAPI.Web;
using Tests.TestBusinessLayer;
namespace Tests.TestForms;

[TestClass]
public class TestMain
{
    public required TestDBContext _inMemoryDbContext;
    public required Main _mainForm;
    public required User _currentUser;
    static string filePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/JukeBoxd" + "/ClientSecrets.json";
    // Arrange  
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
        _mainForm.Main_Load(null!, null!);

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
    public void UpdateButton_Click_UpdatesSelectedEntry()
    {
        // Arrange
        _mainForm.Main_Load(this, EventArgs.Empty); // Pass non-null arguments to avoid CS8625
        _mainForm.MainDataGridView.Rows[0].Selected = true;

        // Act
        _mainForm.AddButton_Click(null!, null!);

        // Assert
        var updatedEntry = _inMemoryDbContext.Entries.FirstOrDefault(e => e.SpotifyToken == "6rqhFgbbKwnb9MLmUQDhG6");
        Assert.IsNotNull(updatedEntry);
        Assert.AreEqual("Song1", updatedEntry.Title);
    }

    [TestMethod]
    public void PreviewButton_Click_PlaysPreviewForSelectedSong()
    {
        // Arrange
        _mainForm.Main_Load(null!, null!);
        _mainForm.MainDataGridView.Rows[0].Selected = true;

        // Act
        _mainForm.PreviewButton_Click(null!, null!);

        // Assert
        // Verify that the preview was played (mock DeezerClient or SpotifyClient for this)
    }
    [TestMethod]
    public void MainDataGridView_CellClick_1_UpdatesAlbumCoverAndReviewLabel()
    {
        // Arrange
        _mainForm.Main_Load(null!, null!);
        _mainForm.MainDataGridView.Rows[0].Selected = true;
        var cellEventArgs = new DataGridViewCellEventArgs(0, 0);

        // Act
        _mainForm.MainDataGridView_CellClick_1(_mainForm.MainDataGridView, cellEventArgs);

        // Assert
        var selectedRow = _mainForm.MainDataGridView.Rows[0];
        Assert.AreEqual("Great song", _mainForm.ReviewTextBox.Text);
        Assert.IsNotNull(_mainForm.AlbumCoverPictureBox.ImageLocation);
    }
}
