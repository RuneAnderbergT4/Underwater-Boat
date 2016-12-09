﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public class Weapon
    {
        public string Name { get; private set; }
        
        public string Texture { get; private set; }
        public int CurrentAmmo { get; set; }
        public int ShotsFired { get; set; }

        public Weapon()
        {
            Name = "";
            Texture = "Skott";
            CurrentAmmo = 50;
            ShotsFired = 1;
        }
    }
}
