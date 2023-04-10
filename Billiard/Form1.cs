using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using Microsoft.VisualBasic.Devices;
using NAudio.Wave;

namespace Billiard
{
    public partial class Form1 : Form
    {
        Vector2 mouseLocation = new Vector2(50, 50);
        Pen blackPen = new Pen(Color.Black, 4f);
        bool draweDirLine;
        static float topLeftX = 310;
        static float topLeftY = 220;
        static float bottomRightX = 1240;
        static float bottomRightY = 540;
        bool NextTurn = true;
        bool PlayerTurn = true;
        bool WhiteBallMissing = false;
        int FirstPlayerScore = 0;
        int SecondPlayerScore = 0;

        Rectangle board = new Rectangle((int)topLeftX, (int)topLeftY, (int)bottomRightX, (int)bottomRightY);

        public Ball ball = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 300, topLeftY + 280), new Pen(Color.Black, 4f), new SolidBrush(Color.White));
        public Ball ball1 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 700, topLeftY + 280), new Pen(Color.Black, 4f), new SolidBrush(Color.Yellow));
        public Ball ball2 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 731, topLeftY + 296), new Pen(Color.Black, 4f), new SolidBrush(Color.Blue));
        public Ball ball3 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 731, topLeftY + 264), new Pen(Color.Black, 4f), new SolidBrush(Color.Red));
        public Ball ball4 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 762, topLeftY + 280), new Pen(Color.Black, 4f), new SolidBrush(Color.Purple));
        public Ball ball5 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 762, topLeftY + 311), new Pen(Color.Black, 4f), new SolidBrush(Color.GreenYellow));
        public Ball ball6 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 762, topLeftY + 249), new Pen(Color.Black, 4f), new SolidBrush(Color.Black));
        public Ball ball7 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 793, topLeftY + 296), new Pen(Color.Black, 4f), new SolidBrush(Color.DarkRed));
        public Ball ball8 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 793, topLeftY + 264), new Pen(Color.Black, 4f), new SolidBrush(Color.Red));
        public Ball ball9 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 793, topLeftY + 233), new Pen(Color.Black, 4f), new SolidBrush(Color.Yellow));
        public Ball ball10 = new Ball(new Vector2(0, 0), new Vector2(topLeftX + 793, topLeftY + 327), new Pen(Color.Black, 4f), new SolidBrush(Color.Orange));
        List<Ball> balls = new List<Ball>();

        public Hole hole1 = new Hole(new Vector2(topLeftX, topLeftY), new Pen(Color.Black, 4f), new SolidBrush(Color.Black));
        public Hole hole2 = new Hole(new Vector2(topLeftX + bottomRightX / 2, topLeftY), new Pen(Color.Black, 4f), new SolidBrush(Color.Black));
        public Hole hole3 = new Hole(new Vector2(topLeftX + bottomRightX, topLeftY), new Pen(Color.Black, 4f), new SolidBrush(Color.Black));
        public Hole hole4 = new Hole(new Vector2(topLeftX, topLeftY + bottomRightY), new Pen(Color.Black, 4f), new SolidBrush(Color.Black));
        public Hole hole5 = new Hole(new Vector2(topLeftX + bottomRightX / 2, topLeftY + bottomRightY), new Pen(Color.Black, 4f), new SolidBrush(Color.Black));
        public Hole hole6 = new Hole(new Vector2(topLeftX + bottomRightX, topLeftY + bottomRightY), new Pen(Color.Black, 4f), new SolidBrush(Color.Black));
        List<Hole> holes = new List<Hole>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            Sound.MakeSoundOnLoop("sound2.wav");
            Main.Visible = false;
            Start.TabStop = false;
            Start.FlatStyle = FlatStyle.Flat;
            Start.FlatAppearance.BorderSize = 0;
            Resume.TabStop = false;
            Resume.FlatStyle = FlatStyle.Flat;
            Resume.FlatAppearance.BorderSize = 0;
            MenuButton.TabStop = false;
            MenuButton.FlatStyle = FlatStyle.Flat;
            MenuButton.FlatAppearance.BorderSize = 0;
            Menu.Left = (this.ClientSize.Width - Menu.Width) / 2;
            Menu.Top = (this.ClientSize.Height - Menu.Height) / 2;
            Start.Parent = Menu;
            Start.Left = (Start.Parent.Width - Start.Width) / 2;
            Resume.Parent = Menu;
            Resume.Left = (Resume.Parent.Width - Resume.Width) / 2;
            MenuButton.Parent = Main;
            Player.Parent = Main;
            Scores.Parent = Main;
            draweDirLine = false;
            ResultLabel.Parent = Menu;
            ResultLabel.Left = (ResultLabel.Parent.Width - ResultLabel.Width) / 2;
            ResultLabel.Visible = false;
            holes.Add(hole1);
            holes.Add(hole2);
            holes.Add(hole3);
            holes.Add(hole4);
            holes.Add(hole5);
            holes.Add(hole6);
        }
        private void Main_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawRectangle(blackPen, board);
            g.FillRectangle(new SolidBrush(Color.ForestGreen), board);
            for (int i = 0; i < holes.Count; ++i)
            {
                holes[i].ShowHole(e);
            }
            for (int i = 0; i < balls.Count; ++i)
            {
                balls[i].ShowBall(e);
            }
            if (draweDirLine && !WhiteBallMissing)
            {
                e.Graphics.DrawLine(blackPen, (int)mouseLocation.X, (int)mouseLocation.Y, (int)ball.ballCenter.X, (int)ball.ballCenter.Y);
            }
        }
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (NextTurn == true)
            {
                mouseLocation.X = e.X;
                mouseLocation.Y = e.Y;
                draweDirLine = true;
                ball.moveBall = false;
            }
        }
        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (NextTurn == true)
            {
                mouseLocation.X = e.X;
                mouseLocation.Y = e.Y;
            }
        }
        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            if (NextTurn == true && draweDirLine)
            {
                draweDirLine = false;

                ball.SetSpeed(mouseLocation, (int)topLeftX, (int)topLeftY, (int)bottomRightX, (int)bottomRightY);
                ball.moveBall = true;
            }
        }
        private void Main_MouseClick(object sender, MouseEventArgs e)
        {
            if (WhiteBallMissing && NextTurn)
            {
                ball.velocity = new Vector2(0, 0);
                ball.speed = 1;
                ball.ballCenter.X = e.X;
                ball.ballCenter.Y = e.Y;
                bool a = true;
                if (topLeftX + ball.diameter / 2 > e.X ||
                    e.X > bottomRightX + topLeftX - ball.diameter / 2 || 
                    topLeftY + ball.diameter / 2 > e.Y ||
                    e.Y > bottomRightY + topLeftY - ball.diameter / 2)
                {
                    a = false;
                }
                for (int i = 0; i < balls.Count; ++i)
                {
                    if (balls[i].DetectCollision(ball))
                    {
                        a = false;
                    }
                }
                for (int i = 0; i < holes.Count; ++i)
                {
                    if (holes[i].DetectCollision(ball))
                    {
                        a = false;
                    }
                }
                if (a)
                {
                    WhiteBallMissing = false;
                    balls.Add(ball);
                    timer3.Start();
                }
            }
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Main.Visible = false;
            Menu.Visible = true;
        }
        private void Start_Click(object sender, EventArgs e)
        {
            Main.Visible = true;
            Menu.Visible = false;
            Main.BringToFront();
            balls.Clear();

            /*ball.BallCenter = new Vector2(topLeftX + 300, topLeftY + 15);
            ball1.BallCenter = new Vector2(topLeftX + 700, topLeftY + 15);*/

            ball.ballCenter = new Vector2(topLeftX + 300, topLeftY + 280);
            ball1.ballCenter = new Vector2(topLeftX + 700, topLeftY + 280);
            ball2.ballCenter = new Vector2(topLeftX + 731, topLeftY + 296);
            ball3.ballCenter = new Vector2(topLeftX + 731, topLeftY + 264);
            ball4.ballCenter = new Vector2(topLeftX + 762, topLeftY + 280);
            ball5.ballCenter = new Vector2(topLeftX + 762, topLeftY + 311);
            ball6.ballCenter = new Vector2(topLeftX + 762, topLeftY + 249);
            ball7.ballCenter = new Vector2(topLeftX + 793, topLeftY + 296);
            ball8.ballCenter = new Vector2(topLeftX + 793, topLeftY + 264);
            ball9.ballCenter = new Vector2(topLeftX + 793, topLeftY + 233);
            ball10.ballCenter = new Vector2(topLeftX + 793, topLeftY + 327);

            balls.Add(ball);
            balls.Add(ball1);
            balls.Add(ball2);
            balls.Add(ball3);
            balls.Add(ball4);
            balls.Add(ball5);
            balls.Add(ball6);
            balls.Add(ball7);
            balls.Add(ball8);
            balls.Add(ball9);
            balls.Add(ball10);/**/
            for (int i = 0; i < balls.Count; ++i)
            {
                balls[i].speed = 1;
                balls[i].velocity = new Vector2(0, 0);
            }
            FirstPlayerScore = 0;
            SecondPlayerScore = 0;
            PlayerTurn = true;
            WhiteBallMissing = false;
            NextTurn = true;
            timer3.Start();
            Resume.Visible = true;
            ResultLabel.Visible = false;
        }
        private void Resume_Click(object sender, EventArgs e)
        {
            if (balls.Count > 1)
            {
                Main.Visible = true;
                Menu.Visible = false;
                Main.BringToFront();
            }
        }
        private void EndGame()
        {
            Main.Visible = false;
            Menu.Visible = true;
            Menu.BringToFront();
            Resume.Visible = false;
            ResultLabel.Visible = true;
            if (FirstPlayerScore > SecondPlayerScore)
            {
                ResultLabel.Text = "FIRST PLAYER WON!";
            }
            else if (SecondPlayerScore > FirstPlayerScore)
            {
                ResultLabel.Text = "SECOND PLAYER WON!";
            }
            else
            {
                ResultLabel.Text = "DRAW!";
            }
            Sound.MakeSound("victory.wav");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < balls.Count / 2; ++i)
            {
                balls[i].MoveTheBall(topLeftX, topLeftY, bottomRightX, bottomRightY);
            }
            for (int i = 0; i < balls.Count / 2; ++i)
            {
                for (int j = i + 1; j < balls.Count; ++j)
                {
                    if (balls[i].DetectCollision(balls[j]))
                    {
                        balls[i].ChangeVelicities(balls[j]);
                    }
                }
            }
            for (int i = 0; i < holes.Count; ++i)
            {
                for (int j = 0; j < balls.Count; ++j)
                {
                    if (holes[i].DetectCollision(balls[j]) && balls[j] != ball)
                    {
                        Sound.MakeSound("hole.wav");
                        balls.RemoveAt(j);
                        if (PlayerTurn)
                        {
                            FirstPlayerScore++;
                        }
                        else
                        {
                            SecondPlayerScore++;
                        }
                    }
                    else if (holes[i].DetectCollision(balls[j]) && balls[j] == ball)
                    {
                        Sound.MakeSound("hole.wav");
                        balls.RemoveAt(j);
                        WhiteBallMissing = true;
                    }
                }
            }
            this.Refresh();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = balls.Count / 2; i < balls.Count; ++i)
            {
                balls[i].MoveTheBall(topLeftX, topLeftY, bottomRightX, bottomRightY);
            }
            for (int i = balls.Count / 2; i < balls.Count; ++i)
            {
                for (int j = i + 1; j < balls.Count; ++j)
                {
                    if (balls[i].DetectCollision(balls[j]))
                    {
                        balls[i].ChangeVelicities(balls[j]);
                    }
                }
            }
            this.Refresh();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            bool a = true;
            for (int i = 0; i < balls.Count; ++i)
            {
                if (balls[i].velocity.Length() != 0)
                {
                    a = false;
                }
            }
            if (a && !NextTurn)
            {
                if (a)
                {
                    PlayerTurn = !PlayerTurn;
                }
            }
            NextTurn = a;
            if (PlayerTurn)
            {
                Player.Text = "First player turn";
            }
            else
            {
                Player.Text = "Second player turn";
            }
            Scores.Text = $"First player score:      {FirstPlayerScore}" + Environment.NewLine + $"Second player score: {SecondPlayerScore}";
            if (FirstPlayerScore + SecondPlayerScore == 10)
            {
                EndGame();
                timer3.Stop();
            }
            if(WhiteBallMissing && NextTurn)
            {
                timer3.Stop();
            }
        }
    }
}