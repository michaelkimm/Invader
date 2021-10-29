using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class Stars
    {
        struct Star
        {
            public Point point;
            public Pen pen;
            const float radius = 1;

            public Star(Point point, Pen pen)
            {
                this.point = point;
                this.pen = pen;
            }

            public void Draw(Graphics g)
            {
                g.DrawEllipse(pen, new Rectangle((int)(point.X - radius * 0.5), (int)(point.Y - radius * 0.5), (int)(2 * radius), (int)(2 * radius)));
            }
        }

        List<Star> starList = new List<Star>();
        Random random = new Random();
        const int STAR_COUNT = 300;
        int twinkleCount = 5;
        Rectangle boundary;

        public Stars (Rectangle border)
        {
            boundary = border;
            initializeStar();
        }
        

        public void Draw(Graphics g)
        {
            for (int i = 0; i < STAR_COUNT; i++)
            {
                //starList[i]
                starList[i].Draw(g);
            }
        }

        public void Twinkle()
        {
            starList.RemoveRange(starList.Count - twinkleCount, twinkleCount);
            for (int i = 0; i < twinkleCount; i++)
            {
                makeRandStar();
            }
        }
        
        void initializeStar()
        {
            for (int i = 0; i < STAR_COUNT; i++)
            {
                makeRandStar();
            }
        }

        Pen RandomPen(Random random)
        {
            return new Pen(Color.FromArgb(255, 255 - random.Next(0, 256), 255 - random.Next(0, 256), 255 - random.Next(0, 256)));
        }

        void makeRandStar()
        {
            starList.Add(new Star(new Point(random.Next() % (boundary.Right - boundary.Left), random.Next() % (boundary.Bottom - boundary.Top)),
                             RandomPen(random)));
        }
    }
}
