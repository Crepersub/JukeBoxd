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
using SpotifyAPI.Web;

namespace JukeBoxd.Forms
{
    public partial class Add : Form
    {
        /// <summary>
        /// Counter to track the number of text updates in the ComboBox.
        /// </summary>
        static int counter = 0;
        private PictureBox[] stars;
        private float rating = 0;
        bool[] isclicked = new bool[10];
        public event EventHandler SongAdded;

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> form.
        /// </summary>
        public Add()
        {
            InitializeComponent();
            stars = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };

            for (int i = 0; i < stars.Length; i++)
            {
                int index = i;
                stars[i].Click += (s, e) =>
                {
                    rating = (index + 1) / 2f;
                    HighlightStars(index + 1);
                    SetAsClicked(index + 1);
                };
                stars[i].MouseEnter += (s, e) => HighlightStars(index + 1);
                stars[i].MouseLeave += (s, e) => EmptyAfter(index + 1);
            }
        }
        //causes a crash because of invalid image path(commented out) - FIX - IT BROKEN!!!!!!!!!
        private void SetAsClicked(int index)
        {
            for (int i = 0; i < index; i++)
            {
                isclicked[i] = true;
            }
        }
        private void EmptyAfter(int count)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i % 2 == 0 && !isclicked[i])
                {
                    stars[i].Image = Properties.Resources.Untitled_2;
                }
                else if (!isclicked[i])
                {
                    stars[i].Image = Properties.Resources.Untitled_1;
                }
            }
        }

        private void HighlightStars(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0 && !isclicked[i])
                {
                    stars[i].Image = Properties.Resources.FilledStarLeft;
                }
                else if (!isclicked[i])
                {
                    stars[i].Image = Properties.Resources.FilledStarRight;
                }
            }
        }

        /// <summary>
        /// Handles the TextUpdate event of the ComboBox. 
        /// Performs a search on Spotify every third update and updates the ComboBox items with the search results.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            if (counter % 3 == 0)
            {
                // Save the current text and cursor position
                string currentText = comboBox1.Text;
                int cursorPosition = comboBox1.SelectionStart;

                comboBox1.Items.Clear();

                // Perform a Spotify search with the current text
                var searchResults = EntryMid.SpotifySearch(currentText);
                if (searchResults != null)
                {
                    foreach (var track in searchResults)
                    {
                        comboBox1.Items.Add(track);
                    }
                }

                // Restore the text and cursor position
                comboBox1.Text = currentText;
                comboBox1.SelectionStart = cursorPosition;
            }
            counter++;
        }

        /// <summary>
        /// Handles the Click event of the button. 
        /// Creates a new entry with the selected track, user ID, and rating, then saves it and updates the main form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is not null)
            {
                // Create a new entry with the selected track, user ID, and rating
                Entry entrytosave = new Entry(comboBox1.SelectedItem as FullTrack, Program.CurrentUser.Id, rating);

                // Save the entry and update the main form
                EntryMid.AddEntry(entrytosave);
                SongAdded?.Invoke(this, EventArgs.Empty);
                this.Close();
                
            }
        }
    }
}
