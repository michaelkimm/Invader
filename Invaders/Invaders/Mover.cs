using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    abstract class Mover
    {
        public Point Location { get; protected set; }

        // public Rectangle Area { get { return new Rectangle(new Point(Location.X - image.Size.Width / 2, Location.Y - image.Size.Height / 2), image.Size); } }

        //protected float speed = 1;

        abstract public float Speed { get; } 

        protected Rectangle gameBoundaries;


        public Mover(Point location, Rectangle gameBoundaries)
        {
            Location = location;
            this.gameBoundaries = gameBoundaries;
        }

        public abstract void Draw(Graphics g);

        public virtual bool Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Location = new Point((int)(Location.X - Speed), Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point((int)(Location.X + Speed), Location.Y);
                    break;
                case Direction.Up:
                    Location = new Point(Location.X, (int)(Location.Y - Speed));
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, (int)(Location.Y + Speed));
                    break;
                default:
                    break;
            }

            if (gameBoundaries.Contains(Location))
                return true;
            else
                return false;
        }
    }
}
