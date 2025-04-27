using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using NAudio.Wave;
using System.Net;
using SpotifyAPI.Web;

namespace JukeBoxd.Models
{
    public class DeezerClient
    {
        static private WebClient client = new WebClient();
        static public async Task<DeezerTrack> SearchTrackISRC(FullTrack track)
        {
            Uri uri = new Uri($"https://api.deezer.com/search?q=artist:\"{Uri.EscapeDataString(track.Artists.First().Name)}\" track:\"{Uri.EscapeDataString(track.Name)}\"");

            byte[] data = client.DownloadData(uri);
            string response = System.Text.Encoding.UTF8.GetString(data);

            var json = JsonDocument.Parse(response);
            var tracks = json.RootElement.GetProperty("data");

            if (tracks.GetArrayLength() == 0)
                return null;

            var first = tracks[0];

            // Now fetch track details
            Uri secondURI = new Uri($"https://api.deezer.com/track/{first.GetProperty("id").GetInt32()}");
            byte[] data2 = client.DownloadData(secondURI);
            string response2 = System.Text.Encoding.UTF8.GetString(data2);
            var json2 = JsonDocument.Parse(response2);
            var track2 = json2.RootElement;

            // No ".GetProperty("data")" here, it's already the object

            var DeezerTrack = new DeezerTrack
            {
                PreviewURL = track2.GetProperty("preview").GetString(),
                Id = track2.GetProperty("id").GetInt32().ToString(),
                ISRC = track2.GetProperty("isrc").GetString()
            };

            if (track.ExternalIds["isrc"] == DeezerTrack.ISRC)
            {
                return DeezerTrack;
            }
            else
            {
                return null;
            }

        }
        static public async Task PlayPreviewAsync(string previewUrl)
        {
            using var mf = new MediaFoundationReader(previewUrl);
            using var wo = new WaveOutEvent();
            wo.Init(mf);
            wo.Play();
            while (wo.PlaybackState == PlaybackState.Playing)
            {
                await Task.Delay(500);
            }
        }
    }
}
