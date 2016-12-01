using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public class PlayerStat
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
        public Vector2 Position { get; set; }
        public Vector2 Angle { get; set; }
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
            PlayerStat standard = new PlayerStat();
            PlayerStat aqua = new PlayerStat();
            PlayerStat x_1 = new PlayerStat();
            PlayerStat megalodon = new PlayerStat();
            PlayerStat yellowSubmarine = new PlayerStat();
            switch (type)
            {
                case SubmarineType.Standard:
                    standard.PlayerId = 1;
                    standard.Name = "Standard";
                    standard.Health = 50;
                    standard.Resistance = 0;
                    standard.ExplosionResistence = 0;
                    standard.Damage = 10;
                    standard.Speed = 1;
                    standard.Size = 1;
                    standard.Fuel = 50;
                    return standard;
                    break;
                case SubmarineType.Aqua:
                    aqua.PlayerId = 2;
                    aqua.Name = "Aqua";
                    aqua.Health = 50;
                    aqua.Resistance = 10;
                    aqua.ExplosionResistence = 10;
                    aqua.Damage = 5;
                    aqua.Speed = 2;
                    aqua.Size = 1;
                    aqua.Fuel = 75;
                    return aqua;
                    break;
                case SubmarineType.X_1:
                    x_1.PlayerId = 3;
                    x_1.Name = "X-1";
                    x_1.Health = 35;
                    x_1.Resistance = 5;
                    x_1.ExplosionResistence = 0;
                    x_1.Damage = 15;
                    x_1.Speed = 1;
                    x_1.Size = 1;
                    x_1.Fuel = 100;
                    return x_1;
                    break;
                case SubmarineType.Megalodon:
                    megalodon.PlayerId = 4;
                    megalodon.Name = "Megalodon";
                    megalodon.Health = 75;
                    megalodon.Resistance = 0;
                    megalodon.ExplosionResistence = 0;
                    megalodon.Damage = 20;
                    megalodon.Speed = 1;
                    megalodon.Size = 2;
                    megalodon.Fuel = 25;
                    return megalodon;
                    break;
                case SubmarineType.YellowSubmarine:
                    yellowSubmarine.PlayerId = 5;
                    yellowSubmarine.Name = "Yellow Submarine";
                    yellowSubmarine.Health = 50;
                    yellowSubmarine.Resistance = 5;
                    yellowSubmarine.ExplosionResistence = 0;
                    yellowSubmarine.Damage = 10;
                    yellowSubmarine.Speed = 0.5f;
                    yellowSubmarine.Size = 1;
                    yellowSubmarine.Fuel = 60;
                    return yellowSubmarine;
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
