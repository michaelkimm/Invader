using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class Shot
    {
        const int moveInterval = 20;
        const int width = 5;
        const int height = 15;

        public Point Location { get; private set; }

        Direction direction;
        Rectangle gameBoundaries;   

        public void Draw(Graphics g)
        {

        }

        public bool Move()
        {
            return true;
        }
    }
}
