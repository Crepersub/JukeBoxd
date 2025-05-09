using JukeBoxd.Forms;
using JukeBoxd.Models;
using Tests.TestBusinessLayer;
namespace Tests.TestForms;

[TestClass]
public class TestUpdate
{
    public required TestDBContext _inMemoryDbContext;
    public required Update _updateForm;
    public required Entry _testEntry;

    [TestInitialize]
    public void Setup()
    {
        // Initialize the in-memory DbContext  
        _inMemoryDbContext = new();
        _inMemoryDbContext.Database.EnsureCreated();

        // Seed sample data  
        _testEntry = new Entry
        {
            Id = 1,
            Title = "Test Song",
            Author = "Test Artist",
            EntryDate = DateOnly.FromDateTime(DateTime.Now),
            Rating = 4.0f,
            Review = "Great song!"
        };
        _inMemoryDbContext.Entries.Add(_testEntry);
        _inMemoryDbContext.SaveChanges();

        // Initialize the Update form  
        _updateForm = new Update(_testEntry.Title, _testEntry.Author, _testEntry.EntryDate, _testEntry.Id, _testEntry.Review, _inMemoryDbContext);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Clear the in-memory database after each test  
        _inMemoryDbContext.Entries.RemoveRange(_inMemoryDbContext.Entries);
        _inMemoryDbContext.Database.EnsureDeleted();
        _inMemoryDbContext.Dispose();
    }

    [TestMethod]
    public void UpdateButton_Click_UpdatesEntryAndClosesForm()
    {
        // Arrange  
        _updateForm.ReviewTextBox.Text = "Updated review";
        _updateForm.EntryDateTimePicker.Value = DateTime.Now.AddDays(-1);
        _updateForm.rating = 5.0f;

        bool songUpdatedEventTriggered = false;
        _updateForm.SongUpdated += (s, e) => songUpdatedEventTriggered = true;

        // Act  
        _updateForm.UpdateButton_Click(null!, null!);

        // Assert  
        var updatedEntry = _inMemoryDbContext.Entries.FirstOrDefault(e => e.Id == _testEntry.Id);
        Assert.IsNotNull(updatedEntry);
        Assert.AreEqual("Updated review", updatedEntry.Review);
        Assert.AreEqual(5.0f, updatedEntry.Rating);
        Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), updatedEntry.EntryDate);
        Assert.IsTrue(songUpdatedEventTriggered);
    }

    [TestMethod]
    public void SetRating_UpdatesRatingValueCorrectly()
    {
        // Act  
        _updateForm.SetRating(4);

        // Assert  
        Assert.AreEqual(2.0f, _updateForm.rating);
    }
}
