using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Billiard.Properties;
using Billiard.Mechanics;

namespace Billiard
{
    public class Game
    {
        static Billiards form = Application.OpenForms.OfType<Billiards>().FirstOrDefault();
        Vector2 mouseLocation = new Vector2(50, 50);
        Pen blackPen = new Pen(Color.Black, 4f);
        bool draweDirLine;
        public static float topLeftX = 310;
        public static float topLeftY = 220;
        public static float bottomRightX = 1240;
        public static float bottomRightY = 540;
        bool NextTurn = true;
        bool PlayerTurn = true;
        int ballsCount = 0;
        Rectangle board = new Rectangle((int)topLeftX, (int)topLeftY, (int)bottomRightX, (int)bottomRightY);
        public Vector2[] positionsBall = { 
            new Vector2(topLeftX + 300, topLeftY + 280),
            new Vector2(topLeftX + 800, topLeftY + 280),
            new Vector2(topLeftX + 831, topLeftY + 296),
            new Vector2(topLeftX + 831, topLeftY + 264),
            new Vector2(topLeftX + 862, topLeftY + 280),
            new Vector2(topLeftX + 862, topLeftY + 311),
            new Vector2(topLeftX + 862, topLeftY + 249),
            new Vector2(topLeftX + 893, topLeftY + 296),
            new Vector2(topLeftX + 893, topLeftY + 264),
            new Vector2(topLeftX + 893, topLeftY + 233),
            new Vector2(topLeftX + 893, topLeftY + 327)};
        public Vector2[] positionsHole = {
            new Vector2(topLeftX, topLeftY),
            new Vector2(topLeftX + bottomRightX / 2, topLeftY),
            new Vector2(topLeftX + bottomRightX, topLeftY),
            new Vector2(topLeftX, topLeftY + bottomRightY),
            new Vector2(topLeftX + bottomRightX / 2, topLeftY + bottomRightY),
            new Vector2(topLeftX + bottomRightX, topLeftY + bottomRightY)};
        public List<Color> colors = new List<Color>();
        List<Ball> balls = new List<Ball>();
        List<Hole> holes = new List<Hole>();
        public ScoredBalls scoredBalls = new ScoredBalls();

