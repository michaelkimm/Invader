using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    abstract class Shot : Mover
    {
        protected const int WIDTH = 5;
        protected const int HEIGHT = 15;

        //public bool IsEnabled { get; set; }

        Direction direction;

        public Shot(Point location, Direction direction, Rectangle gameBoundaries)
            : base(location, gameBoundaries)
        {
            this.direction = direction;
        }

        public bool Move()
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

    class PlayerShot : Shot
    {
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);

        protected const int MOVE_INTERVAL = 20;
        override public float Speed { get { return MOVE_INTERVAL; } }

        public PlayerShot(Point location, Direction direction, Rectangle gameBoundaries)
            : base(location, direction, gameBoundaries)
        {
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(myBrush, new Rectangle((int)(Location.X - WIDTH * 0.5f), (int)(Location.Y - HEIGHT * 0.5), WIDTH, HEIGHT));
            //g.FillRectangle()
        }
    }

    class EnemyShot : Shot
    {
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

        protected const int MOVE_INTERVAL = 10;
        override public float Speed { get { return MOVE_INTERVAL; } }

        public EnemyShot(Point location, Direction direction, Rectangle gameBoundaries)
            : base(location, direction, gameBoundaries)
        {
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(myBrush, new Rectangle((int)(Location.X - WIDTH * 0.5f), (int)(Location.Y - HEIGHT * 0.5), WIDTH, HEIGHT));
            //g.FillRectangle()
        }
    }
}
