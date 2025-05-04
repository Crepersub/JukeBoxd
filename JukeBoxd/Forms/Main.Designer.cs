namespace JukeBoxd.Forms
{
    partial class Main
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
            components = new System.ComponentModel.Container();
            AddMainButton = new Button();
            MainDataGridView = new DataGridView();
            entryBindingSource = new BindingSource(components);
            UpdateMainButton = new Button();
            DeleteMainButton = new Button();
            AlbumCoverPictureBox = new PictureBox();
            PreviewButton = new Button();
            ReviewLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)MainDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)entryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AlbumCoverPictureBox).BeginInit();
            SuspendLayout();
            // 
            // AddMainButton
            // 
            AddMainButton.BackgroundImage = Properties.Resources.button1;
            AddMainButton.BackgroundImageLayout = ImageLayout.Stretch;
            AddMainButton.FlatStyle = FlatStyle.Flat;
            AddMainButton.Font = new Font("Microsoft PhagsPa", 12F);
            AddMainButton.Location = new Point(14, 15);
            AddMainButton.Margin = new Padding(0);
            AddMainButton.Name = "AddMainButton";
            AddMainButton.Size = new Size(146, 48);
            AddMainButton.TabIndex = 1;
            AddMainButton.Text = "Add ";
            AddMainButton.UseVisualStyleBackColor = false;
            AddMainButton.Click += AddButton_Click;
            // 
            // MainDataGridView
            // 
            
            MainDataGridView.AllowUserToAddRows = false;
            MainDataGridView.AutoGenerateColumns = false;
            MainDataGridView.BorderStyle = BorderStyle.None;
            MainDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MainDataGridView.DataSource = entryBindingSource;
            MainDataGridView.GridColor = Color.Black;
            MainDataGridView.Location = new Point(14, 83);
            MainDataGridView.MultiSelect = false;
            MainDataGridView.Name = "MainDataGridView";
            MainDataGridView.ReadOnly = true;
            MainDataGridView.RowHeadersWidth = 51;
            MainDataGridView.Size = new Size(631, 281);
            // 
            // UpdateMainButton
            // 
            UpdateMainButton.BackColor = Color.Transparent;
            UpdateMainButton.BackgroundImage = Properties.Resources.button1;
            UpdateMainButton.BackgroundImageLayout = ImageLayout.Stretch;
            UpdateMainButton.FlatStyle = FlatStyle.Flat;
            UpdateMainButton.Font = new Font("Microsoft PhagsPa", 12F);
            UpdateMainButton.Location = new Point(191, 15);
            UpdateMainButton.Margin = new Padding(0);
            UpdateMainButton.Name = "UpdateMainButton";
            UpdateMainButton.Size = new Size(131, 48);
            UpdateMainButton.TabIndex = 3;
            UpdateMainButton.Text = "Update ";
            UpdateMainButton.UseVisualStyleBackColor = false;
            UpdateMainButton.Click += UpdateButton_Click;
            // 
            // DeleteMainButton
            // 
            DeleteMainButton.BackColor = Color.Transparent;
            DeleteMainButton.BackgroundImage = Properties.Resources.button1;
            DeleteMainButton.BackgroundImageLayout = ImageLayout.Stretch;
            DeleteMainButton.FlatStyle = FlatStyle.Flat;
            DeleteMainButton.Font = new Font("Microsoft PhagsPa", 12F);
            DeleteMainButton.Location = new Point(366, 15);
            DeleteMainButton.Margin = new Padding(0);
            DeleteMainButton.Name = "DeleteMainButton";
            DeleteMainButton.Size = new Size(137, 48);
            DeleteMainButton.TabIndex = 4;
            DeleteMainButton.Text = "Delete";
            DeleteMainButton.UseVisualStyleBackColor = false;
            DeleteMainButton.Click += DeleteButton_Click;

            /* textBox1.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
             textBox1.Location = new Point(854, 227);
             textBox1.Multiline = true;
             textBox1.Name = "textBox1";
             textBox1.ReadOnly = true;
             textBox1.Size = new Size(178, 112);
             textBox1.TabIndex = 6;*/

            // 
            // AlbumCoverPictureBox
            // 
            AlbumCoverPictureBox.Location = new Point(728, 15);
            AlbumCoverPictureBox.Name = "AlbumCoverPictureBox";
            AlbumCoverPictureBox.Size = new Size(250, 250);
            AlbumCoverPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            AlbumCoverPictureBox.TabIndex = 5;
            AlbumCoverPictureBox.TabStop = false;
            // 
            // PreviewButton
            // 
            PreviewButton.BackColor = Color.Transparent;
            PreviewButton.BackgroundImage = Properties.Resources.button1;
            PreviewButton.BackgroundImageLayout = ImageLayout.Stretch;
            PreviewButton.FlatStyle = FlatStyle.Flat;
            PreviewButton.Font = new Font("Microsoft PhagsPa", 12F);
            PreviewButton.Location = new Point(533, 15);
            PreviewButton.Margin = new Padding(0);
            PreviewButton.Name = "PreviewButton";
            PreviewButton.Size = new Size(146, 48);
            PreviewButton.TabIndex = 8;
            PreviewButton.Text = "Preview song";
            PreviewButton.UseVisualStyleBackColor = false;
            PreviewButton.Click += PreviewButton_Click;
            // 
            // ReviewLabel
            // 
            ReviewLabel.BorderStyle = BorderStyle.FixedSingle;
            ReviewLabel.FlatStyle = FlatStyle.Flat;
            ReviewLabel.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ReviewLabel.Location = new Point(728, 269);
            ReviewLabel.Name = "ReviewLabel";
            ReviewLabel.Size = new Size(250, 95);
            ReviewLabel.TabIndex = 9;

            button5.BackColor = Color.Transparent;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Microsoft PhagsPa", 12F);
            button5.Location = new Point(29, 114);
            button5.Margin = new Padding(0);
            button5.Name = "button5";
            button5.Size = new Size(146, 48);
            button5.TabIndex = 8;
            button5.Text = "Preview song";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.main3;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1079, 448);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(MainDataGridView);
            Controls.Add(button1);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "JukeBoxd";
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)MainDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)entryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)AlbumCoverPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button AddMainButton;
        private DataGridView MainDataGridView;
        private BindingSource entryBindingSource;
        private Button UpdateMainButton;
        private Button DeleteMainButton;
        private PictureBox AlbumCoverPictureBox;
        private Button PreviewButton;
        private Label ReviewLabel;
    }
}