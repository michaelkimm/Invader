using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class PlayerShip : Ship
    {
        const int MOVE_INTERVAL = 5;
        const float DEAD_SHIP_HEIGHT = 10;
        const float DYING_TIME = 3;
        
        public override float Speed { get { return MOVE_INTERVAL; } }

        public PlayerShip(Bitmap bitImage, Point location, Rectangle gameBoundaries)
            : base(location, gameBoundaries)
        {
            this.image = bitImage;
        }

        public override void Draw(Graphics g)
        {
            if (Alive)
            {
                base.Draw(g);
            }
            else
            {
                if (deadTimeSpan > DYING_TIME)
                {
                    Alive = true;
                    return;
                }

                Bitmap resizedImage = ResizeImage(image, image.Width, (int)(image.Height - (image.Height - DEAD_SHIP_HEIGHT) / DYING_TIME * deadTimeSpan));
                g.DrawImage(resizedImage, new Rectangle(Location.X, Location.Y, resizedImage.Width, resizedImage.Height));
            }
        }

        Bitmap ResizeImage (Bitmap picture, int width, int height)
        {
            Bitmap resizedPicture = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedPicture))
            {
                graphics.DrawImage(picture, 0, 0, width, height);
            }
            return resizedPicture;
        }

        public override bool Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Location = new Point((int)(Location.X - Speed), Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point((int)(Location.X + Speed), Location.Y);
                    break;
                default:
                    break;
            }

            if (gameBoundaries.Contains(Location))
                return true;
            else
                return false;
        }

        public Shot FireShot()
        {
            return new PlayerShot(Location, Direction.Up, gameBoundaries);
        }

        void Died()
        {
            deadTime = dateTime;
        }
    }
}
