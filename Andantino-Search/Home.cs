using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Andantino_Search
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(box_p1_ai.Checked)
            {
                box_p1_human.Enabled = false;
            }
            else
            {
                box_p1_human.Enabled = true;
            }
        }

        private void box_p1_human_CheckedChanged(object sender, EventArgs e)
        {
            if(box_p1_human.Checked)
            {
                box_p1_ai.Enabled = false;
            }
            else
            {
                box_p1_ai.Enabled = true;
            }
        }

        private void box_p2_ai_CheckedChanged(object sender, EventArgs e)
        {
            if (box_p2_ai.Checked)
            {
                box_p2_human.Enabled = false;
            }
            else
            {
                box_p2_human.Enabled = true;
            }

        }

        private void box_p2_human_CheckedChanged(object sender, EventArgs e)
        {
            if (box_p2_human.Checked)
            {
                box_p2_ai.Enabled = false;
            }
            else
            {
                box_p2_ai.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(box_p1_ai.Checked && box_p2_ai.Checked)
            {
                MessageBox.Show("AI vs AI option not implemented");
                return;
            }
            if(box_p2_human.Checked && box_p1_human.Checked )
            {
                MessageBox.Show("Human vs Human option deprecated :(");
                return;
            }

            if((box_p1_ai.Checked || box_p1_human.Checked) && (box_p2_ai.Checked || box_p2_human.Checked))
            {
                //if(box_p1_ai.Checked)
                //{
                //    Option.player1_type = 1;
                //    Option
                //}
                //else
                //{
                //    Option.player1_type = 2;
                //}
                //if(box_p2_ai.Checked)
                //{
                //    Option.player2_type = 1;
                //}
                //else
                //{
                //    Option.player2_type = 2;
                //}
                if(box_p1_ai.Checked && box_p2_human.Checked)
                {
                    Option.isplayer1_turn = false;
                    Option.isplayer2_turn = true;
                    Option.i_play_second = false;
                }
                else
                {
                    Option.isplayer1_turn = true;
                    Option.isplayer2_turn = false;
                    Option.i_play_second = true;
                }

                //this.Hide();
                Form1 f = new Form1();
                //f.Visible = true;
                f.FormClosing += delegate { /*this.Hide(); */this.Close(); };
                f.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Please choose a type of player for both players.");
                return;
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Home_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Drawing.draw_player1_home(e.Graphics, label1.Location.X + 65, label1.Location.Y+5);
            Drawing.draw_player2_home(e.Graphics, label2.Location.X + 65, label1.Location.Y + 5);

        }
    }
}