        public Game()
        {
        }
        public void StartGame()
        {
            form.Menu.Visible = false;
            form.Main.Visible = true;
            PlayerTurn = true;
            NextTurn = true;
            form.Resume.Visible = true;
            form.ResultLabel.Visible = false;
            ballsCount = 0;
            form.timer3.Start();
            colors.Add(Color.Yellow);
            colors.Add(Color.Blue);
            colors.Add(Color.Red);
            colors.Add(Color.Purple);
            colors.Add(Color.GreenYellow);
            colors.Add(Color.Black);
            colors.Add(Color.DarkRed);
            colors.Add(Color.Red);
            colors.Add(Color.Yellow);
            colors.Add(Color.Orange);
            AddElements();
            form.Main.BringToFront();
            scoredBalls = new ScoredBalls();
            scoredBalls.GeneratePlaces();
        }
        public void Drawing(PaintEventArgs e) {
            Graphics g = e.Graphics;
            scoredBalls.ShowScoredBalls(e);
            g.DrawRectangle(blackPen, board);
            g.FillRectangle(new SolidBrush(Color.ForestGreen), board);
            for (int i = 0; i < holes.Count; ++i)
            {
                holes[i].ShowHole(e);
            }
            for (int i = ballsCount; i < balls.Count; ++i)
            {
                balls[i].ShowBall(e);
            }
            if (draweDirLine && ballsCount == 0)
            {
                e.Graphics.DrawLine(blackPen, (int)mouseLocation.X, (int)mouseLocation.Y, (int)balls[0].ballCenter.X, (int)balls[0].ballCenter.Y);
                if(form.settings.aim) Aim.DrawAim(mouseLocation, balls[0].ballCenter, e, balls);
            }
        }
        public void AddElements()
        {
            balls.Clear();
            holes.Clear();
            balls.Add(new MainBall(new Vector2(0, 0), new Vector2(positionsBall[0].X - Ball.Diameter, positionsBall[0].Y - Ball.Diameter), new Pen(Color.Black, 4f), new SolidBrush(Color.White)));
            Random r = new Random();
            for (int i = 1; i < 11; ++i)
            {
                balls.Add(new Ball(new Vector2(0, 0), new Vector2(positionsBall[i].X - Ball.Diameter, positionsBall[i].Y - Ball.Diameter), new Pen(Color.Black, 4f), new SolidBrush(colors[i - 1])));
            }
            for (int i = 0; i < 6; ++i)
            {
                holes.Add(new Hole(positionsHole[i], new Pen(Color.Black, 4f), new SolidBrush(Color.Black)));
            }
        }
        public void MouseDown(MouseEventArgs e)
        {
            if (NextTurn == true)
            {
                mouseLocation.X = e.X;
                mouseLocation.Y = e.Y;
                draweDirLine = true;
            }
        }
        public void MouseMove(MouseEventArgs e)
        {
            if (NextTurn == true)
            {
                mouseLocation.X = e.X;
                mouseLocation.Y = e.Y;
            }
        }
        public void MouseUp(MouseEventArgs e)
        {
            if (NextTurn == true && draweDirLine)
            {
                draweDirLine = false;

                if (balls[0] is MainBall ball)
                {
                    ball.SetSpeed(mouseLocation);
                }
            }
        }
        public void MouseClick(MouseEventArgs e)
        {
            if (ballsCount == 1 && NextTurn)
            {
                balls[0].direction = new Vector2(0, 0);
                balls[0].speed = 1;
                balls[0].ballCenter.X = e.X;
                balls[0].ballCenter.Y = e.Y;
                bool a = true;
                if (topLeftX + Ball.Diameter / 2 > e.X ||
                    e.X > bottomRightX + topLeftX - Ball.Diameter / 2 ||
                    topLeftY + Ball.Diameter / 2 > e.Y ||
                    e.Y > bottomRightY + topLeftY - Ball.Diameter / 2)
                {
                    a = false;
                }
                for (int i = ballsCount; i < balls.Count; ++i)
                {
                    if (balls[i].DetectCollision(balls[0]))
                    {
                        a = false;
                    }
                }
                for (int i = 0; i < holes.Count; ++i)
                {
                    if (holes[i].DetectCollision(balls[0]))
                    {
                        a = false;
                    }
                }
                if (a)
                {
                    ballsCount = 0;
                    form.Main.Cursor = Cursors.Default;
                    form.timer3.Start();
                }
            }
        }
        public void timer1_Tick(EventArgs e)
        {
            for (int i = ballsCount; i < balls.Count / 2; ++i)
            {
                balls[i].MoveTheBall();
            }
            for (int i = ballsCount; i < balls.Count / 2; ++i)
            {
                for (int j = i + 1; j < balls.Count; ++j)
                {
                    if (balls[i].DetectCollision(balls[j]))
                    {
                        if (form.settings.soundEffects) Sound.MakeSound(Application.StartupPath + "\\/Sounds/collision.wav");
                        balls[i].ChangeDirections(balls[j]);
                    }
                }
            }
            for (int i = 0; i < holes.Count; ++i)
            {
                for (int j = ballsCount; j < balls.Count; ++j)
                {
                    if (holes[i].DetectCollision(balls[j]) && balls[j] != balls[0])
                    {
                        if(form.settings.soundEffects) Sound.MakeSound(Application.StartupPath + "\\/Sounds/hole.wav");
                        if (PlayerTurn)
                        {
                            scoredBalls.FirstPlayerScored(balls[j]);
                        }
                        else
                        {
                            scoredBalls.SecondPlayerScored(balls[j]);
                        }
                        balls.RemoveAt(j);
                    }
                    else if (holes[i].DetectCollision(balls[j]) && balls[j] == balls[0])
                    {
                        if (form.settings.soundEffects) Sound.MakeSound(Application.StartupPath + "\\/Sounds/hole.wav");
                        ballsCount = 1;
                        form.Main.Cursor = Cursors.Hand;
                    }
                }
            }
            form.Refresh();
        }
        public void timer2_Tick(EventArgs e)
        {
            for (int i = balls.Count / 2; i < balls.Count; ++i)
            {
                balls[i].MoveTheBall();
            }
            for (int i = balls.Count / 2; i < balls.Count; ++i)
            {
                for (int j = i + 1; j < balls.Count; ++j)
                {
                    if (balls[i].DetectCollision(balls[j]))
                    {
                        if (form.settings.soundEffects) Sound.MakeSound(Application.StartupPath + "\\/Sounds/collision.wav");
                        balls[i].ChangeDirections(balls[j]);
                    }
                }
            }
            form.Refresh();
        }
        public void timer3_Tick(EventArgs e)
        {
            bool a = true;
            for (int i = ballsCount; i < balls.Count; ++i)
            {
                if (balls[i].direction.Length() != 0)
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
                if(form.settings.language == "Українська")
                {
                    form.Player.Text = "Хід першого гравця";

                }
                else
                {
                    form.Player.Text = "First player turn";
                }
            }
            else
            {
                if (form.settings.language == "Українська")
                {
                    form.Player.Text = "Хід другого гравця";

                }
                else
                {
                    form.Player.Text = "Second player turn";
                }
            }
            if (form.settings.language == "Українська")
            {
                form.lblFirstPlyerScore.Text = $"Рахунок першого гравця: {scoredBalls.FirstPlayerBalls.Count()}";
                form.lblSecondPlyerScore.Text = $"Рахунок другого гравця: {scoredBalls.SecondPlayerBalls.Count()}";
            }
            else
            {
                form.lblFirstPlyerScore.Text = $"First player score: {scoredBalls.FirstPlayerBalls.Count()}";
                form.lblSecondPlyerScore.Text = $"Second player score: {scoredBalls.SecondPlayerBalls.Count()}";
            }
            
            if (scoredBalls.FirstPlayerBalls.Count() + scoredBalls.SecondPlayerBalls.Count() == 10)
            {
                EndGame();
                form.timer3.Stop();
            }
            if (ballsCount == 1 && NextTurn)
            {
                form.timer3.Stop();
            }
        }
        private void EndGame()
        {
            form.Main.Visible = false;
            form.Menu.Visible = true;
            form.Menu.BringToFront();
            form.Resume.Visible = false;
            form.ResultLabel.Visible = true;
            if (scoredBalls.FirstPlayerBalls.Count() > scoredBalls.SecondPlayerBalls.Count())
            {
                if (form.settings.language == "Українська")
                {
                    form.ResultLabel.Text = "ПЕРШИЙ ГРАВЕЦЬ ПЕРЕМІГ!";
                }
                else
                {
                    form.ResultLabel.Text = "FIRST PLAYER WON!";
                }
            }
            else if (scoredBalls.SecondPlayerBalls.Count() > scoredBalls.FirstPlayerBalls.Count())
            {
                if (form.settings.language == "Українська")
                {
                    form.ResultLabel.Text = "ДРУГИЙ ГРАВЕЦЬ ПЕРЕМІГ!";
                }
                else
                {
                    form.ResultLabel.Text = "SECOND PLAYER WON!";
                }
            }
            else
            {
                if (form.settings.language == "Українська")
                {
                    form.ResultLabel.Text = "НІЧИЯ!";
                }
                else
                {
                    form.ResultLabel.Text = "DRAW!";
                }
            }
            if (form.settings.soundEffects) Sound.MakeSound(Application.StartupPath + "\\Sounds/victory.wav");
        }
        public void Resume_Click()
        {
            if (balls.Count > 1)
            {
                form.Main.Visible = true;
                form.Menu.Visible = false;
                form.Main.BringToFront();
            }
        }
    }
}
