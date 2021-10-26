using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    public enum ShipType { Bug, Saucer, Satellite, Spaceship, Star }

    class Invader
    {
        const int HorizontalInterval = 10;
        const int VerticalInterval = 40;

        Bitmap image;

        public Point Location { get; private set; }
        public ShipType InvaderType { get; private set; }

        public Rectangle Area
        {
            get { return new Rectangle(Location, image.Size); }
        }

        public int Score { get; private set; }

        public Invader(ShipType invaderType, Point location, int score)
        {
            this.InvaderType = invaderType;
            this.Location = location;
            this.Score = score;
            image = InvaderImage(0);
        }

        public void Draw(Graphics g)
        {

        }

        public bool Move()
        {
            return true;
        }

        Bitmap InvaderImage(int animationCell)
        {

        }
    }
}
