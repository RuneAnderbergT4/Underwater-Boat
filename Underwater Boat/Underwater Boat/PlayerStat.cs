using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Underwater_Boat
{
    public abstract class PlayerStatBase
    {
        public string Name { get; set; }
        public int PlayerId { get; set; }
        public int Health { get; set; }
        public int Resistance { get; set; }
        public int ExplosionResistence { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }
        public float Size { get; set; }
        public float Fuel { get; set; }
        public float Maxspeed { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Angle { get; set; }
        public Texture2D Texture { get; set; }
        public Team T { get; set; }
        public string Texturestring { get; internal set; }
        public bool isboat { get; set; }
    }
    public class PlayerStat : PlayerStatBase
    {
        
    }

    public enum SubmarineType 
    {
        Standard,
        Aqua,
        X_1,
        Megalodon,
        YellowSubmarine,
    }
    public enum ShipType
    {
        Tradional,
        Pansar,
        Camoflage,
        Carrier,
        Vintage
    }
    class Submarine
    {
        public static PlayerStat Sub(SubmarineType type)
        {
            PlayerStat stats = new PlayerStat();
            
            switch (type)
            {
                case SubmarineType.Standard:
                    stats.PlayerId = 1;
                    stats.Name = "Standard";
                    stats.Health = 50;
                    stats.Resistance = 0;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 10;
                    stats.Speed = 1;
                    stats.Size = 1;
                    stats.Fuel = 50;
                    stats.Texturestring = "submarine";
                    return stats;
                    break;
                case SubmarineType.Aqua:
                    stats.PlayerId = 2;
                    stats.Name = "Aqua";
                    stats.Health = 50;
                    stats.Resistance = 10;
                    stats.ExplosionResistence = 10;
                    stats.Damage = 5;
                    stats.Speed = 2;
                    stats.Size = 1;
                    stats.Fuel = 75;
                    stats.Texturestring = "submarine 2";
                    return stats;
                    break;
                case SubmarineType.X_1:
                    stats.PlayerId = 3;
                    stats.Name = "X-1";
                    stats.Health = 35;
                    stats.Resistance = 5;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 15;
                    stats.Speed = 1;
                    stats.Size = 1;
                    stats.Texturestring = "submarine 3";
                    stats.Fuel = 100;
                    return stats;
                    break;
                case SubmarineType.Megalodon:
                    stats.PlayerId = 4;
                    stats.Name = "Megalodon";
                    stats.Health = 75;
                    stats.Resistance = 0;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 20;
                    stats.Speed = 1;
                    stats.Size = 2;
                    stats.Fuel = 25;
                    stats.Texturestring = "submarine 4";
                    return stats;
                    break;
                case SubmarineType.YellowSubmarine:
                    stats.PlayerId = 5;
                    stats.Name = "Yellow Submarine";
                    stats.Health = 50;
                    stats.Resistance = 5;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 10;
                    stats.Speed = 0.5f;
                    stats.Size = 1;
                    stats.Fuel = 60;
                    stats.Texturestring = "submarine 5";
                    return stats;
                    break; 
            }
            return null;
        }
    }
    class Ship 
    {
        public static PlayerStat ship(ShipType type)
        {
            PlayerStat tradional = new PlayerStat();
            PlayerStat pansar = new PlayerStat();
            PlayerStat camoflage = new PlayerStat();
            PlayerStat carrier = new PlayerStat();
            PlayerStat vintage = new PlayerStat();
            switch(type)
            {
                case ShipType.Tradional:
                    tradional.PlayerId = 1;
                    tradional.Name = "Tradional";
                    tradional.Health = 50;
                    tradional.Resistance = 10;
                    tradional.ExplosionResistence = 10;
                    tradional.Damage = 10;
                    tradional.Speed = 1;
                    tradional.Size = 1;
                    tradional.Fuel = 50;
                    return tradional;
                    break;
                case ShipType.Pansar:
                    pansar.PlayerId = 2;
                    pansar.Name = "Pansar";
                    pansar.Health = 35;
                    pansar.Resistance = 25;
                    pansar.ExplosionResistence = 20;
                    pansar.Damage = 10;
                    pansar.Speed = 1;
                    pansar.Size = 1;
                    pansar.Fuel = 50;
                    return pansar;
                    break;
                case ShipType.Camoflage:
                    camoflage.PlayerId = 3;
                    camoflage.Name = "Camoflage";
                    camoflage.Health = 50;
                    camoflage.Resistance = 10;
                    camoflage.ExplosionResistence = 10;
                    camoflage.Damage = 6;
                    camoflage.Speed = 2;
                    camoflage.Size = 1;
                    camoflage.Fuel = 80;
                    return camoflage;
                    break;
                case ShipType.Carrier:
                    carrier.PlayerId = 4;
                    carrier.Name = "Carrier";
                    carrier.Health = 75;
                    carrier.Resistance = 10;
                    carrier.ExplosionResistence = 10;
                    carrier.Damage = 10;
                    carrier.Speed = 0.5f;
                    carrier.Size = 2;
                    carrier.Fuel = 50;
                    return carrier;
                    break;
                case ShipType.Vintage:
                    vintage.PlayerId = 5;
                    vintage.Name = "Vintage";
                    vintage.Health = 50;
                    vintage.Resistance = 0;
                    vintage.ExplosionResistence = 0;
                    vintage.Damage = 20;
                    vintage.Speed = 1.5f;
                    vintage.Size = 1;
                    vintage.Fuel = 50;
                    return vintage;
                    break;
            }
            return null;
        }
    }
}
