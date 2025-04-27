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
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
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
            if (dataGridView1.SelectedRows == null)
            {
                MessageBox.Show("Please select a row from the table.", "Update", MessageBoxButtons.OK);
            }
            var entries = new BindingList<Entry>(UserMid.GetUsersEntries(Program.CurrentUser.Id));
            source = new BindingSource(entries, null);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = source;
            dataGridView1_CellClick(null, new DataGridViewCellEventArgs(0, 0));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    throw new InvalidOperationException("No row selected.");
                }
                Update update = new Update(dataGridView1.CurrentRow.Cells[2].Value.ToString(),//title 
                dataGridView1.CurrentRow.Cells[3].Value.ToString(), //author
                DateOnly.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()), //date
                (int)dataGridView1.CurrentRow.Cells[0].Value,
                dataGridView1.CurrentRow.Cells[7].Value.ToString()); //id
                update.SongUpdated += (s, args) => UpdateDataGridView();
                update.Show();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please select a song from the table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    throw new InvalidOperationException("No row selected.");
                }
                EntryMid.RemoveEntry((int)dataGridView1.CurrentRow.Cells[0].Value);
                UpdateDataGridView();
            }
            catch (InvalidOperationException) 
            {
                MessageBox.Show("Please select a song from the table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            if (row.Cells[7].Value != null)
            {
                if (row.Cells[8].Value.ToString() != string.Empty)
                {
                    textBox1.Text = row.Cells[7].Value.ToString();
                    var track = Program.spotify.Tracks.Get(row.Cells[8].Value.ToString()).Result.Album.Images[0].Url;
                    pictureBox1.Load(track);
                }
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            var selectedTrack = Program.spotify.Tracks.Get(row.Cells[8].Value.ToString()).Result;
            var deezerTrack = DeezerClient.SearchTrackISRC(selectedTrack);//selectedTrack.ExternalIds["isrc"]);
            if (deezerTrack is not null)
            {
                await DeezerClient.PlayPreviewAsync(deezerTrack.Result.PreviewURL);
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
