using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billiard;

namespace Billiard
{
    public static class Aim
    {
        public static void DrawAim(Vector2 mouseLocation, Vector2 ballCenter, PaintEventArgs e, List<Ball> balls)
        {
            Vector2 direction = ballCenter - mouseLocation;
            Vector2 center = ballCenter;
            if (direction.Length() > 1)
            {
                direction.Normalize(direction);
                direction = direction.Scale(1500);
                AimBall aimBall = new AimBall(direction, center, new Pen(Color.White, 2f), new SolidBrush(Color.Transparent));
                while (true)
                {
                    bool a = false;
                    aimBall.ballCenter = center;
                    aimBall.collided = false;
                    for (int i = 1; i < balls.Count(); ++i)
                    {
                        if (aimBall.DetectCollision(balls[i]))
                        {
                            if (aimBall.collided) aimBall.ChangeDirections(balls[i]);
                            aimBall.ShowBall(e);
                            a = true;
                            break;
                        }
                    }
                    if (a)
                    {
                        break;
                    }
                    center += direction.Scale(0.001);
                }
                e.Graphics.DrawLine(new Pen(Color.White, 2f), (int)ballCenter.X, (int)ballCenter.Y, (int)center.X, (int)center.Y);
            }
        }
    }
}
