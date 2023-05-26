using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Billiard;

namespace Billiard
{
    public class Ball : IBall
    {
        protected Vector2 Direction = new Vector2(0, 0);
        protected Vector2 BallCenter = new Vector2(0, 0);
        protected double Speed;
        protected Pen Pen;
        protected Brush Brush;
        public static int Diameter = 30;
        
        public Vector2 direction
        {
            get
            {
                return Direction;
            }
            set
            {
                Direction = value;
            }
        }
        public Vector2 ballCenter
        {
            get
            {
                return BallCenter;
            }
            set
            {
                BallCenter = value;
            }
        }
        public double speed
        {
            get
            {
                return Speed;
            }
            set
            {
                Speed = value;
            }
        }
        public Pen pen
        {
            get
            {
                return Pen;
            }
            set
            {
                Pen = value;
            }
        }
        public Brush brush
        {
            get
            {
                return Brush;
            }
            set
            {
                Brush = value;
            }
        }
        public Ball(Vector2 direction, Vector2 ballcenter, Pen pen, Brush brush) {
            Diameter = 30;
            Direction = direction;
            BallCenter = new Vector2(ballcenter.X + Diameter / 2, ballcenter.Y + Diameter / 2);
            Pen = pen;
            Brush = brush;
            Speed = 1;
        }
        public void ShowBall(PaintEventArgs e)
        {
            RectangleF rect = new RectangleF((float)BallCenter.X - Diameter / 2, (float)BallCenter.Y - Diameter / 2, Diameter, Diameter);
            e.Graphics.DrawEllipse(Pen, rect);
            e.Graphics.FillEllipse(Brush, rect);
        }
        public void MoveTheBall()
        {
            BallCenter.X += Direction.X * Speed;
            BallCenter.Y += Direction.Y * Speed;

            Direction.X *= 0.995;
            Direction.Y *= 0.995;

            if ((Direction.X < 0.05 && Direction.X > -0.05) && (Direction.Y < 0.05 && Direction.Y > -0.05))
            {
                Direction.X = 0;
                Direction.Y = 0;
            }

            if (BallCenter.X <= Game.topLeftX + Diameter / 2)
            {
                Direction.X = -Direction.X / 2;
                BallCenter.X = Game.topLeftX + Diameter / 2;
            }
            if (BallCenter.X >= Game.topLeftX + Game.bottomRightX - Diameter / 2)
            {
                Direction.X = -Direction.X / 2;
                BallCenter.X = Game.topLeftX + Game.bottomRightX - Diameter / 2;
            }
            if (BallCenter.Y <= Game.topLeftY + Diameter / 2)
            {
                Direction.Y = -Direction.Y / 2;
                BallCenter.Y = Game.topLeftY + Diameter / 2;
            }
            if (BallCenter.Y >= Game.topLeftY + Game.bottomRightY - Diameter / 2)
            {
                Direction.Y = -Direction.Y / 2;
                BallCenter.Y = Game.topLeftY + Game.bottomRightY - Diameter / 2;
            }
        }
        public bool DetectCollision(Ball ball)
        {
            Vector2 distanceBallOneBallTwo = BallCenter - ball.BallCenter;
            float distance = (float)distanceBallOneBallTwo.Length();
            if (distance <= Diameter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ChangeDirections(Ball ball) {
            if (Direction.Length() > ball.Direction.Length())
            {
                ball.Speed = Speed;
            }
            else if (Direction.Length() < ball.Direction.Length())
            {
                Speed = ball.Speed;
            }
            Vector2 normal1 = new Vector2();
            normal1.Normalize(BallCenter - ball.BallCenter);
            Vector2 normal2 = new Vector2();
            normal2.Normalize(ball.BallCenter - BallCenter);
            double overlap = Diameter - (BallCenter - ball.BallCenter).Length();
            BallCenter += normal1.Scale((overlap + 1) / 2);
            ball.BallCenter += normal2.Scale((overlap + 1) / 2);
            Vector2 centresVector = BallCenter - ball.BallCenter;
            Vector2 centresVector2 = ball.BallCenter - BallCenter;
            Vector2 ballOnePerpendicular = centresVector.PerpendicularComponent(Direction);
            Vector2 ballTwoPerpendicular = centresVector2.PerpendicularComponent(ball.Direction);
            Vector2 ballOnePara = centresVector.ParralelComponent(Direction);
            Vector2 ballTwoPara = centresVector2.ParralelComponent(ball.Direction);
            Vector2 ballOneNewVelocity = ballTwoPara + ballOnePerpendicular; //http://sinepost.wordpress.com/category/mathematics/geometry/trigonometry/
            Vector2 ballTwoNewVelocity = ballOnePara + ballTwoPerpendicular;
            ball.Direction = ballTwoNewVelocity;
            Direction = ballOneNewVelocity;
        }
    }
}
