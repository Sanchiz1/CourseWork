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
    public class AimBall : IBall
    {
        protected Vector2 Direction = new Vector2(0, 0);
        protected Vector2 BallCenter = new Vector2(0, 0);
        protected Pen Pen;
        protected Brush Brush;
        public static int Diameter = 30;
        protected bool Collided = false;

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
        public bool collided
        {
            get
            {
                return Collided;
            }
            set
            {
                Collided = value;
            }
        }
        public AimBall(Vector2 direction, Vector2 ballcenter, Pen pen, Brush brush)
        {
            Diameter = 30;
            Direction = direction;
            BallCenter = new Vector2(ballcenter.X, ballcenter.Y);
            Pen = pen;
            Brush = brush;
        }
        public void ShowBall(PaintEventArgs e)
        {
            RectangleF rect = new RectangleF((float)BallCenter.X - Diameter / 2, (float)BallCenter.Y - Diameter / 2, Diameter, Diameter);
            e.Graphics.DrawEllipse(Pen, rect);
            e.Graphics.FillEllipse(Brush, rect);
            e.Graphics.DrawLine(new Pen(Color.White, 2f), (int)ballCenter.X, (int)ballCenter.Y, (int)ballCenter.X + (int)Direction.Scale(0.05).X, (int)ballCenter.Y + (int)Direction.Scale(0.05).Y);
        }
        public bool DetectCollision(Ball ball)
        {
            Vector2 distanceBallOneBallTwo = BallCenter - ball.ballCenter;
            float distance = (float)distanceBallOneBallTwo.Length();
            if (distance <= Diameter)
            {
                Collided = true;
                return true;
            }
            if (BallCenter.X <= Game.topLeftX + Diameter / 2)
            {
                Direction.X = -Direction.X / 2;
                return true;
            }
            if (BallCenter.X >= Game.topLeftX + Game.bottomRightX - Diameter / 2)
            {
                Direction.X = -Direction.X / 2;
                return true;
            }
            if (BallCenter.Y <= Game.topLeftY + Diameter / 2)
            {
                Direction.Y = -Direction.Y / 2;
                return true;
            }
            if (BallCenter.Y >= Game.topLeftY + Game.bottomRightY - Diameter / 2)
            {
                Direction.Y = -Direction.Y / 2;
                return true;
            }
            else
            {
                Collided = false;
                return false;
            }
        }
        public void ChangeDirections(Ball ball)
        {
            Vector2 centresVector = BallCenter - ball.ballCenter;
            Vector2 centresVector2 = ball.ballCenter - BallCenter;
            Vector2 ballOnePerpendicular = centresVector.PerpendicularComponent(Direction);
            Vector2 ballTwoPerpendicular = centresVector2.PerpendicularComponent(ball.direction);
            Vector2 ballOnePara = centresVector.ParralelComponent(Direction);
            Vector2 ballTwoPara = centresVector2.ParralelComponent(ball.direction);
            Vector2 ballOneNewVelocity = ballTwoPara + ballOnePerpendicular; //http://sinepost.wordpress.com/category/mathematics/geometry/trigonometry/
            Vector2 ballTwoNewVelocity = ballOnePara + ballTwoPerpendicular;
            Direction = ballOneNewVelocity;
        }
    }
}
