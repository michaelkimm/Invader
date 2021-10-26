using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    public class BugControl : PictureBox
    {
        Timer animationTimer = new Timer();
        public BugControl()
        {
            animationTimer.Tick += new EventHandler(animationTimer_Tick);
            animationTimer.Interval = 150;
            animationTimer.Start();
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        int cell = 0;
        void animationTimer_Tick(object sender, EventArgs e)
        {
            cell++;
            switch (cell)
            {
                case 1: BackgroundImage = Properties.Resources.bug1; break;
                case 2: BackgroundImage = Properties.Resources.bug2; break;
                case 3: BackgroundImage = Properties.Resources.bug3; break;
                case 4: BackgroundImage = Properties.Resources.bug4; break;
                default:
                    BackgroundImage = Properties.Resources.bug1;
                    cell = 1;
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                animationTimer.Dispose();
        }
    }
}
