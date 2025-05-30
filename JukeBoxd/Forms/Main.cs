﻿using System.ComponentModel;
using JukeBoxd.BusinessLayer;
using JukeBoxd.Models;
using SpotifyAPI.Web;

namespace JukeBoxd.Forms
{
    /// <summary>
    /// Represents the main form of the application.
    /// Provides functionality for managing user entries, including adding, updating, deleting, and previewing songs.
    /// </summary>
    public partial class Main : Form
    {
        private DiaryDbContext dbContext;
        private User CurrentUser;
        private SpotifyClient spotify;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// Sets up the UI components and their initial properties.
        /// </summary>
        /// <param name="dbContext">The database context for accessing entries and users.</param>
        /// <param name="currentUser">The currently logged-in user.</param>
        /// <param name="spotify">The Spotify client for interacting with Spotify API.</param>
        public Main(DiaryDbContext dbContext, User currentUser, SpotifyClient spotify)
        {
            InitializeComponent();

            AddMainButton.FlatAppearance.BorderSize = 0;
            UpdateMainButton.FlatAppearance.BorderSize = 0;
            DeleteMainButton.FlatAppearance.BorderSize = 0;
            PreviewButton.FlatAppearance.BorderSize = 0;
            MainDataGridView.BackgroundColor = Color.FromArgb(255, 233, 205);
            ReviewTextBox.BackColor = Color.FromArgb(255, 233, 205);
            this.dbContext = dbContext;
            this.CurrentUser = currentUser;
            this.spotify = spotify;
        }

        BindingSource source = new BindingSource();

        /// <summary>
        /// Handles the Load event of the Main form.
        /// Sets the data source of the DataGridView to the current user's entries.
        /// Configures the visibility of specific columns and loads the first entry's album cover and review.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void Main_Load(object sender, EventArgs e)
        {
            var entries = new BindingList<Entry>(UserMid.GetUsersEntries(CurrentUser!.Id, dbContext));
            source = new BindingSource(entries, null!);
            MainDataGridView.AutoGenerateColumns = true;
            MainDataGridView.DataSource = source;
            MainDataGridView.Columns[0].Visible = false;
            MainDataGridView.Columns[1].Visible = false;
            MainDataGridView.Columns[7].Visible = false;
            MainDataGridView.Columns[8].Visible = false;
            MainDataGridView.Columns[9].Visible = false;
            if (MainDataGridView.Rows.Count > 0)
            {
                MainDataGridView.Rows[0].Cells[2].Selected = true;
                var firstRow = MainDataGridView.Rows[0];
                if (firstRow.Cells[8].Value != null && !string.IsNullOrEmpty(firstRow.Cells[8].Value!.ToString()))
                {
                    var track = spotify.Tracks.Get(firstRow.Cells[8].Value!.ToString()!).Result.Album.Images[0].Url;
                    ReviewTextBox.Text = firstRow.Cells[7].Value!.ToString();
                    AlbumCoverPictureBox.Load(track);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the Add button.
        /// Opens the Add form to allow the user to add a new entry.
        /// Refreshes the DataGridView after a new entry is added.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void AddButton_Click(object sender, EventArgs e)
        {
            Add addForm = new(CurrentUser, spotify, dbContext);
            addForm.SongAdded += (s, args) => UpdateDataGridView();
            addForm.Show();
        }

        /// <summary>
        /// Updates the DataGridView with the latest entries for the current user.
        /// This method is used to refresh the data in real-time after adding or modifying an entry.
        /// </summary>
        public void UpdateDataGridView()
        {
            if (MainDataGridView.SelectedRows == null)
            {
                MessageBox.Show("Please select a row from the table.", "Update", MessageBoxButtons.OK);
            }
            var entries = new BindingList<Entry>(UserMid.GetUsersEntries(CurrentUser!.Id, dbContext));
            source = new BindingSource(entries, null!);
            MainDataGridView.AutoGenerateColumns = true;
            MainDataGridView.DataSource = source;
            MainDataGridView_CellClick(null!, new DataGridViewCellEventArgs(0, 0));
        }

        /// <summary>
        /// Handles the Click event of the Update button.
        /// Opens the Update form to allow the user to modify the selected entry.
        /// Refreshes the DataGridView after the entry is updated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDataGridView.CurrentRow == null)
                {
                    throw new InvalidOperationException("No row selected.");
                }
                Update update = new(MainDataGridView.CurrentRow.Cells[2].Value!.ToString()!, // title
                    MainDataGridView.CurrentRow.Cells[3].Value!.ToString()!, // author
                    DateOnly.Parse(MainDataGridView.CurrentRow.Cells[5].Value!.ToString()!), // date
                    (int)MainDataGridView.CurrentRow.Cells[0].Value!, // id
                    MainDataGridView.CurrentRow.Cells[7].Value!.ToString()!, Program.dbContext); // review
                update.SongUpdated += (s, args) => UpdateDataGridView();
                update.Show();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please select a song from the table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the Delete button.
        /// Deletes the selected entry from the database and refreshes the DataGridView.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDataGridView.CurrentRow is not null)
                {
                    DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{MainDataGridView.CurrentRow.Cells[0].ToString()}'?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
                    if (result == DialogResult.Yes)
                    {
                        EntryMid.RemoveEntry((int)MainDataGridView.CurrentRow.Cells[0].Value!, Program.dbContext);
                UpdateDataGridView();
                    }
                }
                if (MainDataGridView.CurrentRow == null)
                {
                    throw new InvalidOperationException("No row selected.");
                }
                
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please select a song from the table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the DataGridView.
        /// Updates the album cover and review label based on the selected row.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MainDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                var row = MainDataGridView.Rows[e.RowIndex];
                if (row.Cells[7].Value != null && MainDataGridView.Rows.Count > 0)
                {
                    if (row.Cells[8].Value!.ToString() != string.Empty)
                    {
                        ReviewTextBox.Text = row.Cells[7].Value!.ToString();
                        var track = spotify.Tracks.Get(row.Cells[8].Value!.ToString()!).Result.Album.Images[0].Url;
                        AlbumCoverPictureBox.Load(track);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the Preview button.
        /// Plays a preview of the selected song using the Deezer API.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public async void PreviewButton_Click(object sender, EventArgs e)
        {
            var row = MainDataGridView.CurrentRow!;
            if (row.Cells[8].Value != null)
            {
                var selectedTrack = spotify.Tracks.Get(row.Cells[8].Value!.ToString()!).Result;
                var deezerTrack = DeezerClient.SearchTrackISRC(selectedTrack);
                if (deezerTrack is not null)
                {
                    await DeezerClient.PlayPreviewAsync(deezerTrack.PreviewURL!);
                }
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the Main form.
        /// Exits the application when the form is closed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        public void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Handles the CellClick event of the DataGridView.
        /// Updates the album cover and review label based on the selected row.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public void MainDataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = MainDataGridView.Rows[e.RowIndex];
                if (row.Cells[7].Value != null && MainDataGridView.Rows.Count > 0)
                {
                    if (row.Cells[8].Value!.ToString() != string.Empty)
                    {
                        ReviewTextBox.Text = row.Cells[7].Value!.ToString();
                        var track = spotify.Tracks.Get(row.Cells[8].Value!.ToString()!).Result.Album.Images[0].Url;
                        AlbumCoverPictureBox.Load(track);
                    }
                }
            }
        }
    }
}
