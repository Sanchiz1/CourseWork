using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Billiard.Balls;
using Billiard;

namespace Billiard
{
    public class AimBall : IBall
    {
        protected Vector2 Velocity = new Vector2(0, 0);
        protected Vector2 BallCenter = new Vector2(0, 0);
        protected Pen Pen;
        protected Brush Brush;
        protected bool MoveBall = true;
        public static int Diameter = 30;
        protected bool Collided = false;

        public Vector2 velocity
        {
            get
            {
                return Velocity;
            }
            set
            {
                Velocity = value;
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
        public bool moveBall
        {
            get
            {
                return MoveBall;
            }
            set
            {
                MoveBall = value;
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
        public AimBall(Vector2 velocity, Vector2 ballcenter, Pen pen, Brush brush)
        {
            Diameter = 30;
            Velocity = velocity;
            BallCenter = new Vector2(ballcenter.X, ballcenter.Y);
            Pen = pen;
            Brush = brush;
        }
        public void ShowBall(PaintEventArgs e)
        {
            RectangleF rect = new RectangleF((float)BallCenter.X - Diameter / 2, (float)BallCenter.Y - Diameter / 2, Diameter, Diameter);
            e.Graphics.DrawEllipse(Pen, rect);
            e.Graphics.FillEllipse(Brush, rect);
            e.Graphics.DrawLine(new Pen(Color.White, 2f), (int)ballCenter.X, (int)ballCenter.Y, (int)ballCenter.X + (int)Velocity.Scale(0.05).X, (int)ballCenter.Y + (int)Velocity.Scale(0.05).Y);
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
                Velocity.X = -Velocity.X / 2;
                return true;
            }
            if (BallCenter.X >= Game.topLeftX + Game.bottomRightX - Diameter / 2)
            {
                Velocity.X = -Velocity.X / 2;
                return true;
            }
            if (BallCenter.Y <= Game.topLeftY + Diameter / 2)
            {
                Velocity.Y = -Velocity.Y / 2;
                return true;
            }
            if (BallCenter.Y >= Game.topLeftY + Game.bottomRightY - Diameter / 2)
            {
                Velocity.Y = -Velocity.Y / 2;
                return true;
            }
            else
            {
                Collided = false;
                return false;
            }
        }
        public void ChangeVelicities(Ball ball)
        {

            /*
            double a = (Velocity - ball.Velocity).DotProduct((BallCenter - ball.BallCenter));
            a = a / (BallCenter - ball.BallCenter).Length();
            Vector3 NewVelocity1 = Velocity - (BallCenter - ball.BallCenter).Scale(a);

            double b = (ball.Velocity - Velocity).DotProduct((ball.BallCenter - BallCenter));
            b = b / (ball.BallCenter - BallCenter).Length();
            Vector3 NewVelocity2 = ball.Velocity - (ball.BallCenter - BallCenter).Scale(b);
            

            ball.Velocity.Normalize(NewVelocity2);
            Velocity.Normalize(NewVelocity1);
            
            
            
            Vector3 normal1 = new Vector3();
            normal1.Normalize(BallCenter - ball.BallCenter);
            Vector3 normal2 = new Vector3();
            normal2.Normalize(ball.BallCenter - BallCenter);
            double overlap = ball.Diameter - (BallCenter - ball.BallCenter).Length();
            if (overlap > 0)
            {
                BallCenter += normal1.Scale(overlap * 2);
                ball.BallCenter += normal2.Scale(overlap * 2);
            }

            
            Vector3 centresVector = BallCenter - ball.BallCenter;
            double towardsThem = distAlong(Velocity.X, Velocity.Y, centresVector.X, centresVector.Y);
            double towardsMe = distAlong(ball.Velocity.X, ball.Velocity.Y, centresVector.X, centresVector.Y);

            double myOrtho = distAlong(Velocity.X, Velocity.Y, centresVector.Y, -centresVector.X);
            double theirOrtho = distAlong(ball.Velocity.X, ball.Velocity.Y, centresVector.Y, -centresVector.X);

            Velocity = new Vector3((towardsMe * centresVector.X + myOrtho * centresVector.Y),
                      towardsMe * centresVector.Y + myOrtho * -centresVector.X, 0).Scale(0.1);
            ball.Velocity = new Vector3(towardsThem * centresVector.X + theirOrtho * centresVector.Y,
                      towardsThem * centresVector.Y + theirOrtho * -centresVector.X, 0).Scale(0.1);
            Vector3 ballOneNewVelocity = Velocity + (ball.Velocity - Velocity);
            Vector3 ballTwoNewVelocity = ball.Velocity + (Velocity - ball.Velocity);
            */
            Vector2 centresVector = BallCenter - ball.ballCenter;
            Vector2 ballOnePerpendicular = centresVector.PerpendicularComponent(Velocity);
            Vector2 ballTwoPerpendicular = centresVector.PerpendicularComponent(ball.velocity);
            Vector2 ballTwoPara = centresVector.ParralelComponent(ball.velocity);
            Vector2 ballOneNewVelocity = ballTwoPara + ballOnePerpendicular; //http://sinepost.wordpress.com/category/mathematics/geometry/trigonometry/
            Vector2 ballOnePara = centresVector.ParralelComponent(Velocity);
            Velocity = ballOneNewVelocity;

        }
    }
}
