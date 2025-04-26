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
        private bool[] isclicked = new bool[10];
        public Update()
        {
            InitializeComponent();
            comboBox1.DataSource = UserMid.GetUsersEntries(Program.CurrentUser.Id);
            stars = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };

            for (int i = 0; i < stars.Length; i++)
            {
                int index = i;
                stars[i].Click += (s, e) =>
                {
                    rating = (index+1) / 2f;
                    HighlightStars(index + 1);
                    SetAsClicked(index + 1);
                };
                stars[i].MouseEnter += (s, e) => HighlightStars(index + 1);
                stars[i].MouseLeave += (s, e) => EmptyAfter(index + 1);
            }
        }
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

        private void button1_Click(object sender, EventArgs e)
        {
            EntryMid.ChangeRating(EntryMid.GetSongId((comboBox1.SelectedItem as Entry).Id), rating);
            SongUpdated?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}
