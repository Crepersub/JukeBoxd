using JukeBoxd.Models;
using SpotifyAPI.Web;

namespace JukeBoxd.BusinessLayer
{
    /// <summary>
    /// Provides methods for interacting with Spotify and managing entries in the database.
    /// </summary>
    public class EntryMid
    {
        /// <summary>
        /// Searches for tracks on Spotify based on a search query.
        /// </summary>
        /// <param name="text">The search query text.</param>
        /// <param name="spotify">The Spotify client used to perform the search.</param>
        /// <returns>A list of tracks matching the search query.</returns>
        public static List<FullTrackWithString> SpotifySearch(string text, SpotifyClient spotify)
        {
            var searchResult = spotify.Search.Item(new SearchRequest(SearchRequest.Types.Track, text)).Result;
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
        /// <param name="date">The date the entry was created.</param>
        /// <param name="review">The review text for the entry.</param>
        /// <param name="dbContext">The database context to use for the operation.</param>
        public static void AddEntry(FullTrack track, int userid, float rating, DateOnly date, string review, DiaryDbContext dbContext)
        {
            dbContext.Entries.Add(new Entry(track, userid, rating, date, review));
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Adds a new entry to the database.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        /// <param name="dbContext">The database context to use for the operation.</param>
        public static void AddEntry(Entry entry, DiaryDbContext dbContext)
        {
            dbContext.Entries.Add(entry);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates the rating, date, and review of an existing entry.
        /// </summary>
        /// <param name="entryid">The ID of the entry to update.</param>
        /// <param name="newrating">The new rating to assign to the entry.</param>
        /// <param name="date">The new date to assign to the entry.</param>
        /// <param name="review">The new review text to assign to the entry.</param>
        /// <param name="dbContext">The database context to use for the operation.</param>
        public static void UpdateEntry(int entryid, float newrating, DateOnly date, string review, DiaryDbContext dbContext)
        {
            var entry = dbContext.Entries.FirstOrDefault(x => x.Id == entryid);
            if (entry is not null)
            {
                entry.Rating = newrating;
                entry.EntryDate = date;
                entry.Review = review;
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Removes an entry from the database.
        /// </summary>
        /// <param name="entryid">The ID of the entry to remove.</param>
        /// <param name="dbContext">The database context to use for the operation.</param>
        public static void RemoveEntry(int entryid, DiaryDbContext dbContext)
        {
            var entry = dbContext.Entries.FirstOrDefault(x => x.Id == entryid);
            if (entry is not null)
            {
                dbContext.Entries.Remove(entry);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves the song ID associated with a specific entry.
        /// </summary>
        /// <param name="entryID">The ID of the entry to retrieve the song ID for.</param>
        /// <param name="dbContext">The database context to use for the operation.</param>
        /// <returns>The song ID associated with the entry.</returns>
        public static int GetSongId(int entryID, DiaryDbContext dbContext)
        {
            var title = dbContext.Entries.SingleOrDefault(x => x.Id == entryID);
            return title!.Id;
        }
    }
}
