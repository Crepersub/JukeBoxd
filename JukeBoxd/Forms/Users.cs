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
    public partial class Users : Form
    {
        static string currentmode = "";
        public Users()
        {
            InitializeComponent();

            this.BackgroundImage = Properties.Resources.user2;
            this.BackColor = Color.FromArgb(230, 218, 206);
            listBox1.BackColor = Color.FromArgb(224,224,224);
            textBox1.BackColor = Color.FromArgb(224, 224, 224);
            button1.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button3.FlatAppearance.BorderSize = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Show();
            textBox1.Clear();
            textBox1.Show();
            currentmode = "add";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter /*&& listBox1.SelectedItem is not null*/)
            {
                if (currentmode == "add")
                {
                    UserMid.AddUser(textBox1.Text);
                }
                else if (currentmode == "modify")
                {
                    UserMid.ChangeUsername(listBox1.SelectedItem.ToString()!, textBox1.Text);
                }
                Reload();
            }
        }
        private void Reload()
        {
            listBox1.Items.Clear();
            foreach (User user in Program.dbContext.Users)
            {
                listBox1.Items.Add(user.Username);
            }
            label1.Hide();
            textBox1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Show();
            textBox1.Clear();
            textBox1.Show();
            currentmode = "modify";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is not null)
            {
                UserMid.RemoveUser(listBox1.SelectedItem.ToString()!);
                Reload();
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            foreach (User user in Program.dbContext.Users)
            {
                listBox1.Items.Add(user.Username);
            }
        }

        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.login.Reload();
        }
    }
}
