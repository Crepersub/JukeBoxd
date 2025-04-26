using JukeBoxd;
using JukeBoxd.Models;

namespace JukeBoxd.Forms
{
    public partial class Login : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the login button. 
        /// Sets the current user based on the selected username and opens the main form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is not null)
            {
                Program.CurrentUser = Program.dbContext.Users.Where(x => x.Username == comboBox1.SelectedItem.ToString()).FirstOrDefault()!;
                new Main().Show();
            }
        }

        /// <summary>
        /// Handles the load event for the login form.
        /// Populates the combo box with usernames from the database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Login_Load(object sender, EventArgs e)
        {
            foreach (User user in Program.dbContext.Users)
            {
                comboBox1.Items.Add(user.Username);
            }
        }

        /// <summary>
        /// Reloads the combo box with updated usernames from the database.
        /// </summary>
        public void Reload()
        {
            comboBox1.Items.Clear();
            foreach (User user in Program.dbContext.Users)
            {
                comboBox1.Items.Add(user.Username);
            }
        }

        /// <summary>
        /// Handles the click event for the manage users button.
        /// Opens the users management form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            new Users().Show();
        }
    }
}
