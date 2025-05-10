namespace JukeBoxd.Forms
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            label1 = new Label();
            LoginComboBox = new ComboBox();
            LoginButton = new Button();
            UsersButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Berlin Sans FB Demi", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(235, 152);
            label1.Name = "label1";
            label1.Size = new Size(289, 48);
            label1.TabIndex = 0;
            label1.Text = "Select a user:";
            // 
            // LoginComboBox
            // 
            LoginComboBox.BackColor = Color.Cornsilk;
            LoginComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LoginComboBox.Font = new Font("Microsoft PhagsPa", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoginComboBox.ForeColor = Color.Black;
            LoginComboBox.FormattingEnabled = true;
            LoginComboBox.ItemHeight = 29;
            LoginComboBox.Location = new Point(306, 215);
            LoginComboBox.Name = "LoginComboBox";
            LoginComboBox.Size = new Size(160, 37);
            LoginComboBox.TabIndex = 1;
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.Transparent;
            LoginButton.BackgroundImage = (Image)resources.GetObject("LoginButton.BackgroundImage");
            LoginButton.BackgroundImageLayout = ImageLayout.Zoom;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.Font = new Font("Microsoft PhagsPa", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoginButton.Location = new Point(306, 258);
            LoginButton.Margin = new Padding(0);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(160, 54);
            LoginButton.TabIndex = 2;
            LoginButton.Text = "Log in";
            LoginButton.UseVisualStyleBackColor = false;
            LoginButton.Click += LoginButton_Click;
            // 
            // UsersButton
            // 
            UsersButton.BackColor = Color.Transparent;
            UsersButton.BackgroundImage = Properties.Resources.button1;
            UsersButton.BackgroundImageLayout = ImageLayout.Stretch;
            UsersButton.FlatStyle = FlatStyle.Flat;
            UsersButton.Font = new Font("Microsoft PhagsPa", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UsersButton.Location = new Point(280, 322);
            UsersButton.Margin = new Padding(0);
            UsersButton.Name = "UsersButton";
            UsersButton.Size = new Size(203, 42);
            UsersButton.TabIndex = 3;
            UsersButton.Text = "Modify users";
            UsersButton.UseVisualStyleBackColor = false;
            UsersButton.Click += UsersButton_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.login2;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(747, 478);
            Controls.Add(UsersButton);
            Controls.Add(LoginButton);
            Controls.Add(LoginComboBox);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Login";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "JukeBoxd";
            Load += Login_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button LoginButton;
        private Button UsersButton;
        public ComboBox LoginComboBox;
    }
}
