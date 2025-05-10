namespace JukeBoxd.Models
{
    /// <summary>
    /// Represents a track from Deezer with associated metadata.
    /// </summary>
    public class DeezerTrack
    {
        /// <summary>
        /// Gets or sets the URL for the preview of the track.
        /// </summary>
        public string? PreviewURL { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the track.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the International Standard Recording Code (ISRC) of the track.
        /// </summary>
        public string? ISRC { get; set; }
    }
}
