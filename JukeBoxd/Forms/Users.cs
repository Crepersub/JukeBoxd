using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JukeBoxd.Models;
using Windows.System.RemoteSystems;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        static string currentmode = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="Users"/> class.
        /// Sets up the form's appearance and initializes components.
        /// </summary>
        public Users()
        {
            InitializeComponent();

        
            this.BackColor = Color.FromArgb(230, 218, 206);           
            UsernameTextBox.BackColor = Color.FromArgb(224, 224, 224);
            UsersListBox.BackColor = Color.FromArgb(224,224,224);
            UpdateButton.FlatAppearance.BorderSize = 0;
            DeleteButton.FlatAppearance.BorderSize = 0;
            AddUsersButton.FlatAppearance.BorderSize = 0;

        }

        /// <summary>
        /// Handles the Add User button click event.
        /// Prepares the form for adding a new user.
        /// </summary>
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            if (UsernameTextBox.Text != string.Empty)
            {
                UserMid.AddUser(UsernameTextBox.Text, Program.dbContext);
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
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (currentmode == "add")
                {
                    if (UsernameTextBox.Text != string.Empty)
                    {
                        UserMid.AddUser(UsernameTextBox.Text, Program.dbContext);
                    }
                }
                else if (currentmode == "modify" && UsersListBox.SelectedItem is not null)
                {
                    if (UsernameTextBox.Text != string.Empty)
                    {
                        UserMid.ChangeUsername(UsersListBox.SelectedItem.ToString()!, UsernameTextBox.Text, Program.dbContext);
                    }
                }
                Reload();
                currentmode = "";
            }
        }

        /// <summary>
        /// Reloads the user list and resets the form's state.
        /// </summary>
        private void Reload()
        {
            UsersListBox.Items.Clear();
            foreach (User user in Program.dbContext.Users)
            {
                UsersListBox.Items.Add(user.Username);
            }
            UsernameLabel.Hide();
            UsernameTextBox.Hide();
        }

        /// <summary>
        /// Handles the Update User button click event.
        /// Prepares the form for modifying an existing user.
        /// </summary>
        private void UpdateUserButton_Click(object sender, EventArgs e)
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
                    UserMid.ChangeUsername(selectedUsername, newUsername, Program.dbContext);
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
        /// Removes the selected user from the database.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
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
                    if (result==DialogResult.Yes)
                    {
                        UserMid.RemoveUser(UsersListBox.SelectedItem.ToString()!, Program.dbContext);
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
        private void Users_Load(object sender, EventArgs e)
        {
            foreach (User user in Program.dbContext.Users)
            {
                UsersListBox.Items.Add(user.Username);
            }
        }

        /// <summary>
        /// Handles the form's FormClosed event.
        /// Reloads the login form when the Users form is closed.
        /// </summary>
        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.login.Reload();
        }
    }
}
