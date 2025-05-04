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
            button1 = new Button();
            dataGridView1 = new DataGridView();
            entryBindingSource = new BindingSource(components);
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            button4 = new Button();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)entryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = Properties.Resources.button1;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft PhagsPa", 12F);
            button1.Location = new Point(29, 57);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(132, 48);
            button1.TabIndex = 1;
            button1.Text = "Add ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.DataSource = entryBindingSource;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.Location = new Point(29, 151);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(636, 236);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImage = Properties.Resources.button1;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft PhagsPa", 12F);
            button2.Location = new Point(189, 57);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(132, 48);
            button2.TabIndex = 3;
            button2.Text = "Update ";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.BackgroundImage = Properties.Resources.button1;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Microsoft PhagsPa", 12F);
            button3.Location = new Point(348, 57);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Size = new Size(132, 48);
            button3.TabIndex = 4;
            button3.Text = "Delete";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(742, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(178, 190);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(742, 227);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(178, 112);
            textBox1.TabIndex = 6;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BackgroundImage = Properties.Resources.button1;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Microsoft PhagsPa", 12F);
            button4.Location = new Point(761, 363);
            button4.Margin = new Padding(0);
            button4.Name = "button4";
            button4.Size = new Size(132, 48);
            button4.TabIndex = 7;
            button4.Text = "Close";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.BackgroundImage = Properties.Resources.button1;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Microsoft PhagsPa", 12F);
            button5.Location = new Point(507, 57);
            button5.Margin = new Padding(0);
            button5.Name = "button5";
            button5.Size = new Size(158, 48);
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
            ClientSize = new Size(952, 438);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            DoubleBuffered = true;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)entryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private DataGridView dataGridView1;
        private BindingSource entryBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn authorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn entryDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ratingDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn userDataGridViewTextBoxColumn;
        private Button button2;
        private Button button3;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Button button4;
        private Button button5;
    }
}