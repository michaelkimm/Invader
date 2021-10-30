using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invaders
{
    interface IShooter
    {
        Shot FireShot();

        Shot FireShot(Shot reusableShot);
    }
}
