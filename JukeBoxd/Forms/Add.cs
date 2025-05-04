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
        private bool isRatingSet = false;
        private int selectedCount = 0;
        public event EventHandler SongAdded;
        private Dictionary<PictureBox, Point> originalPositions = new Dictionary<PictureBox, Point>();
        private System.Windows.Forms.Timer animationTimer;
        private PictureBox hoveredStar;
        private int animationStep = 0;
        private bool goingUp = true;
        private List<PictureBox> jumpingStars = new List<PictureBox>();
        private System.Windows.Forms.Timer groupJumpTimer;
        private int groupAnimationStep = 0;
        private bool groupGoingUp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> form.
        /// </summary>
        public Add()
        {
            InitializeComponent();

            this.BackgroundImage = Properties.Resources.add2;
            this.BackColor = Color.FromArgb(230, 218, 206);
            //button1.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button1.FlatAppearance.BorderSize = 0;
            textBox1.BackColor = Color.FromArgb(224, 224, 224);
            comboBox1.BackColor = Color.FromArgb(224, 224, 224);
            dateTimePicker1.CalendarMonthBackground = Color.FromArgb(224, 224, 224);
            dateTimePicker1.CalendarTitleBackColor = Color.FromArgb(224, 224, 224);

            stars = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5,
                                pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };

            foreach (var star in stars)
            {
                originalPositions[star] = star.Location;
            }

            for (int i = 0; i < stars.Length; i++)
            {
                int index = i;
                stars[i].MouseEnter += (s, e) =>
                {
                    HighlightStars(index + 1);
                    hoveredStar = stars[index];
                    animationStep = 0;
                    goingUp = true;
                    StartStarAnimation();
                };

                    stars[i].Click += (s, e) =>
                {
                    rating = (index + 1) / 2f;
                    selectedCount = index + 1;
                    SetRating(index+1);
                    isRatingSet = true;
                    HighlightStars(selectedCount);
                    label3.Text = $"{rating}";
                    StartGroupJump();
                };
                stars[i].MouseLeave += (s, e) =>
                {
                    if (!isRatingSet) ResetStars();
                    else 
                        HighlightStars((int)rating*2);
                    HighlightStars(selectedCount);
                    if (hoveredStar != null)
                    {
                        hoveredStar.Location = originalPositions[hoveredStar];
                        hoveredStar = null;
                        animationTimer?.Stop();
                    }
                };
            }
        }
        private void StartStarAnimation()
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
            }

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 20; // Faster animation, 20 ms tick
            animationTimer.Tick += (s, e) =>
            {
                if (hoveredStar == null)
                    return;

                var originalLocation = originalPositions[hoveredStar];
                int jumpHeight = 5; // pixels up

                if (goingUp)
                {
                    hoveredStar.Location = new Point(originalLocation.X, originalLocation.Y - animationStep);
                    animationStep++;

                    if (animationStep >= jumpHeight)
                    {
                        goingUp = false;
                    }
                }
                else
                {
                    hoveredStar.Location = new Point(originalLocation.X, originalLocation.Y - (jumpHeight - animationStep));
                    animationStep--;

                    if (animationStep <= 0)
                    {
                        hoveredStar.Location = originalLocation;
                        animationTimer.Stop();
                    }
                }
            };
            animationTimer.Start();
        }
        private void StartGroupJump()
        {
            if (groupJumpTimer != null)
            {
                groupJumpTimer.Stop();
                groupJumpTimer.Dispose();
            }

            // Collect all filled stars
            jumpingStars.Clear();
            for (int i = 0; i < selectedCount; i++)
            {
                jumpingStars.Add(stars[i]);
            }

            groupAnimationStep = 0;
            groupGoingUp = true;

            groupJumpTimer = new System.Windows.Forms.Timer();
            groupJumpTimer.Interval = 20; // Fast animation
            groupJumpTimer.Tick += (s, e) =>
            {
                foreach (var star in jumpingStars)
                {
                    var originalLocation = originalPositions[star];
                    int jumpHeight = 5;

                    if (groupGoingUp)
                    {
                        star.Location = new Point(originalLocation.X, originalLocation.Y - groupAnimationStep);
                    }
                    else
                    {
                        star.Location = new Point(originalLocation.X, originalLocation.Y - (jumpHeight - groupAnimationStep));
                    }
                }

                if (groupGoingUp)
                {
                    groupAnimationStep++;
                    if (groupAnimationStep >= 5)
                    {
                        groupGoingUp = false;
                    }
                }
                else
                {
                    groupAnimationStep--;
                    if (groupAnimationStep <= 0)
                    {
                        // Reset stars back to original position
                        foreach (var star in jumpingStars)
                        {
                            star.Location = originalPositions[star];
                        }
                        groupJumpTimer.Stop();
                    }
                }
            };

            groupJumpTimer.Start();
        }

        private void SetRating(int halfStarIndex)
        {
            rating = halfStarIndex * 0.5f; // 1 half-star = 0.5 rating
        }
        private void ResetStars()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i%2==0)
                {
                    stars[i].Image = Properties.Resources.newEStar2;
                }
                else
                    stars[i].Image= Properties.Resources.newEStar1;
            }
        }
        private void HighlightStars(int count)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i < count)
                {
                    if (i%2==0)
                        stars[i].Image = Properties.Resources.newFStar2;  // filled half-star left

                    else
                        stars[i].Image=Properties.Resources.newFStar1; // filled half-star right
                }
                else
                {
                    if (i%2==0)
                        stars[i].Image = Properties.Resources.newEStar2; // empty half-star left
                    else
                        stars[i].Image = Properties.Resources.newEStar1; // empty half-star right
                }
                }
        }

        /// <summary>
        /// Handles the TextUpdate event of the ComboBox. 
        /// Performs a search on Spotify every third update and updates the ComboBox items with the search results.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private System.Timers.Timer typingTimer;

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            if (typingTimer == null)
            {
                typingTimer = new System.Timers.Timer(500); // 500ms delay
                typingTimer.Elapsed += OnTypingTimerElapsed;
                typingTimer.AutoReset = false;
            }

            typingTimer.Stop();
            typingTimer.Start();
        }

        private void OnTypingTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => PerformSearch()));
            }
            else
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            if (comboBox1.Text != string.Empty)
            {
                // Save the current text and cursor position
                string currentText = comboBox1.Text;
                int cursorPosition = comboBox1.SelectionStart;

                // Temporarily store items before clearing the ComboBox
                var itemsToAdd = new List<FullTrackWithString>();

                // Perform a Spotify search with the current text
                var searchResults = EntryMid.SpotifySearch(currentText);
                if (searchResults != null)
                {
                    foreach (var track in searchResults)
                    {
                        itemsToAdd.Add(track);  // Collect the search results
                    }
                }

                // Clear items and then add all at once
                comboBox1.Items.Clear();
                foreach (var item in itemsToAdd)
                {
                    comboBox1.Items.Add(item);
                }

                // Restore the text and cursor position
                comboBox1.Text = currentText;
                comboBox1.SelectionStart = cursorPosition;
            }
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
                Entry entrytosave = new Entry(comboBox1.SelectedItem as FullTrack, Program.CurrentUser.Id, rating, DateOnly.FromDateTime(dateTimePicker1.Value), textBox1.Text);

                // Save the entry and update the main form
                EntryMid.AddEntry(entrytosave);
                SongAdded?.Invoke(this, EventArgs.Empty);
                this.Close();

            }
            else MessageBox.Show("Select a song");
        }
    }
}
