using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JukeBoxd.Models;
using Microsoft.EntityFrameworkCore;

namespace JukeBoxd
{
    public class UserMid
    {
        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="username">The username of the new user.</param>
        public static void AddUser(string username)
        {
            Program.dbContext.Users.Add(new User(username));
            Program.dbContext.SaveChanges();
        }

        /// <summary>
        /// Removes a user and their associated entries from the database.
        /// </summary>
        /// <param name="username">The username of the user to remove.</param>
        public static void RemoveUser(string username)
        {
            var user = Program.dbContext.Users.FirstOrDefault(u => u.Username == username);
            if (user is not null)
            {
                var entries = Program.dbContext.Entries.Where(x => x.User.Username == username).ToList();
                Program.dbContext.Users.Remove(user);
                Program.dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Changes the username of an existing user.
        /// </summary>
        /// <param name="oldUsername">The current username of the user.</param>
        /// <param name="newUsername">The new username to assign to the user.</param>
        public static void ChangeUsername(string oldUsername, string newUsername)
        {
            var user = Program.dbContext.Users.FirstOrDefault(u => u.Username == oldUsername);
            if (user != null)
            {
                user.Username = newUsername;
                Program.dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all entries associated with a specific user.
        /// </summary>
        /// <param name="userid">The ID of the user.</param>
        /// <returns>A list of entries belonging to the user.</returns>
        public static List<Entry> GetUsersEntries(int userid)
        {
            return Program.dbContext.Entries.Where(x => x.User.Id == userid).ToList();
        }
    }
}
