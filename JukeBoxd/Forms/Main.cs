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
    public partial class Main : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(230, 218, 206);
            button1.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button1.FlatAppearance.BorderSize = 3;
            button2.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button2.FlatAppearance.BorderSize = 3;
            button3.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button3.FlatAppearance.BorderSize = 3;
            dataGridView1.BackgroundColor = Color.FromArgb(230, 218, 206);
            textBox1.BackColor = Color.FromArgb(230, 218, 206);
        }
        BindingSource source = new BindingSource();
        /// <summary>
        /// Handles the Load event of the Main form.
        /// Sets the data source of the DataGridView to the current user's entries.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            var entries = new BindingList<Entry>(UserMid.GetUsersEntries(Program.CurrentUser.Id));
            source = new BindingSource(entries, null);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[7].Visible = false;
        }

        /// <summary>
        /// Handles the Click event of button1.
        /// Opens the Add form to allow the user to add a new entry.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            addForm.SongAdded += (s, args) => UpdateDataGridView();
            addForm.ShowDialog();
        }

        /// <summary>
        /// Updates the DataGridView with the latest entries for the current user.
        /// This method is used to refresh the data in real-time after adding a new entry.
        /// </summary>
        /// <param name="entry">The new <see cref="Entry"/> to be added.</param>
        public void UpdateDataGridView()
        {

            var entries = new BindingList<Entry>(UserMid.GetUsersEntries(Program.CurrentUser.Id));
            source = new BindingSource(entries, null);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = source;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Update update = new Update();
            update.SongUpdated += (s, args) => UpdateDataGridView();
            update.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Delete deleteForm = new Delete();
            deleteForm.SongDeleted += (s, args) => UpdateDataGridView();
            deleteForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
