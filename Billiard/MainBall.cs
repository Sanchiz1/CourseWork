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
    public class MainBall : Ball
    {
        protected int MaxVelocity = 300;
        public MainBall(Vector2 velocity, Vector2 ballcenter, Pen pen, Brush brush) : base(velocity, ballcenter, pen, brush)
        {
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
    }
}
