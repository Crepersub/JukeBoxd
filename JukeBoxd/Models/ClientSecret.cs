namespace JukeBoxd.Models
{
    /// <summary>
    /// Represents the client secret information used for authentication.
    /// </summary>
    public class ClientSecret
    {
        /// <summary>
        /// Gets or sets the unique identifier for the client.
        /// </summary>
        public string? ClientID { get; set; }

        /// <summary>
        /// Gets or sets the secret key associated with the client.
        /// </summary>
        public string? clientSecret { get; set; }
    }
}
