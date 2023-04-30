using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout_Game
{
    public partial class Form1 : Form
    {

        bool goLeft;
        bool goRight;
        bool isGameover;


        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random rnd= new Random();




        public Form1()
        {
            InitializeComponent();

            setupGame();
        }

        private void setupGame()
        {
            score = 0;
            ballx = 12;
            bally = 12;
            playerSpeed = 50;
            txtScore.Text ="Score: " + score;


            gameTimer.Start();
            
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag =="blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }

            }

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gameOver(string message)
        {
            isGameover = true;
            gameTimer.Stop();

            txtScore.Text = "Score: " + score + " " + message;
        }



        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;


            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left < 757)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 837)
            {
                ballx = -ballx;

            }
            if (ball.Top < 0)
            {
                bally = -bally;
            }

            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = rnd.Next(12, 50) * -1;

                if (ballx < 0)
                {
                    ballx = rnd.Next(12, 50) * -1;

                }
                else
                {
                    ballx = rnd.Next(12, 50) * -1;
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if(ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;

                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }

            if(score == 15)
            {
                //shows the end game message here
                gameOver("You Win!");
            }

            if(ball.Top > 616)
            {
                //shows game over
                gameOver("You Lose!!!!");
            }



        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true; 
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }
    }
}
