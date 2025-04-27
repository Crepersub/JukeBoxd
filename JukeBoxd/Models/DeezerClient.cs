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
        //static public async Task<DeezerTrack> SearchTrackISRC(FullTrack track)
        //{
        //    //Uri uri = new Uri($"https://api.deezer.com/search?q=isrc:{Uri.EscapeDataString(isrc)}");
        //    Uri uri = new($"https://api.deezer.com/search?q=artist:{string.Join(" ", track)}";

        //    byte[] data = client.DownloadData(uri);
        //    string response = System.Text.Encoding.UTF8.GetString(data);

        //    var json = JsonDocument.Parse(response);
        //    var tracks = json.RootElement.GetProperty("data");

        //    if (tracks.GetArrayLength() == 0)
        //        return null;

        //    var first = tracks[0];
        //    var DeezerTrack = new DeezerTrack
        //    {
        //        PreviewURL = first.GetProperty("preview").GetString()
        //    };
        //    return DeezerTrack;
        //}
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
