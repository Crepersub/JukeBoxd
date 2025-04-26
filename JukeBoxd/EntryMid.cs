using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JukeBoxd.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;

namespace JukeBoxd
{
    public  class EntryMid
    {
        /// <summary>
        /// Searches for tracks on Spotify based on a search query.
        /// </summary>
        /// <param name="text">The search query text.</param>
        /// <returns>A list of tracks matching the search query.</returns>
        public static List<FullTrackWithString> SpotifySearch(string text)
        {
            var searchResult = Program.spotify.Search.Item(new SearchRequest(SearchRequest.Types.Track, text)).Result;
            if (searchResult.Tracks?.Items == null)
            {
                return new List<FullTrackWithString>();
            }

            return searchResult.Tracks.Items
                .Where(x => x != null)
                .Select(x => new FullTrackWithString
                {
                    Id = x.Id,
                    Name = x.Name,
                    Artists = x.Artists,
                    Album = x.Album,
                    DurationMs = x.DurationMs
                })
                .ToList();
        }

        /// <summary>
        /// Adds a new entry to the database for a specific user.
        /// </summary>
        /// <param name="track">The track to add as an entry.</param>
        /// <param name="userid">The ID of the user associated with the entry.</param>
        /// <param name="rating">The rating of the track.</param>
        public static void AddEntry(FullTrack track, int userid, float rating)
        {
            Program.dbContext.Entries.Add(new Entry(track, userid, rating));
            Program.dbContext.SaveChanges();
        }

        /// <summary>
        /// Adds a new entry to the database.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        public static void AddEntry(Entry entry)
        {
            Program.dbContext.Entries.Add(entry);
            Program.dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates the rating of an existing entry.
        /// </summary>
        /// <param name="entryid">The ID of the entry to update.</param>
        /// <param name="newrating">The new rating to assign to the entry.</param>
        public static void ChangeRating(int entryid, float newrating)
        {
            var entry = Program.dbContext.Entries.FirstOrDefault(x => x.Id == entryid);
            if (entry is not null)
            {
                entry.Rating = newrating;
                //new
                entry.EntryDate = DateOnly.FromDateTime(DateTime.Now); 
                Program.dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Removes an entry from the database.
        /// </summary>does
        /// <param name="entryid">The ID of the entry to remove.</param>
        public static void RemoveEntry(int entryid)
        {
            var entry = Program.dbContext.Entries.FirstOrDefault(x => x.Id == entryid);
            if (entry is not null)
            {
                Program.dbContext.Entries.Remove(entry);
                Program.dbContext.SaveChanges();
            }
        }

        public static int GetSongId(int entryID)
        {
            var title = Program.dbContext.Entries.SingleOrDefault(x => x.Id==entryID);
            return title.Id;
        }
    }
}
