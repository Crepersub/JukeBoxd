using System.Data;
using SpotifyAPI.Web;
namespace JukeBoxd.Models
{
    /// <summary>
    /// Represents a Spotify track with a custom string representation.
    /// </summary>
    public class FullTrackWithString : FullTrack
    {
        /// <summary>
        /// Returns a string representation of the track, including its name and artists.
        /// </summary>
        /// <returns>A string in the format "TrackName by Artist1, Artist2, ...".</returns>
        public override string ToString()
        {
            return $"{Name} by {string.Join(", ", Artists.Select(x => x.Name))}";
        }
    }
}
