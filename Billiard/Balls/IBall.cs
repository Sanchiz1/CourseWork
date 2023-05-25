using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiard
{
    public interface IBall
    {
        public void ShowBall(PaintEventArgs e);
        public bool DetectCollision(Ball ball);
        public void ChangeDirections(Ball ball);
    }
}
