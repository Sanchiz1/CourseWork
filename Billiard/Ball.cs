using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Billiard
{
    public class Ball
    {
        private Vector2 Velocity = new Vector2(0, 0);
        private Vector2 BallCenter = new Vector2(0, 0);
        private double Speed;
        private Pen Pen;
        private Brush Brush;
        private bool MoveBall = true;
        private int Diameter = 30;
        private int MaxVelocity = 300;
        
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
        public int diameter
        {
            get
            {
                return Diameter;
            }
            set
            {
                Diameter = value;
            }
        }
        public Ball(Vector2 velocity, Vector2 ballcenter, Pen pen, Brush brush) {
            Diameter = 30;
            Velocity = velocity;
            BallCenter = new Vector2(ballcenter.X + Diameter / 2, ballcenter.Y + Diameter / 2);
            Pen = pen;
            Brush = brush;
            Speed = 1;
        }
        public void ShowBall(PaintEventArgs e)
        {
            RectangleF outerRectRed = new RectangleF((float)BallCenter.X - Diameter / 2, (float)BallCenter.Y - Diameter / 2, Diameter, Diameter);
            e.Graphics.DrawEllipse(Pen, outerRectRed);
            e.Graphics.FillEllipse(Brush, outerRectRed);
        }
        public void MoveTheBall(float topLeftX, float topLeftY, float bottomRightX, float bottomRightY)
        {
            if (MoveBall == true)
            {
                BallCenter.X += Velocity.X * Speed;
                BallCenter.Y += Velocity.Y * Speed;

                Velocity.X *= 0.991;
                Velocity.Y *= 0.991;
                if ((Velocity.X < 0.01 && Velocity.X > -0.01) && (Velocity.Y < 0.01 && Velocity.Y > -0.01))
                {
                    Velocity.X = 0;
                    Velocity.Y = 0;
                }

                if (BallCenter.X <= topLeftX + Diameter / 2)
                {
                    Velocity.X = -Velocity.X / 2;
                    BallCenter.X = topLeftX + Diameter / 2;
                }
                if (BallCenter.X >= topLeftX + bottomRightX - Diameter / 2)
                {
                    Velocity.X = -Velocity.X / 2;
                    BallCenter.X = topLeftX + bottomRightX - Diameter / 2;
                }
                if (BallCenter.Y <= topLeftY + Diameter / 2)
                {
                    Velocity.Y = -Velocity.Y / 2;
                    BallCenter.Y = topLeftY + Diameter / 2;
                }
                if (BallCenter.Y >= topLeftY + bottomRightY - Diameter / 2)
                {
                    Velocity.Y = -Velocity.Y / 2;
                    BallCenter.Y = topLeftY + bottomRightY - Diameter / 2;
                }
            }
        }
        public void SetSpeed(Vector2 mouseLocation, float topLeftX, float topLeftY, float bottomRightX, float bottomRightY)
        {
            Vector2 v = new Vector2();
            float length;
            if ((BallCenter - mouseLocation).Length() > MaxVelocity)
            {
                v.Normalize((BallCenter - mouseLocation));
                Velocity.X = v.X;
                Velocity.Y = v.Y;
                Speed = MaxVelocity / 20;
                length = 1;
            }
            else
            {
                Velocity.X = (BallCenter.X - mouseLocation.X);
                Velocity.Y = (BallCenter.Y - mouseLocation.Y);
                length = (float)Velocity.Length();
                Speed = length / 20;
            }

            if (length != 0)
            {
                Velocity.X = Velocity.X / length;
                Velocity.Y = Velocity.Y / length;
            }
            MoveBall = false;
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
        public void ChangeVelicities(Ball ball) {
            Sound.MakeSound("sound1.wav");

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

            if (Velocity.Length() > ball.Velocity.Length())
            {
                ball.Speed = Speed;
                Speed /= 2;
            }
            else if (Velocity.Length() < ball.Velocity.Length())
            {
                Speed = ball.Speed;
                ball.Speed /= 2;
            }
            Vector2 normal1 = new Vector2();
            normal1.Normalize(BallCenter - ball.BallCenter);
            Vector2 normal2 = new Vector2();
            normal2.Normalize(ball.BallCenter - BallCenter);
            double overlap = ball.Diameter - (BallCenter - ball.BallCenter).Length();
            BallCenter += normal1.Scale((overlap + 1) / 2);
            ball.BallCenter += normal2.Scale((overlap + 1) / 2);
            Vector2 centresVector = BallCenter - ball.BallCenter;
            Vector2 ballOnePerpendicular = centresVector.PerpendicularComponent(Velocity);
            Vector2 ballTwoPerpendicular = centresVector.PerpendicularComponent(ball.Velocity);
            Vector2 ballTwoPara = centresVector.ParralelComponent(ball.Velocity);
            Vector2 ballOneNewVelocity = ballTwoPara + ballOnePerpendicular; //http://sinepost.wordpress.com/category/mathematics/geometry/trigonometry/
            Vector2 centresVector2 = ball.BallCenter - BallCenter;
            Vector2 ballOnePara = centresVector.ParralelComponent(Velocity);
            Vector2 ballTwoNewVelocity = ballOnePara + ballTwoPerpendicular;
            ball.Velocity = ballTwoNewVelocity;
            Velocity = ballOneNewVelocity;
        }
    }
}
