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

namespace JukeBoxd.Forms
{
    public partial class Delete : Form
    {
        public event EventHandler SongDeleted;
        public Delete()
        {
            InitializeComponent();
            comboBox1.DataSource = UserMid.GetUsersEntries(Program.CurrentUser.Id);
            this.BackColor = Color.FromArgb(230, 218, 206);
            button1.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button1.FlatAppearance.BorderSize = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EntryMid.RemoveEntry((comboBox1.SelectedItem as Entry).Id);
            SongDeleted?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
