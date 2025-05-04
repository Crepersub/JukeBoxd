using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JukeBoxd.BusinessLayer;
using JukeBoxd.Models;
using SpotifyAPI.Web;

namespace JukeBoxd.Forms
{
    public partial class Add : Form
    {
        /// <summary>
        /// Array of PictureBox controls representing the stars for rating.
        /// </summary>
        private PictureBox[] stars;

        /// <summary>
        /// The current rating value, calculated based on the selected stars.
        /// </summary>
        private float rating = 0;

        /// <summary>
        /// Indicates whether the rating has been set by the user.
        /// </summary>
        private bool isRatingSet = false;

        /// <summary>
        /// The number of stars currently selected by the user.
        /// </summary>
        private int selectedCount = 0;

        /// <summary>
        /// Event triggered when a song is successfully added.
        /// </summary>
        public event EventHandler? SongAdded;

        /// <summary>
        /// Dictionary storing the original positions of each star PictureBox.
        /// </summary>
        private Dictionary<PictureBox, Point> originalPositions = new Dictionary<PictureBox, Point>();

        /// <summary>
        /// Timer used for animating the bounce effect of a hovered star.
        /// </summary>
        private System.Windows.Forms.Timer? animationTimer;

        /// <summary>
        /// The PictureBox currently being hovered over by the user.
        /// </summary>
        private PictureBox? hoveredStar;

        /// <summary>
        /// Tracks the current step of the animation for the hovered star.
        /// </summary>
        private int animationStep = 0;

        /// <summary>
        /// Indicates whether the hovered star is moving up or down during the animation.
        /// </summary>
        private bool goingUp = true;

        /// <summary>
        /// List of PictureBox controls representing the stars involved in the group jump animation.
        /// </summary>
        private List<PictureBox> jumpingStars = new List<PictureBox>();

        /// <summary>
        /// Timer used for animating the group jump effect of selected stars.
        /// </summary>
        private System.Windows.Forms.Timer? groupJumpTimer;

        /// <summary>
        /// Tracks the current step of the group jump animation.
        /// </summary>
        private int groupAnimationStep = 0;

        /// <summary>
        /// Indicates whether the stars in the group jump animation are moving up or down.
        /// </summary>
        private bool groupGoingUp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> form.
        /// </summary>
        public Add()
        {
            InitializeComponent();

            AddButton.FlatAppearance.BorderColor = Color.FromArgb(255, 233, 205);
            AddButton.FlatAppearance.BorderSize = 0;

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
            Icon = Program.Icon;
        }

        /// <summary>
        /// Starts the animation for a single hovered star.
        /// The star "bounces" up and down to provide a visual effect.
        /// </summary>
        private void StartStarAnimation()
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
            }

            animationTimer = new();
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

        /// <summary>
        /// Starts the group jump animation for all selected stars.
        /// The stars "bounce" together to provide a visual effect.
        /// </summary>
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

            groupJumpTimer = new();
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

        /// <summary>
        /// Sets the rating value based on the number of half-stars selected.
        /// </summary>
        /// <param name="halfStarIndex">The index of the last selected half-star.</param>
        private void SetRating(int halfStarIndex)
        {
            rating = halfStarIndex * 0.5f; // 1 half-star = 0.5 rating
        }

        /// <summary>
        /// Resets all stars to their default (unselected) state.
        /// </summary>
        private void ResetStars()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i % 2 == 0)
                {
                    stars[i].Image = Properties.Resources.newEStar2;
                }
                else
                {
                    stars[i].Image = Properties.Resources.newEStar1;
                }
            }
        }
        /// <summary>
        /// Highlights the stars up to the specified count.
        /// Updates the star images to represent filled or empty states.
        /// </summary>
        /// <param name="count">The number of stars to highlight.</param>
        private void HighlightStars(int count)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i < count)
                {
                    if (i % 2 == 0)
                        stars[i].Image = Properties.Resources.newFStar2;  // filled half-star left
                    else
                        stars[i].Image = Properties.Resources.newFStar1;  // filled half-star right
                }
                else
                {
                    if (i % 2 == 0)
                        stars[i].Image = Properties.Resources.newEStar2;  // empty half-star left
                    else
                        stars[i].Image = Properties.Resources.newEStar1;  // empty half-star right
                }
            }
        }

        /// <summary>
        /// A timer used to delay the execution of the Spotify search operation.
        /// This prevents triggering the search too frequently while the user is typing in the ComboBox.
        /// </summary>
        private System.Timers.Timer? typingTimer;

        /// <summary>
        /// Handles the TextUpdate event of the SongComboBox.
        /// Starts a timer to delay the search operation, allowing the user to finish typing.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void SongComboBox1_TextUpdate(object sender, EventArgs e)
        {
            if (typingTimer == null)
            {
                typingTimer = new System.Timers.Timer(500); // 500ms delay
                typingTimer.Elapsed += OnTypingTimerElapsed!;
                typingTimer.AutoReset = false;
            }

            typingTimer.Stop();
            typingTimer.Start();
        }

        /// <summary>
        /// Handles the elapsed event of the typing timer.
        /// Invokes the PerformSearch method to update the ComboBox items with search results.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
            SongComboBox.DroppedDown = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// Performs a search on Spotify using the current text in the SongComboBox.
        /// Updates the ComboBox items with the search results.
        /// </summary>
        private void PerformSearch()
        {
            if (SongComboBox.Text != string.Empty)
            {
                // Save the current text and cursor position  
                string currentText = SongComboBox.Text;
                int cursorPosition = SongComboBox.SelectionStart;

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
                SongComboBox.Items.Clear();
                foreach (var item in itemsToAdd)
                {
                    SongComboBox.Items.Add(item);
                }

                // Restore the text and cursor position  
                SongComboBox.Text = currentText;
                SongComboBox.SelectionStart = cursorPosition;
            }
        }

        /// <summary>
        /// Handles the Click event of the Add button.
        /// Creates a new entry with the selected track, user ID, and rating, then saves it and updates the main form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void AddButton1_Click(object sender, EventArgs e)
        {
            if (SongComboBox.SelectedItem is not null)
            {
                // Create a new entry with the selected track, user ID, and rating
                Entry entrytosave = new(SongComboBox.SelectedItem as FullTrack, Program.CurrentUser!.Id, rating, DateOnly.FromDateTime(EntryDateTimePicker.Value), ReviewTextBox.Text);

                // Save the entry and update the main form
                EntryMid.AddEntry(entrytosave);
                SongAdded?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a song!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
