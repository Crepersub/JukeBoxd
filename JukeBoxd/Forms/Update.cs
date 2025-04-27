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
    public partial class Update : Form
    {
        private PictureBox[] stars;
        private float rating = 0;
        public event EventHandler SongUpdated;
        private bool isRatingSet = false;
        private int selectedCount = 0;
        private int selectedID = 0;
        private Dictionary<PictureBox, Point> originalPositions = new Dictionary<PictureBox, Point>();
        private System.Windows.Forms.Timer animationTimer;
        private PictureBox hoveredStar;
        private int animationStep = 0;
        private bool goingUp = true;
        private List<PictureBox> jumpingStars = new List<PictureBox>();
        private System.Windows.Forms.Timer groupJumpTimer;
        private int groupAnimationStep = 0;
        private bool groupGoingUp = true;
        public Update(string title, string author, DateOnly date, int id, string review)
        {
            InitializeComponent();
            textBox1.Text = $"{title} by {author}";
            textBox2.Text = review;
            stars = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };
            this.BackColor = Color.FromArgb(230, 218, 206);
            button1.FlatAppearance.BorderColor = Color.FromArgb(159, 160, 154);
            button1.FlatAppearance.BorderSize = 3;

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
            dateTimePicker1.Value = date.ToDateTime(new TimeOnly(0, 0));
            selectedID = id;
        }
        private void SetRating(int halfStarIndex)
        {
            rating = halfStarIndex * 0.5f; // 1 half-star = 0.5 rating
        }
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
        private void button1_Click(object sender, EventArgs e)
        {
            EntryMid.UpdateEntry(selectedID, rating,DateOnly.FromDateTime(dateTimePicker1.Value),textBox2.Text);
            SongUpdated?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}
