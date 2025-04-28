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
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
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
            // comboBox1
            // 
            comboBox1.BackColor = Color.Cornsilk;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Microsoft PhagsPa", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.Black;
            comboBox1.FormattingEnabled = true;
            comboBox1.ItemHeight = 29;
            comboBox1.Location = new Point(306, 215);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(160, 37);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft PhagsPa", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(306, 258);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(160, 54);
            button1.TabIndex = 2;
            button1.Text = "Log in";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImage = Properties.Resources.button1;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft PhagsPa", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(280, 322);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(203, 42);
            button2.TabIndex = 3;
            button2.Text = "Modify users";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(747, 478);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Login";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "JukeBoxd";
            Load += Login_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
    }
}
