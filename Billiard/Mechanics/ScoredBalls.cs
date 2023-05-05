using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billiard.Balls;

namespace Billiard
{
    public class ScoredBalls
    {
        List<Ball> FirstPlayerBalls = new List<Ball>();
        List<Ball> SecondPlayerBalls = new List<Ball>();
        List<Ball> places1 = new List<Ball>();
        List<Ball> places2 = new List<Ball>();
        public void GeneratePlaces()
        {
            for (int i = 0; i < 10; i++)
            {
                Ball ball1 = new Ball(new Vector2(0, 0), new Vector2(Game.topLeftX - 200 - Ball.Diameter / 2, 30 + Game.topLeftY + 50 * i + 1 - Ball.Diameter / 2), new Pen(Color.DimGray, 4f), new SolidBrush(Color.DimGray));
                places1.Add(ball1);
            }
            for (int i = 0; i < 10; i++)
            {
                Ball ball2 = new Ball(new Vector2(0, 0), new Vector2(Game.topLeftX + Game.bottomRightX + 200 - Ball.Diameter / 2, 30 + Game.topLeftY + 50 * i + 1 - Ball.Diameter / 2), new Pen(Color.DimGray, 4f), new SolidBrush(Color.DimGray));
                places2.Add(ball2);
            }
        }
        public void FirstPlayerScored(Ball ball)
        {
            ball.ballCenter = new Vector2(Game.topLeftX - 200, 30 + Game.topLeftY + 50 * FirstPlayerBalls.Count + 1);
            FirstPlayerBalls.Add(ball);
        }
        public void SecondPlayerScored(Ball ball)
        {
            ball.ballCenter = new Vector2(Game.topLeftX + Game.bottomRightX + 200, 30 + Game.topLeftY + 50 * SecondPlayerBalls.Count + 1);
            SecondPlayerBalls.Add(ball);
        }
        public void ShowScoredBalls(PaintEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                places1[i].ShowBall(e);
                places2[i].ShowBall(e);
            }
            for (int i = 0; i < FirstPlayerBalls.Count; ++i)
            {
                FirstPlayerBalls[i].ShowBall(e);
            }
            for (int i = 0; i < SecondPlayerBalls.Count; ++i)
            {
                SecondPlayerBalls[i].ShowBall(e);
            }
        }
    }
}
