using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Invaders
{
    static class InvaderUtility
    {
        public static IEnumerable<String> GetImageList(ShipType shipType)
        {
            string name;
            switch (shipType)
            {
                case ShipType.Bug:
                    name = "bug";
                    break;
                case ShipType.Saucer:
                    name = "flyingsaucer";
                    break;
                case ShipType.Satellite:
                    name = "satellite";
                    break;
                case ShipType.Spaceship:
                    name = "spaceship";
                    break;
                default:
                    // star
                    name = "star";
                    break;
            }

            List<string> imageList = new List<string>();

            for (int i = 1; i <= 4; i++)
                imageList.Add(name + i.ToString() + ".png");

            return imageList;
        }

        public static Bitmap GetImage(string fileName)
        {
            Console.WriteLine(Application.StartupPath + "\\Assets\\" + fileName);
            return new Bitmap(Application.StartupPath + "\\Assets\\" + fileName);
        }

        public static Bitmap[] GetImageFromFactory(ShipType shipType)
        {

            IEnumerable<string> fileNameEnumerable = GetImageList(shipType);

            Bitmap[] bitmaps = new Bitmap[fileNameEnumerable.ToList().Count];

            for (int i = 0; i < bitmaps.Length; i++)
            {
                bitmaps[i] = GetImage(fileNameEnumerable.ElementAt(i));
            }

            return bitmaps;
        }
    }
}
