using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    public enum ShipType { Bug, Saucer, Satellite, Spaceship, Star }

    class Invader : Ship
    {
        const int HORIZONTAL_INTERVAL = 10;
        const int VERTICAL_INTERVAL = 40;

        public override float Speed { get { return HORIZONTAL_INTERVAL; } }

        public ShipType InvaderType { get; private set; }

        //public Rectangle Area
        //{
        //    get { return new Rectangle(Location, image.Size); }
        //}

        public int Score
        {
            get
            {
                int result = 0;
                switch (InvaderType)
                {
                    case ShipType.Bug:
                        result = 10;
                        break;
                    case ShipType.Saucer:
                        result = 20;
                        break;
                    case ShipType.Satellite:
                        result = 30;
                        break;
                    case ShipType.Spaceship:
                        result = 40;
                        break;
                    case ShipType.Star:
                        result = 50;
                        break;
                    default:
                        break;
                }
                return result;
            }
        }

        public Invader(Point location, Rectangle gameBoundaries, ShipType invaderType)
            : base(location, gameBoundaries)
        {
            this.InvaderType = invaderType;
            // image = InvaderImage(0);
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
                case Direction.Down:
                    Location = new Point(Location.X, (int)(Location.Y + VERTICAL_INTERVAL));
                    break;
                default:
                    break;
            }

            if (gameBoundaries.Contains(Location))
                return true;
            else
                return false;
        }

        Bitmap InvaderImage(int animationCell)
        {
            Bitmap returnImage;

            if (animationCell > 3)
                animationCell = 0;
            switch (InvaderType)
            {
                case ShipType.Bug:
                    switch (animationCell)
                    {
                        case 0:
                            returnImage = Properties.Resources.bug1;
                            break;
                        case 1:
                            returnImage = Properties.Resources.bug2;
                            break;
                        case 2:
                            returnImage = Properties.Resources.bug3;
                            break;
                        case 3:
                            returnImage = Properties.Resources.bug4;
                            break;
                        default:
                            returnImage = Properties.Resources.bug1;
                            break;
                    }
                    break;
                case ShipType.Saucer:
                    switch (animationCell)
                    {
                        case 0:
                            returnImage = Properties.Resources.flyingsaucer1;
                            break;
                        case 1:
                            returnImage = Properties.Resources.flyingsaucer2;
                            break;
                        case 2:
                            returnImage = Properties.Resources.flyingsaucer3;
                            break;
                        case 3:
                            returnImage = Properties.Resources.flyingsaucer4;
                            break;
                        default:
                            returnImage = Properties.Resources.flyingsaucer1;
                            break;
                    }
                    break;
                case ShipType.Satellite:
                    switch (animationCell)
                    {
                        case 0:
                            returnImage = Properties.Resources.satellite1;
                            break;
                        case 1:
                            returnImage = Properties.Resources.satellite2;
                            break;
                        case 2:
                            returnImage = Properties.Resources.satellite3;
                            break;
                        case 3:
                            returnImage = Properties.Resources.satellite4;
                            break;
                        default:
                            returnImage = Properties.Resources.satellite1;
                            break;
                    }
                    break;
                case ShipType.Spaceship:
                    switch (animationCell)
                    {
                        case 0:
                            returnImage = Properties.Resources.spaceship1;
                            break;
                        case 1:
                            returnImage = Properties.Resources.spaceship2;
                            break;
                        case 2:
                            returnImage = Properties.Resources.spaceship3;
                            break;
                        case 3:
                            returnImage = Properties.Resources.spaceship4;
                            break;
                        default:
                            returnImage = Properties.Resources.spaceship1;
                            break;
                    }
                    break;
                case ShipType.Star:
                    switch (animationCell)
                    {
                        case 0:
                            returnImage = Properties.Resources.star1;
                            break;
                        case 1:
                            returnImage = Properties.Resources.star2;
                            break;
                        case 2:
                            returnImage = Properties.Resources.star3;
                            break;
                        case 3:
                            returnImage = Properties.Resources.star4;
                            break;
                        default:
                            returnImage = Properties.Resources.star1;
                            break;
                    }
                    break;
                default:
                    returnImage = Properties.Resources.bug1;
                    break;
            }
            
            return returnImage;
        }

        public override void Draw(Graphics g, int animationCell)
        {
            if (!Alive)
                return;

            image = InvaderImage(animationCell);
            base.Draw(g);
        }

        public Shot FireShot()
        {
            return new EnemyShot(Location, Direction.Down, gameBoundaries);
        }
    }
}
