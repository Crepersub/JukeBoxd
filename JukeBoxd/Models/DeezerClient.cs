using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using NAudio.Wave;

namespace JukeBoxd.Models
{
    public class DeezerClient
    {
        static private readonly HttpClient httpClient = new();
        static public async Task<DeezerTrack> SearchTrackISRC(string isrc)
        {
            string url = $"http://api.deezer.com/search?q=isrc:{Uri.EscapeDataString(isrc)}";
            var response = await httpClient.GetStringAsync(url);
            var json = JsonDocument.Parse(response);
            var tracks = json.RootElement.GetProperty("data");

            if (tracks.GetArrayLength() == 0)
                return null;

            var first = tracks[0];
            var track = new DeezerTrack
            {
                PreviewURL = first.GetProperty("preview").GetString()
            };
            return track;
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
