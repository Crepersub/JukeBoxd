using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace JukeBoxd.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public TimeSpan Length { get; set; }
        public DateOnly EntryDate { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public string SpotifyToken { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public Entry(FullTrack? track, int userid, float rating, DateOnly date, string review)
        {
            userId = userid;
            Title = track!.Name;
            Author = string.Join(", ", track.Artists.Select(a => a.Name));
            Length = TimeSpan.FromMilliseconds(track.DurationMs);
            EntryDate = date;
            SpotifyToken = track.Id;
            Review = review;
            Rating = rating;
        }
        public Entry() { }
        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
