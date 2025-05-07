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

namespace JukeBoxd.Forms
{
    public partial class Update : Form
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
        /// Event triggered when a song is updated.
        /// </summary>
        public event EventHandler? SongUpdated;

        /// <summary>
        /// Indicates whether the rating has been set by the user.
        /// </summary>
        private bool isRatingSet = false;

        /// <summary>
        /// The number of stars currently selected by the user.
        /// </summary>
        private int selectedCount = 0;

        /// <summary>
        /// The ID of the selected song entry being updated.
        /// </summary>
        private int selectedID = 0;

        /// <summary>
        /// Dictionary storing the original positions of the stars for animation purposes.
        /// </summary>
        private Dictionary<PictureBox, Point> originalPositions = new Dictionary<PictureBox, Point>();

        /// <summary>
        /// Timer used for animating the hover effect on a single star.
        /// </summary>
        private System.Windows.Forms.Timer? animationTimer;

        /// <summary>
        /// The star currently being hovered over by the user.
        /// </summary>
        private PictureBox? hoveredStar;

        /// <summary>
        /// The current step of the animation for the hovered star.
        /// </summary>
        private int animationStep = 0;

        /// <summary>
        /// Indicates whether the hovered star is moving up in the animation.
        /// </summary>
        private bool goingUp = true;

        /// <summary>
        /// List of stars that are currently "jumping" in the group animation.
        /// </summary>
        private List<PictureBox> jumpingStars = new List<PictureBox>();

        /// <summary>
        /// Timer used for animating the group jump effect for selected stars.
        /// </summary>
        private System.Windows.Forms.Timer? groupJumpTimer;
        /// <summary>
        /// The current step of the group animation for the selected stars.
        /// </summary>
        private int groupAnimationStep = 0;

        /// <summary>
        /// Indicates whether the group of stars is moving up in the group animation.
        /// </summary>
        private bool groupGoingUp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Update"/> form.
        /// </summary>
        /// <param name="title">The title of the song being updated.</param>
        /// <param name="author">The author of the song being updated.</param>
        /// <param name="date">The date associated with the song entry.</param>
        /// <param name="id">The unique identifier of the song entry being updated.</param>
        /// <param name="review">The review text for the song.</param>
        public Update(string title, string author, DateOnly date, int id, string review)
        {
            InitializeComponent();

            EditingTextBox.BackColor = Color.FromArgb(224, 224, 224);
            EditingTextBox.Text = $"{title} by {author}";
            ReviewTextBox.Text = review;
            ReviewTextBox.BackColor = Color.FromArgb(224, 224, 224);
            EntryDateTimePicker.BackColor = Color.FromArgb(224, 224, 224);
            stars = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };
            this.BackColor = Color.FromArgb(230, 218, 206);
            
            UpdateButton.FlatAppearance.BorderSize = 0;

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
                    SetRating(index + 1);
                    isRatingSet = true;
                    HighlightStars(selectedCount);
                    StartGroupJump();
                    label3.Text = $"{rating}";
                };
                stars[i].MouseLeave += (s, e) =>
                {
                    if (!isRatingSet) ResetStars();
                    else
                        HighlightStars((int)rating * 2);
                    HighlightStars(selectedCount);
                    if (hoveredStar != null)
                    {
                        hoveredStar.Location = originalPositions[hoveredStar];
                        hoveredStar = null;
                        animationTimer?.Stop();
                    }
                };
            }
            EntryDateTimePicker.Value = date.ToDateTime(new TimeOnly(0, 0));
            selectedID = id;
            EntryDateLabel.BackColor = Color.FromArgb(255, 233, 205);
            UpdateButton.FlatAppearance.BorderSize = 0;
        }

        /// <summary>
        /// Sets the rating based on the number of half-stars selected.
        /// </summary>
        /// <param name="halfStarIndex">The index of the half-star selected.</param>
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
                    stars[i].Image = Properties.Resources.newEStar1;
            }
        }
        /// <summary>
        /// Highlights the stars up to the specified count, changing their appearance to indicate selection.
        /// </summary>
        /// <param name="count">The number of stars to highlight.</param>
        private void HighlightStars(int count)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i < count)
                {
                    if (i % 2 == 0)
                    {
                        stars[i].Image = Properties.Resources.newFStar2;  // filled half-star left
                    }
                    else
                        stars[i].Image = Properties.Resources.newFStar1; // filled half-star right
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        stars[i].Image = Properties.Resources.newEStar2; // empty half-star left
                    }
                    else
                        stars[i].Image = Properties.Resources.newEStar1; // empty half-star right
                }
            }
        }
        /// <summary>
        /// Starts the animation for the star currently being hovered over, creating a "jumping" effect.
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
        /// Starts the group animation for all selected stars, creating a synchronized "jumping" effect.
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
        /// Handles the click event for the Update button, updating the song entry and closing the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            EntryMid.UpdateEntry(selectedID, rating, DateOnly.FromDateTime(EntryDateTimePicker.Value), ReviewTextBox.Text, Program.dbContext);
            SongUpdated?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void Update_Load(object sender, EventArgs e)
        {

        }
    }
}
