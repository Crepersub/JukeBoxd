using System.Data;
using JukeBoxd.Models;
using SpotifyAPI.Web;
using JukeBoxd.Forms;
using System.Text.Json;
namespace JukeBoxd
{
    /// <summary>
    /// Represents the main program class for the JukeBoxd application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The currently logged-in user.
        /// </summary>
        static public User? CurrentUser;

        /// <summary>
        /// The database context for managing users and entries.
        /// </summary>
        static public DiaryDbContext dbContext = new DiaryDbContext();

        static string filepath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName + "/ClientSecrets.json";
        static string fullJSON = File.ReadAllText(filepath);
        static ClientSecret clientSecret = JsonSerializer.Deserialize<ClientSecret>(fullJSON);
        /// <summary>
        /// Spotify client configuration with authentication.
        /// </summary>
        static SpotifyClientConfig config = SpotifyClientConfig
            .CreateDefault()
            .WithAuthenticator(new ClientCredentialsAuthenticator(clientSecret.clientID, clientSecret.clientSecret));

        /// <summary>
        /// Spotify client instance for interacting with the Spotify API.
        /// </summary>
        static public SpotifyClient spotify = new SpotifyClient(config);

        /// <summary>
        /// The login form instance.
        /// </summary>
        static public Login login = new Login();

        /// <summary>
        /// The main form instance.
        /// </summary>
        static public Main main = new Main();

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(login);
        }
    }
}
