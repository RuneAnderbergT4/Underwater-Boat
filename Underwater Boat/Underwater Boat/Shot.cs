using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public class Shot
    {
        public Weap Weapon;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Texture2D Texture { get; set; }
        public Shot(Vector2 Position, Vector2 Speed,Weap Weapon)
        {
            this.Position = Position;
            this.Velocity = Speed;
            this.Weapon  = Weapon;
        }
        
    }
}
