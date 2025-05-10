using System.Text.Json;
using JukeBoxd.Models;
using NAudio.Wave;
using SpotifyAPI.Web;

namespace JukeBoxd.BusinessLayer
{
    /// <summary>
    /// Represents a client for interacting with the Deezer API.
    /// </summary>
    public class DeezerClient
    {
        /// <summary>
        /// The HTTP client used for making requests to the Deezer API.
        /// </summary>
        static private HttpClient client = new();

        /// <summary>
        /// Searches for a track on Deezer using the ISRC (International Standard Recording Code) of a Spotify track.
        /// </summary>
        /// <param name="track">The Spotify track to search for on Deezer.</param>
        /// <returns>A <see cref="DeezerTrack"/> object if a matching track is found; otherwise, null.</returns>
        static public DeezerTrack SearchTrackISRC(FullTrack track)
        {
            try
            {
                Uri uri = new($"https://api.deezer.com/search?q=artist:\"{Uri.EscapeDataString(track.Artists.First().Name)}\" track:\"{Uri.EscapeDataString(track.Name)}\"");

                HttpResponseMessage response = client.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();
                string responseContent = response.Content.ReadAsStringAsync().Result;

                var json = JsonDocument.Parse(responseContent);
                var tracks = json.RootElement.GetProperty("data");

                if (tracks.GetArrayLength() == 0)
                    return null!;

                var first = tracks[0];

                // Now fetch track details
                Uri secondURI = new($"https://api.deezer.com/track/{first.GetProperty("id")}");
                HttpResponseMessage response2 = client.GetAsync(secondURI).Result;
                response2.EnsureSuccessStatusCode();
                string responseContent2 = response2.Content.ReadAsStringAsync().Result;
                var json2 = JsonDocument.Parse(responseContent2);
                var track2 = json2.RootElement;

                var DeezerTrack = new DeezerTrack
                {
                    PreviewURL = track2.GetProperty("preview").GetString() ?? string.Empty,
                    Id = track2.GetProperty("id").GetRawText(), // Use GetRawText to handle numbers as strings
                    ISRC = track2.GetProperty("isrc").GetString() ?? string.Empty
                };

                if (track.ExternalIds["isrc"] == DeezerTrack.ISRC)
                {
                    return DeezerTrack;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception)
            {
                return null!;
            }
        }

        /// <summary>
        /// Indicates whether a preview track is currently playing.
        /// </summary>
        private static bool isPlaying = false;

        /// <summary>
        /// Plays a preview of a track from the given URL.
        /// </summary>
        /// <param name="previewUrl">The URL of the track preview to play.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        static public async Task PlayPreviewAsync(string previewUrl)
        {
            if (!isPlaying)
            {
                using var mf = new MediaFoundationReader(previewUrl);
                using var wo = new WaveOutEvent();
                wo.Init(mf);
                wo.Volume = 0.1f;
                wo.Play();
                isPlaying = true;
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(500);
                }
                isPlaying = false;
            }
        }
    }
}
