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
            listBox1 = new ListBox();
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.DarkSeaGreen;
            listBox1.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(453, 18);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(287, 342);
            listBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(44, 247);
            label1.Name = "label1";
            label1.Size = new Size(103, 26);
            label1.TabIndex = 1;
            label1.Text = "Username:";
            label1.Visible = false;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Microsoft PhagsPa", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(44, 290);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(337, 33);
            textBox1.TabIndex = 2;
            textBox1.Visible = false;
            textBox1.KeyDown += textBox1_KeyDown;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft PhagsPa", 12F);
            button1.Location = new Point(34, 100);
            button1.Name = "button1";
            button1.Size = new Size(113, 39);
            button1.TabIndex = 3;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft PhagsPa", 12F);
            button2.Location = new Point(34, 162);
            button2.Name = "button2";
            button2.Size = new Size(113, 39);
            button2.TabIndex = 4;
            button2.Text = "Delete";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Microsoft PhagsPa", 12F);
            button3.Location = new Point(34, 35);
            button3.Name = "button3";
            button3.Size = new Size(113, 39);
            button3.TabIndex = 5;
            button3.Text = "Add";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // Users
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 383);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
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

        private ListBox listBox1;
        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}