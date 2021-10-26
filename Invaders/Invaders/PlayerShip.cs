using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class PlayerShip
    {
        Bitmap image = new Bitmap(Properties.Resources.player);

        public bool Alive { get; set; }
        public Point Location { get; private set; }
        public Rectangle Area { get { return new Rectangle(Location, image.Size); } }

        public PlayerShip(Point location)
        {
            Location = location;
            Alive = true;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, Area);
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Location = new Point(Location.X - 1, Location.Y);
                    //Location.X -= 1;
                    break;
                case Direction.Right:
                    Location = new Point(Location.X + 1, Location.Y);
                    //Location.X += 1;
                    break;
                default:
                    break;
            }
        }
    }
}
