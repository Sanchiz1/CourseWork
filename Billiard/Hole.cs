using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiard
{
    public class Hole
    {
        private Vector2 Position = new Vector2(0, 0);
        private int Diameter = 50;
        private Pen Pen;
        private Brush Brush;
        public Vector2 position
        {
            get
            {
                return Position;
            }
            set
            {
                Position = value;
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

        public Hole(Vector2 position, Pen pen, Brush brush)
        {
            Position = new Vector2(position.X, position.Y);
            Pen = pen;
            Brush = brush;
        }
        public void ShowHole(PaintEventArgs e)
        {
            RectangleF outerRectRed = new RectangleF((float)Position.X - Diameter / 2, (float)Position.Y - Diameter / 2, Diameter, Diameter);
            e.Graphics.DrawEllipse(Pen, outerRectRed);
            e.Graphics.FillEllipse(Brush, outerRectRed);
        }
        public bool DetectCollision(Ball ball)
        {
            Vector2 distanceToBall = Position - ball.ballCenter;
            float distance = (float)distanceToBall.Length();
            if (distance <= Ball.Diameter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
