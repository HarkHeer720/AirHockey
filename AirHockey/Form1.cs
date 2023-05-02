using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace AirHockey
{
    public partial class Form1 : Form
    {
        // declaring the size and postition of each drawing
        Rectangle player1 = new Rectangle(180, 32, 40, 40);
        Rectangle player2 = new Rectangle(180, 532, 39, 39);

        Rectangle player1LeftSide = new Rectangle(178, 30, 5, 40);
        Rectangle player1RightSide = new Rectangle(218, 30, 5, 45);
        Rectangle player1Bottom = new Rectangle(178, 70, 40, 5);
        Rectangle player1Top = new Rectangle(178, 30, 40, 5);

        Rectangle player2LeftSide = new Rectangle(178, 530, 5, 40);
        Rectangle player2RightSide = new Rectangle(216, 530, 5, 43);
        Rectangle player2Top = new Rectangle(178, 530, 40, 5);
        Rectangle player2Bottom = new Rectangle(178, 568, 42, 5);

        Rectangle ball = new Rectangle(195, 295, 10, 10);
        Rectangle player1Goal = new Rectangle(150, 18, 100, 8);
        Rectangle player2Goal = new Rectangle(150, 574, 100, 8);

        // decalaring variables
        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 6;
        int ballXSpeed = 0;
        int ballYSpeed = 0;
        bool baller = false;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;

        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        // creating brushes and pens to draw with
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush orangeBrush = new SolidBrush(Color.Orange);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush lightBlueBrush = new SolidBrush(Color.Blue);

        Pen blackPen = new Pen(Color.Black, 4);

        // creating soundplayers
        SoundPlayer puckPlayerHit = new SoundPlayer(Properties.Resources.puckPlayerHit);
        SoundPlayer goalScored = new SoundPlayer(Properties.Resources.goalScored);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) // reading when keys are pressed
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) // reading when keys are let go of
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // painting the the walls, the ball, and each player as 5 seperate rectangles for proper movement and collisons
            e.Graphics.FillRectangle(blackBrush, 42, 298, 320, 4);
            e.Graphics.DrawEllipse(blackPen, 150, 250, 100, 100);
            e.Graphics.FillRectangle(redBrush, 42, 175, 320, 4);
            e.Graphics.FillRectangle(blueBrush, 42, 425, 320, 4);

            e.Graphics.FillRectangle(redBrush, ball);

            e.Graphics.FillRectangle(blueBrush, player1LeftSide);
            e.Graphics.FillRectangle(blueBrush, player1RightSide);
            e.Graphics.FillRectangle(blueBrush, player1Top);
            e.Graphics.FillRectangle(blueBrush, player1Bottom);

            e.Graphics.FillRectangle(redBrush, player2LeftSide);
            e.Graphics.FillRectangle(redBrush, player2RightSide);
            e.Graphics.FillRectangle(redBrush, player2Top);
            e.Graphics.FillRectangle(redBrush, player2Bottom);

            e.Graphics.FillRectangle(blackBrush, player2);
            e.Graphics.FillRectangle(blackBrush, player1);

            e.Graphics.DrawRectangle(blackPen, 40, 20, 320, 560);

            e.Graphics.FillRectangle(blueBrush, player1Goal);
            e.Graphics.FillRectangle(redBrush, player2Goal);
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // only starts moving the ball after it's been hit
            if (baller == true)
            {
                ball.Y += ballYSpeed;
                ball.X += ballXSpeed;
            }
            else
            {

            }

            // moving player 1
            if (wDown == true && player1.Y > 24)
            {
                player1.Y -= playerSpeed;
                player1LeftSide.Y -= playerSpeed;
                player1RightSide.Y -= playerSpeed;
                player1Top.Y -= playerSpeed;
                player1Bottom.Y -= playerSpeed;
            }
            if (sDown == true && player1.Y < this.Height - player1.Height - 305)
            {
                player1.Y += playerSpeed;
                player1LeftSide.Y += playerSpeed;
                player1RightSide.Y += playerSpeed;
                player1Top.Y += playerSpeed;
                player1Bottom.Y += playerSpeed;
            }
            if (aDown == true && player1.X > 44)
            {
                player1.X -= playerSpeed;
                player1LeftSide.X -= playerSpeed;
                player1RightSide.X -= playerSpeed;
                player1Top.X -= playerSpeed;
                player1Bottom.X -= playerSpeed;
            }
            if (dDown == true && player1.X < this.Width - player1.Width - 44)
            {
                player1.X += playerSpeed;
                player1LeftSide.X += playerSpeed;
                player1RightSide.X += playerSpeed;
                player1Top.X += playerSpeed;
                player1Bottom.X += playerSpeed;
            }

            // moving player 2
            if (upArrowDown == true && player2.Y > 305)
            {
                player2.Y -= playerSpeed;
                player2LeftSide.Y -= playerSpeed;
                player2RightSide.Y -= playerSpeed;
                player2Top.Y -= playerSpeed;
                player2Bottom.Y -= playerSpeed;
            }
            if (downArrowDown == true && player2.Y < this.Height - player2.Height - 25)
            {
                player2.Y += playerSpeed;
                player2LeftSide.Y += playerSpeed;
                player2RightSide.Y += playerSpeed;
                player2Top.Y += playerSpeed;
                player2Bottom.Y += playerSpeed;
            }
            if (leftArrowDown == true && player2.X > 44)
            {
                player2.X -= playerSpeed;
                player2LeftSide.X -= playerSpeed;
                player2RightSide.X -= playerSpeed;
                player2Top.X -= playerSpeed;
                player2Bottom.X -= playerSpeed;
            }
            if (rightArrowDown == true && player2.X < this.Width - player2.Width - 45)
            {
                player2.X += playerSpeed;
                player2LeftSide.X += playerSpeed;
                player2RightSide.X += playerSpeed;
                player2Top.X += playerSpeed;
                player2Bottom.X += playerSpeed;
            }

            // checking if the ball has collided with a wall
            if (ball.Y < 20)
            {
                ball.Y += 4;
                ballYSpeed *= -1;
            }
            if (ball.Y > this.Height - ball.Height - 20)
            {
                ball.Y -= 4;
                ballYSpeed *= -1;
            }
            if (ball.X < 40)
            {
                ball.X += 4;
                ballXSpeed *= -1;
            }
            if (ball.X > this.Width - ball.Width - 40)
            {
                ball.X -= 4;
                ballXSpeed *= -1;
            }
            // checking if the ball has intersected with player 1
            if (player1LeftSide.IntersectsWith(ball) && player1LeftSide.X <= 312)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = 8;
                baller = true;
                ball.X -= 5;
                player1.X += 3;
                player1Bottom.X += 3;
                player1Top.X += 3;
                player1LeftSide.X += 3;
                player1RightSide.X += 3;
                ballXSpeed *= -1;
            }
            else if (player1LeftSide.IntersectsWith(ball) && player1LeftSide.X > 312)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = 8;
                baller = true; 
                ball.X -= 5;
                ballXSpeed *= -1;
            }
            if (player1RightSide.IntersectsWith(ball) && player1RightSide.X >= 82)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = -8;
                baller = true;
                ball.X += 5;
                player1.X -= 3;
                player1Bottom.X -= 3;
                player1Top.X -= 3;
                player1LeftSide.X -= 3;
                player1RightSide.X -= 3;
                ballXSpeed *= -1;
            }
            else if (player1RightSide.IntersectsWith(ball) && player1RightSide.X < 82)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = -8;
                baller = true;
                ball.X += 5;
                ballXSpeed *= -1;
            }
            if (player1Top.IntersectsWith(ball) && player1Top.Y <= 534)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = 8;
                baller = true;
                ball.Y -= 5;
                player1.Y += 3;
                player1Bottom.Y += 3;
                player1Top.Y += 3;
                player1LeftSide.Y += 3;
                player1RightSide.Y += 3;
                ballYSpeed *= -1;
            }
            else if (player1Top.IntersectsWith(ball) && player1Top.Y > 534)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = 8;
                baller = true;
                ball.Y -= 5;
                ballYSpeed *= -1;
            }
            if (player1Bottom.IntersectsWith(ball) && player1Bottom.Y >= 58)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = -8;
                baller = true;
                ball.Y += 5;
                player1.Y -= 3;
                player1Bottom.Y -= 3;
                player1Top.Y -= 3;
                player1LeftSide.Y -= 3;
                player1RightSide.Y -= 3;
                ballYSpeed *= -1;
            }
            else if (player1Bottom.IntersectsWith(ball) && player1Bottom.Y < 58)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = -8;
                baller = true;
                ball.Y += 5;
                ballYSpeed *= -1;
            }

            // checking if the ball has colided with player 2
            if (player2LeftSide.IntersectsWith(ball) && player2LeftSide.X <= 312)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = 8;
                baller = true;
                ball.X -= 5;
                player2.X += 3;
                player2Bottom.X += 3;
                player2Top.X += 3;
                player2LeftSide.X += 3;
                player2RightSide.X += 3;
                ballXSpeed *= -1;
            }
            else if (player2LeftSide.IntersectsWith(ball) && player2LeftSide.X > 312)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = 8;
                baller = true;
                ball.X -= 5;
                ballXSpeed *= -1;
            }
            if (player2RightSide.IntersectsWith(ball) && player2RightSide.X >= 82)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = -8;
                baller = true;
                ball.X += 5;
                player2.X -= 3;
                player2Bottom.X -= 3;
                player2Top.X -= 3;
                player2LeftSide.X -= 3;
                player2RightSide.X -= 3;
                ballXSpeed *= -1;
            }
            else if (player2RightSide.IntersectsWith(ball) && player2RightSide.X < 82)
            {
                puckPlayerHit.Play();

                ballYSpeed /= 2;
                ballXSpeed = -8;
                baller = true;
                ball.X += 5;
                ballXSpeed *= -1;
            }
            if (player2Top.IntersectsWith(ball) && player2Top.Y <= 534)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = 8;
                baller = true;
                ball.Y -= 5;
                player2.Y += 3;
                player2Bottom.Y += 3;
                player2Top.Y += 3;
                player2LeftSide.Y += 3;
                player2RightSide.Y += 3;
                ballYSpeed *= -1;
            }
            else if (player2Top.IntersectsWith(ball) && player2Top.Y > 534)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = 8;
                baller = true;
                ball.Y -= 5;
                ballYSpeed *= -1;
            }
            if (player2Bottom.IntersectsWith(ball) && player2Bottom.Y >= 58)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = -8;
                baller = true;
                ball.Y += 5;
                player2.Y -= 3;
                player2Bottom.Y -= 3;
                player2Top.Y -= 3;
                player2LeftSide.Y -= 3;
                player2RightSide.Y -= 3;
                ballYSpeed *= -1;
            }
            else if (player2Bottom.IntersectsWith(ball) && player2Bottom.Y < 58)
            {
                puckPlayerHit.Play();

                ballXSpeed /= 2;
                ballYSpeed = -8;
                baller = true;
                ball.Y += 5;
                ballYSpeed *= -1;
            }

            // checking if a player has scored
            if (player1Goal.IntersectsWith(ball))
            {
                player2Score++;

                ball.X = 195;
                ball.Y = 275;

                p2scoreLabel.Text = $"{player2Score}";

                playerReset();
            }
            else if (player2Goal.IntersectsWith(ball))
            {
                player1Score++;

                ball.X = 195;
                ball.Y = 315;

                p1scoreLabel.Text = $"{player1Score}";

                playerReset();
            }

            // checking if a player has won
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }

            Refresh();
        }
        public void playerReset()
        {
            player1.X = 180;
            player1.Y = 32;
            player1LeftSide.X = 178;
            player1LeftSide.Y = 30;
            player1RightSide.X = 218;
            player1RightSide.Y = 30;
            player1Bottom.X = 178;
            player1Bottom.Y = 70;
            player1Top.X = 178;
            player1Top.Y = 30;

            player2.X = 181;
            player2.Y = 532;
            player2LeftSide.X = 178;
            player2LeftSide.Y = 530;
            player2RightSide.X = 218;
            player2RightSide.Y = 530;
            player2Bottom.X = 178;
            player2Bottom.Y = 568;
            player2Top.X = 178;
            player2Top.Y = 530;

            baller = false;

            ballXSpeed = 0;
            ballYSpeed = 0;

            goalScored.Play();
        }
    }
}