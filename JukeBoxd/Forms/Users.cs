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
            //Icon = Program.Icon!;
 
        }

        /// <summary>
        /// Handles the Add User button click event.
        /// Prepares the form for adding a new user.
        /// </summary>
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            UsernameLabel.Show();
            UsernameTextBox.Clear();
            UsernameTextBox.Show();
            currentmode = "add";
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
                    UserMid.AddUser(UsernameTextBox.Text, Program.dbContext);
                }
                else if (currentmode == "modify" && UsersListBox.SelectedItem is not null)
                {
                    UserMid.ChangeUsername(UsersListBox.SelectedItem.ToString()!, UsernameTextBox.Text,Program.dbContext);
                }
                Reload();
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
            UsernameLabel.Show();
            UsernameTextBox.Clear();
            UsernameTextBox.Show();
            currentmode = "modify";
        }

        /// <summary>
        /// Handles the Delete User button click event.
        /// Removes the selected user from the database.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (UsersListBox.SelectedItem is not null)
            {
                UserMid.RemoveUser(UsersListBox.SelectedItem.ToString()!, Program.dbContext);
                Reload();
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
