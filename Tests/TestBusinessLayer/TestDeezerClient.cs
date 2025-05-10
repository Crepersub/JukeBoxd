using JukeBoxd.BusinessLayer;
using SpotifyAPI.Web;
namespace Tests.TestBusinessLayer;

[TestClass]
public class TestDeezerClient
{
    private FullTrack spotifyTrack = null!;
    private FullTrack fakeTrack = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        spotifyTrack = new FullTrack()
        {
            Name = "Chandelier",
            Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Will Paquin" } },
            ExternalIds = new Dictionary<string, string> { { "isrc", "QM24S2009107" } }
        };
        fakeTrack = new FullTrack()
        {
            Name = "Nonexistent Song",
            Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Nonexistent Artist" } },
            ExternalIds = new Dictionary<string, string> { { "isrc", "INVALIDISRC" } }
        };
    }

    [TestMethod]
    public void SearchTrackISRC_ValidTrack_ReturnsDeezerTrack()
    {
        // Act
        var result = DeezerClient.SearchTrackISRC(spotifyTrack);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("QM24S2009107", result.ISRC);
    }

    [TestMethod]
    public void SearchTrackISRC_InvalidTrack_ReturnsNull()
    {
        // Act
        var result = DeezerClient.SearchTrackISRC(fakeTrack);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task PlayPreviewAsync_InvalidUrl_ThrowsException()
    {
        // Arrange
        var deezerTrack = DeezerClient.SearchTrackISRC(fakeTrack);
        string previewUrl = deezerTrack?.PreviewURL ?? string.Empty;

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
        {
            await DeezerClient.PlayPreviewAsync(previewUrl);
        });
    }
}
