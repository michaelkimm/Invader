using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    abstract class Ship : Mover, IShooter
    {
        protected Bitmap image; // = new Bitmap(Properties.Resources.player);
        protected DateTime dateTime { get { return DateTime.Now; } }
        protected DateTime deadTime;
        protected float deadTimeSpan { get { return dateTime.Second - deadTime.Second; } }

        bool alive = true;
        public virtual bool Alive
        {
            get { return alive; }
            set
            {
                alive = value;
                deadTime = dateTime;
            }
        }


        public Rectangle Area { get { return new Rectangle(new Point(Location.X - image.Size.Width / 2, Location.Y - image.Size.Height / 2), image.Size); } }

        public Ship(Point location, Rectangle gameBoundaries)
            : base(location, gameBoundaries)
        {
            Alive = true;
        }

        public override void Draw(Graphics g)
        {
            if (!Alive)
                return;

            if (image == null)
                return;
            g.DrawImage(image, Area);
        }
        public virtual void Draw(Graphics g, int animationCell) { }

        public abstract Shot FireShot();
        public abstract Shot FireShot(Shot reusableShot);
    }
}
