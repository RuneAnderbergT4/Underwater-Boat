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
        public float MaxFuel { get; set; }
        public float Fuel { get; set; }
        public float Maxspeed { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Angle { get; set; }
        public Texture2D Texture { get; set; }
        public Team T { get; set; }
        public int TextureID { get; internal set; }
        public bool IsBoat { get; set; }
        public Color Color { get; set; }
    }

    public class PlayerStat : PlayerStatBase
    {
        
    }

    public enum SubType 
    {
        Standard,
        Aqua,
        X_1,
        Megalodon,
        YellowSubmarine,
        ShipTradional,
        ShipPansar,
        ShipCamoflage,
        ShipCarrier,
        ShipVintage
    }
   
    class Submarine
    {
        private static List<Texture2D> tex = new List<Texture2D>();

        public static void LoadContent(Game game)
        {
            tex = new List<Texture2D>();
            tex.Add(game.Content.Load<Texture2D>("submarine"));
            tex.Add(game.Content.Load<Texture2D>("submarine 2"));
            tex.Add(game.Content.Load<Texture2D>("submarine 3"));
            tex.Add(game.Content.Load<Texture2D>("submarine 4"));
            tex.Add(game.Content.Load<Texture2D>("submarine 5"));

        }

        public static PlayerStat Sub(SubType type)
        {
            PlayerStat stats = new PlayerStat();
            stats.IsBoat = false;
            switch (type)
            {
                case SubType.Standard:
                    stats.PlayerId = 1;
                    stats.Name = "Standard";
                    stats.Health = 50;
                    stats.Resistance = 0;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 10;
                    stats.Speed = 1;
                    stats.Size = 1;
                    stats.MaxFuel = 50;
                    stats.Texture = tex[0];
                    return stats;
                    break;
                case SubType.Aqua:
                    stats.PlayerId = 2;
                    stats.Name = "Aqua";
                    stats.Health = 50;
                    stats.Resistance = 10;
                    stats.ExplosionResistence = 10;
                    stats.Damage = 5;
                    stats.Speed = 2;
                    stats.Size = 1;
                    stats.MaxFuel = 75;
                    stats.Texture = tex[1];
                    return stats;
                    break;
                case SubType.X_1:
                    stats.PlayerId = 3;
                    stats.Name = "X-1";
                    stats.Health = 35;
                    stats.Resistance = 5;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 15;
                    stats.Speed = 1;
                    stats.Size = 1;
                    stats.Texture = tex[2];
                    stats.MaxFuel = 100;
                    return stats;
                    break;
                case SubType.Megalodon:
                    stats.PlayerId = 4;
                    stats.Name = "Megalodon";
                    stats.Health = 75;
                    stats.Resistance = 0;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 20;
                    stats.Speed = 1;
                    stats.Size = 2;
                    stats.MaxFuel = 25;
                    stats.Texture = tex[3];
                    return stats;
                    break;
                case SubType.YellowSubmarine:
                    stats.PlayerId = 5;
                    stats.Name = "Yellow Antal";
                    stats.Health = 50;
                    stats.Resistance = 5;
                    stats.ExplosionResistence = 0;
                    stats.Damage = 10;
                    stats.Speed = 0.5f;
                    stats.Size = 1;
                    stats.MaxFuel = 60;
                    stats.Texture = tex[4];
                    return stats;
                    break; 
            }
            return null;
        }
    }

    class Ship 
    {
        private static List<Texture2D> tex = new List<Texture2D>();

        public static void LoadContent(Game game)
        {
            tex = new List<Texture2D>();
            tex.Add(game.Content.Load<Texture2D>("ship"));
            tex.Add(game.Content.Load<Texture2D>("ship 2"));
            tex.Add(game.Content.Load<Texture2D>("ship 3"));
            tex.Add(game.Content.Load<Texture2D>("ship 4"));
            tex.Add(game.Content.Load<Texture2D>("ship 5"));

        }

        public static PlayerStat ship(SubType type)
        {
            PlayerStat stat = new PlayerStat();
            stat.IsBoat = false;

            switch (type)
            {
                case SubType.ShipTradional:
                    stat.PlayerId = 1;
                    stat.Name = "Tradional";
                    stat.Health = 50;
                    stat.Resistance = 10;
                    stat.ExplosionResistence = 10;
                    stat.Damage = 10;
                    stat.Speed = 1;
                    stat.Size = 1;
                    stat.MaxFuel = 50;
                    stat.Texture = tex[0];
                    return stat;
                    break;
                case SubType.ShipPansar:
                    stat.PlayerId = 2;
                    stat.Name = "Pansar";
                    stat.Health = 35;
                    stat.Resistance = 25;
                    stat.ExplosionResistence = 20;
                    stat.Damage = 10;
                    stat.Speed = 1;
                    stat.Size = 1;
                    stat.MaxFuel = 50;
                    stat.Texture = tex[1];
                    return stat;
                    break;
                case SubType.ShipCamoflage:
                    stat.PlayerId = 3;
                    stat.Name = "Camoflage";
                    stat.Health = 50;
                    stat.Resistance = 10;
                    stat.ExplosionResistence = 10;
                    stat.Damage = 6;
                    stat.Speed = 2;
                    stat.Size = 1;
                    stat.MaxFuel = 80;
                    stat.Texture = tex[2];
                    return stat;
                    break;
                case SubType.ShipCarrier:
                    stat.PlayerId = 4;
                    stat.Name = "Carrier";
                    stat.Health = 75;
                    stat.Resistance = 10;
                    stat.ExplosionResistence = 10;
                    stat.Damage = 10;
                    stat.Speed = 0.5f;
                    stat.Size = 2;
                    stat.MaxFuel = 50;
                    stat.Texture = tex[3];
                    return stat;
                    break;
                case SubType.ShipVintage:
                    stat.PlayerId = 5;
                    stat.Name = "Vintage";
                    stat.Health = 50;
                    stat.Resistance = 0;
                    stat.ExplosionResistence = 0;
                    stat.Damage = 20;
                    stat.Speed = 1.5f;
                    stat.Size = 1;
                    stat.MaxFuel = 50;
                    stat.Texture = tex[4];
                    return stat;
                    break;
            }
            return null;
        }
    }
}
