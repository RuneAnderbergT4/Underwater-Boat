using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public static class Projectiles
    {
        
        public static List<Shot> projectiles;
        static Dictionary< Weap, Texture2D> Tex;
        public static void LoadContent(Game game)
        {
            projectiles = new List<Shot>();
            Tex = new Dictionary<Weap,Texture2D>();
            Tex.Add(Weap.Nuke, game.Content.Load<Texture2D>("2"));
            Tex.Add(Weap.torpedo, game.Content.Load<Texture2D>("2"));
        }
        public static void Add(Shot shot)
        {
            projectiles.Add(shot);
        }
        public static void Update()
        {

            foreach (Shot s in projectiles)
                s.Position += s.Velocity;
        }
        public static void Draw()
        {
            foreach (Shot s in projectiles)
                Game1.spriteBatch.Draw(Tex[s.Weapon], s.Position, Color.White);
        }

    }
}
