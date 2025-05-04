using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace JukeBoxd.Models
{
    /// <summary>
    /// Represents an entry in the JukeBoxd application.
    /// </summary>
    public class Entry
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who created the entry.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the title of the entry.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the author or artist of the entry.
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Gets or sets the length of the entry in time.
        /// </summary>
        public TimeSpan Length { get; set; }

        /// <summary>
        /// Gets or sets the date the entry was created.
        /// </summary>
        public DateOnly EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the rating of the entry.
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// Gets or sets the review text for the entry.
        /// </summary>
        public string? Review { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Spotify token associated with the entry.
        /// </summary>
        public string? SpotifyToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user who created the entry.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class with the specified parameters.
        /// </summary>
        /// <param name="track">The Spotify track associated with the entry.</param>
        /// <param name="userid">The unique identifier of the user who created the entry.</param>
        /// <param name="rating">The rating of the entry.</param>
        /// <param name="date">The date the entry was created.</param>
        /// <param name="review">The review text for the entry.</param>
        public Entry(FullTrack? track, int userid, float rating, DateOnly date, string review)
        {
            UserId = userid;
            Title = track!.Name;
            Author = string.Join(", ", track.Artists.Select(a => a.Name));
            Length = TimeSpan.FromMilliseconds(track.DurationMs);
            EntryDate = date;
            SpotifyToken = track.Id;
            Review = review;
            Rating = rating;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class.
        /// </summary>
        public Entry() { }

        /// <summary>
        /// Returns a string representation of the entry.
        /// </summary>
        /// <returns>A string containing the title and author of the entry.</returns>
        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
