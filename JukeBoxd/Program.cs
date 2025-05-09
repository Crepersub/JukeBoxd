using System.Text.Json;
using JukeBoxd.Forms;
using JukeBoxd.Models;
using SpotifyAPI.Web;
namespace JukeBoxd
{
    /// <summary>  
    /// Represents the main program class for the JukeBoxd application.  
    /// This class initializes the application, sets up the database, and manages the main forms.  
    /// </summary>  
    public class Program
    {
        /// <summary>  
        /// The currently logged-in user. This is set after a successful login.  
        /// </summary>  
        static public User? CurrentUser;

        /// <summary>  
        /// The database context for managing users and their diary entries.  
        /// </summary>  
        static public DiaryDbContext dbContext = new();

        /// <summary>  
        /// The file path to the client secrets JSON file, used for Spotify API authentication.  
        /// </summary>  
        static string filepath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName + "/ClientSecrets.json";

        /// <summary>  
        /// The full JSON content of the client secrets file, read as a string.  
        /// </summary>  
        static string fullJSON = File.ReadAllText(filepath);

        /// <summary>  
        /// The deserialized client secret object containing Spotify API credentials.  
        /// </summary>  
        static ClientSecret clientSecret = JsonSerializer.Deserialize<ClientSecret>(fullJSON)!;

        /// <summary>  
        /// Spotify client configuration with authentication using client credentials.  
        /// </summary>  
        static SpotifyClientConfig config = SpotifyClientConfig
            .CreateDefault()
            .WithAuthenticator(new ClientCredentialsAuthenticator(clientSecret.ClientID!, clientSecret.clientSecret!));

        /// <summary>  
        /// Spotify client instance for interacting with the Spotify API.  
        /// </summary>  
        static public SpotifyClient spotify = new(config);

        /// <summary>  
        /// The login form instance, used as the initial form for user authentication.  
        /// </summary>  
        static public Login login = new(dbContext, CurrentUser!, spotify);

        /// <summary>  
        /// The main form instance, displayed after a successful login.  
        /// </summary>  
        static public Main main = new(dbContext, CurrentUser!, spotify);

        /// <summary>  
        /// The main entry point for the application.  
        /// Initializes the database, enables visual styles, and starts the login form.  
        /// </summary>  
        /// <param name="args">Command-line arguments passed to the application.</param>  
        static void Main(string[] args)
        {
            // Ensure the database is created before starting the application.  
            dbContext.Database.EnsureCreated();

            // Enable visual styles for the application.  
            Application.EnableVisualStyles();

            // Start the application with the login form.  
            Application.Run(login);
        }
    }
}
