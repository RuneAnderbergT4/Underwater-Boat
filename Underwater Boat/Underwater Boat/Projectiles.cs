using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    static class Projectiles
    {
        public static List<Shot> projectiles;
        public static void Add(Shot shot)
        {
            projectiles.Add(shot);
        }

    }
}
