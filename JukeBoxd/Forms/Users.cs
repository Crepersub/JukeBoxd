using JukeBoxd.Models;

namespace JukeBoxd.Forms
{
    /// <summary>
    /// Represents the Users form, which allows managing user accounts.
    /// </summary>
    public partial class Users : Form
    {
        /// <summary>
        /// Stores the current mode of the form (e.g., "add" or "modify").
        /// </summary>
        private static string currentmode = "";

        private DiaryDbContext dbContext;
        private Login loginForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="Users"/> class.
        /// Sets up the form's appearance and initializes components.
        /// </summary>
        /// <param name="dbContext">The database context used for accessing user data.</param>
        /// <param name="loginform">The login form instance to reload upon closing this form.</param>
        public Users(DiaryDbContext dbContext, Login loginform)
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(230, 218, 206);
            UsernameTextBox.BackColor = Color.FromArgb(224, 224, 224);
            UsersListBox.BackColor = Color.FromArgb(224, 224, 224);
            UpdateButton.FlatAppearance.BorderSize = 0;
            DeleteButton.FlatAppearance.BorderSize = 0;
            AddUsersButton.FlatAppearance.BorderSize = 0;
            this.dbContext = dbContext;
            this.loginForm = loginform;
        }

        /// <summary>
        /// Handles the Add User button click event.
        /// Adds a new user to the database and updates the user list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void AddUserButton_Click(object sender, EventArgs e)
        {
            if (UsernameTextBox.Text != string.Empty)
            {
                UserMid.AddUser(UsernameTextBox.Text, dbContext);
                Reload();
            }
            UsernameLabel.Show();
            UsernameTextBox.Clear();
            UsernameTextBox.Show();
            currentmode = "add";
            UsernameLabel.Text = "Add new user";
        }

        /// <summary>
        /// Handles the KeyDown event for the username text box.
        /// Adds or modifies a user when the Enter key is pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (currentmode == "add")
                {
                    if (UsernameTextBox.Text != string.Empty)
                    {
                        UserMid.AddUser(UsernameTextBox.Text, dbContext);
                    }
                }
                else if (currentmode == "modify" && UsersListBox.SelectedItem is not null)
                {
                    if (UsernameTextBox.Text != string.Empty)
                    {
                        UserMid.ChangeUsername(UsersListBox.SelectedItem.ToString()!, UsernameTextBox.Text, dbContext);
                    }
                }
                Reload();
                currentmode = "";
            }
        }

        /// <summary>
        /// Reloads the user list and resets the form's state.
        /// </summary>
        public void Reload()
        {
            UsersListBox.Items.Clear();
            foreach (User user in dbContext.Users)
            {
                UsersListBox.Items.Add(user.Username);
            }
            UsernameLabel.Hide();
            UsernameTextBox.Hide();
        }

        /// <summary>
        /// Handles the Update User button click event.
        /// Prepares the form for modifying an existing user or updates the username.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void UpdateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentmode != "modify")
                {
                    if (UsersListBox.SelectedItem is not null)
                    {
                        UsernameTextBox.Text = UsersListBox.SelectedItem.ToString()!;
                        UsernameLabel.Text = "Edit username";
                        UsernameLabel.Show();
                        UsernameTextBox.Show();
                        currentmode = "modify";
                    }
                    else
                    {
                        MessageBox.Show("Please select a user to modify", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    string selectedUsername = UsersListBox.SelectedItem?.ToString() ?? "";
                    string newUsername = UsernameTextBox.Text.Trim();

                    if (string.IsNullOrEmpty(newUsername))
                    {
                        MessageBox.Show("Please enter a new username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    UserMid.ChangeUsername(selectedUsername, newUsername, dbContext);
                    Reload();
                    currentmode = "";
                    UsernameTextBox.Clear();
                    UsernameTextBox.Hide();
                    UsernameLabel.Hide();
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Delete User button click event.
        /// Removes the selected user from the database after confirmation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (UsersListBox.SelectedItem is not null)
                {
                    DialogResult result = MessageBox.Show(
                        $"Are you sure you want to delete '{UsersListBox.SelectedItem}'?",
                        "Confirm Deletion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );
                    if (result == DialogResult.Yes)
                    {
                        UserMid.RemoveUser(UsersListBox.SelectedItem.ToString()!, dbContext);
                        Reload();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a user to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the form's Load event.
        /// Populates the user list with data from the database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void Users_Load(object sender, EventArgs e)
        {
            foreach (User user in dbContext.Users)
            {
                UsersListBox.Items.Add(user.Username);
            }
        }

        /// <summary>
        /// Handles the form's FormClosed event.
        /// Reloads the login form when the Users form is closed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm.Reload();
        }
    }
}
