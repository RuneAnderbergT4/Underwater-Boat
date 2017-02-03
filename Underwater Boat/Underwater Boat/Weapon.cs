using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public enum WeaponType
    {
        Nuke,
        Torpedo
    }
    public enum Direction
    {
        Up,
        Forward
    }
    public class Weapon
    {
        public string Name { get; private set; }
        
        public string Texture { get; private set; }
        public int CurrentAmmo { get; set; }
        public int MaxAmmo { get; set; }
        public int ShotsFired { get; set; }
        public Direction direction;

        public WeaponType weapon;

        public Weapon(WeaponType weapon)
        {
            this.weapon = weapon;
            switch (weapon)
            {
                case WeaponType.Torpedo:
                    direction = Direction.Forward;
                    Name = weapon.ToString();
                    Texture = "Skott";
                    MaxAmmo = 10;
                    CurrentAmmo = 10;
                    ShotsFired = 1;
                    break;
                case WeaponType.Nuke:
                    Name = weapon.ToString();
                    Texture = "Skott";
                    CurrentAmmo = 3;
                    MaxAmmo = 3;
                    ShotsFired = 1;
                    break;
            }
            
            
        }
    }
}
