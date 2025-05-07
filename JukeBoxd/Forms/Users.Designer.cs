namespace JukeBoxd.Forms
{
    partial class Users
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UsersListBox = new ListBox();
            UsernameLabel = new Label();
            UsernameTextBox = new TextBox();
            UpdateButton = new Button();
            DeleteButton = new Button();
            AddUsersButton = new Button();
            SuspendLayout();
            // 
            // UsersListBox
            // 
            UsersListBox.BackColor = Color.DarkSeaGreen;
            UsersListBox.BorderStyle = BorderStyle.FixedSingle;
            UsersListBox.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UsersListBox.FormattingEnabled = true;
            UsersListBox.Location = new Point(441, 20);
            UsersListBox.Name = "UsersListBox";
            UsersListBox.Size = new Size(266, 314);
            UsersListBox.TabIndex = 0;
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.BackColor = Color.Transparent;
            UsernameLabel.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UsernameLabel.Location = new Point(34, 236);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(103, 26);
            UsernameLabel.TabIndex = 1;
            UsernameLabel.Text = "Username:";
            UsernameLabel.Visible = false;
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UsernameTextBox.Location = new Point(34, 281);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(274, 33);
            UsernameTextBox.TabIndex = 2;
            UsernameTextBox.Visible = false;
            UsernameTextBox.KeyDown += textBox1_KeyDown;
            // 
            // UpdateButton
            // 
            UpdateButton.BackColor = Color.Transparent;
            UpdateButton.BackgroundImage = Properties.Resources.button1;
            UpdateButton.BackgroundImageLayout = ImageLayout.Stretch;
            UpdateButton.FlatStyle = FlatStyle.Flat;
            UpdateButton.Font = new Font("Microsoft PhagsPa", 12F);
            UpdateButton.Location = new Point(34, 100);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(113, 39);
            UpdateButton.TabIndex = 3;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = false;
            UpdateButton.Click += UpdateUserButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.BackColor = Color.Transparent;
            DeleteButton.BackgroundImage = Properties.Resources.button1;
            DeleteButton.BackgroundImageLayout = ImageLayout.Stretch;
            DeleteButton.FlatStyle = FlatStyle.Flat;
            DeleteButton.Font = new Font("Microsoft PhagsPa", 12F);
            DeleteButton.Location = new Point(34, 162);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(113, 39);
            DeleteButton.TabIndex = 4;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = false;
            DeleteButton.Click += button2_Click;
            // 
            // AddUsersButton
            // 
            AddUsersButton.BackColor = Color.Transparent;
            AddUsersButton.BackgroundImage = Properties.Resources.button1;
            AddUsersButton.BackgroundImageLayout = ImageLayout.Stretch;
            AddUsersButton.FlatStyle = FlatStyle.Flat;
            AddUsersButton.Font = new Font("Microsoft PhagsPa", 12F);
            AddUsersButton.Location = new Point(34, 35);
            AddUsersButton.Name = "AddUsersButton";
            AddUsersButton.Size = new Size(113, 39);
            AddUsersButton.TabIndex = 5;
            AddUsersButton.Text = "Add";
            AddUsersButton.UseVisualStyleBackColor = false;
            AddUsersButton.Click += AddUserButton_Click;
            // 
            // Users
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.user21;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(735, 384);
            Controls.Add(AddUsersButton);
            Controls.Add(DeleteButton);
            Controls.Add(UpdateButton);
            Controls.Add(UsernameTextBox);
            Controls.Add(UsernameLabel);
            Controls.Add(UsersListBox);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Users";
            Padding = new Padding(2);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Users";
            FormClosed += Users_FormClosed;
            Load += Users_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox UsersListBox;
        private Label UsernameLabel;
        private TextBox UsernameTextBox;
        private Button UpdateButton;
        private Button DeleteButton;
        private Button AddUsersButton;
    }
}