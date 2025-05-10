namespace JukeBoxd.Models
{
    /// <summary>
    /// Represents a user in the JukeBoxd application.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets the list of entries associated with the user.
        /// </summary>
        public List<Entry> Entries { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with the specified username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        public User(string username)
        {
            Username = username;
        }
    }
}
