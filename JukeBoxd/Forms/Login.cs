using JukeBoxd;
using JukeBoxd.Models;

namespace JukeBoxd.Forms
{
    public partial class Login : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// Sets up the UI elements and their properties.
        /// </summary>
        public Login()
        {
            InitializeComponent();

           
            LoginButton.FlatAppearance.BorderSize = 0;           
            UsersButton.FlatAppearance.BorderSize = 0;
            LoginComboBox.BackColor = Color.FromArgb(230, 218, 206);
            this.Size = new Size(660, 434);
            label1.Location = new Point(220,110);
            LoginComboBox.Location = new Point(255, 155);
            LoginButton.Location = new Point(255, 195);
            UsersButton.Location = new Point(235, 245);
            // this.Icon = Properties.Resources.logo2;
           // Program.Icon = Icon!;
        }

        /// <summary>
        /// Handles the click event for the login button. 
        /// Sets the current user based on the selected username and opens the main form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (LoginComboBox.SelectedItem is not null)
            {
                Program.CurrentUser = Program.dbContext.Users.Where(x => x.Username == LoginComboBox.SelectedItem.ToString()).FirstOrDefault()!;
                new Main().Show();
                Hide();
            }
            else MessageBox.Show("Select a user!", "No user selected");
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
                LoginComboBox.Items.Add(user.Username);
            }
        }

        /// <summary>
        /// Reloads the combo box with updated usernames from the database.
        /// Clears the existing items and repopulates the combo box.
        /// </summary>
        public void Reload()
        {
            LoginComboBox.Items.Clear();
            foreach (User user in Program.dbContext.Users)
            {
                LoginComboBox.Items.Add(user.Username);
            }
        }

        /// <summary>
        /// Handles the click event for the manage users button.
        /// Opens the users management form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void UsersButton_Click(object sender, EventArgs e)
        {
            new Users().Show();
        }
    }
}
