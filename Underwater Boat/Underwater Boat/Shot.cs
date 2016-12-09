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
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public string Texture { get; set; }
        public Shot(Vector2 Position, Vector2 Speed,string Texture)
        {
            this.Position = Position;
            this.Speed = Speed;
            this.Texture = Texture;
        }

    }
}
