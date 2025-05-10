using JukeBoxd.Models;

namespace Tests.TestModels;

[TestClass]
public class TestDeezerTrack
{
    [TestMethod]
    public void DeezerTrack_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var track = new DeezerTrack();

        // Assert
        Assert.IsNull(track.PreviewURL);
        Assert.IsNull(track.Id);
        Assert.IsNull(track.ISRC);
    }

    [TestMethod]
    public void DeezerTrack_ShouldSetAndGetProperties()
    {
        // Arrange
        var track = new DeezerTrack();
        var previewUrl = "https://example.com/preview";
        var id = "12345";
        var isrc = "USRC17607839";

        // Act
        track.PreviewURL = previewUrl;
        track.Id = id;
        track.ISRC = isrc;

        // Assert
        Assert.AreEqual(previewUrl, track.PreviewURL);
        Assert.AreEqual(id, track.Id);
        Assert.AreEqual(isrc, track.ISRC);
    }
}
