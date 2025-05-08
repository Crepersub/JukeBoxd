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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
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
            AddMainButton.BackColor = Color.Transparent;
            AddMainButton.BackgroundImage = Properties.Resources.button1;
            AddMainButton.BackgroundImageLayout = ImageLayout.Stretch;
            AddMainButton.FlatStyle = FlatStyle.Flat;
            AddMainButton.Font = new Font("Microsoft PhagsPa", 12F);
            AddMainButton.Location = new Point(40, 46);
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
            MainDataGridView.Location = new Point(40, 120);
            MainDataGridView.MultiSelect = false;
            MainDataGridView.Name = "MainDataGridView";
            MainDataGridView.ReadOnly = true;
            MainDataGridView.RowHeadersWidth = 51;
            MainDataGridView.Size = new Size(631, 281);
            MainDataGridView.TabIndex = 10;
            MainDataGridView.CellClick += MainDataGridView_CellClick_1;
            // 
            // UpdateMainButton
            // 
            UpdateMainButton.BackColor = Color.Transparent;
            UpdateMainButton.BackgroundImage = Properties.Resources.button1;
            UpdateMainButton.BackgroundImageLayout = ImageLayout.Stretch;
            UpdateMainButton.FlatStyle = FlatStyle.Flat;
            UpdateMainButton.Font = new Font("Microsoft PhagsPa", 12F);
            UpdateMainButton.Location = new Point(211, 46);
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
            DeleteMainButton.Location = new Point(363, 46);
            DeleteMainButton.Margin = new Padding(0);
            DeleteMainButton.Name = "DeleteMainButton";
            DeleteMainButton.Size = new Size(137, 48);
            DeleteMainButton.TabIndex = 4;
            DeleteMainButton.Text = "Delete";
            DeleteMainButton.UseVisualStyleBackColor = false;
            DeleteMainButton.Click += DeleteButton_Click;
            // 
            // AlbumCoverPictureBox
            // 
            AlbumCoverPictureBox.Location = new Point(738, 33);
            AlbumCoverPictureBox.Name = "AlbumCoverPictureBox";
            AlbumCoverPictureBox.Size = new Size(221, 250);
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
            PreviewButton.Location = new Point(525, 46);
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
            ReviewLabel.Location = new Point(738, 306);
            ReviewLabel.Name = "ReviewLabel";
            ReviewLabel.Size = new Size(221, 95);
            ReviewLabel.TabIndex = 9;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.main4;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(986, 448);
            Controls.Add(ReviewLabel);
            Controls.Add(PreviewButton);
            Controls.Add(AlbumCoverPictureBox);
            Controls.Add(DeleteMainButton);
            Controls.Add(UpdateMainButton);
            Controls.Add(MainDataGridView);
            Controls.Add(AddMainButton);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private BindingSource entryBindingSource;
        private PictureBox AlbumCoverPictureBox;
        private Label ReviewLabel;
        public DataGridView MainDataGridView;
        public Button AddMainButton;
        public Button UpdateMainButton;
        public Button DeleteMainButton;
        public Button PreviewButton;
    }
}