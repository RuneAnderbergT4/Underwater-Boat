using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Underwater_Boat
{
    class Sub 
    {
        Texture2D Texture;
        Team Team;
        SubType subtype;
        bool isBot;
        float HP;
        float dmgMultiplyer;
        float maxspeed;
        float armor;
        float fuel;
        Vector2 speed;
        Vector2 velocity = new Vector2(0,0);

        public Vector2 position { get; private set; }

        public Sub(Team Team,SubType subtype,bool isBot)
        {
           
            this.Team = Team;
            this.subtype = subtype;
            this.isBot = isBot;
            
            
        }
        public new void Initialize()
        {
            switch (subtype)
            {
                case SubType.Highdmg:
                    HP = 50;
                    dmgMultiplyer = 2.5f;
                    armor = 30;
                    maxspeed = 5;
                    break;
                case SubType.Light:
                    HP = 100;
                    dmgMultiplyer = 1f;
                    armor = 20;
                    maxspeed = 8;
                    break;
                case SubType.Heavy:
                    HP = 200;
                    dmgMultiplyer = 0.5f;
                    armor = 50;
                    maxspeed = 3;
                    break;
                    
            }
            fuel = 300888;
            position = new Vector2(500,500);
            
        }

        
        public new void LoadContent(Game Game1)
        {
            switch (subtype)
            {
                case SubType.Highdmg:
                    Texture = Game1.Content.Load<Texture2D>("2");
                    break;
                case SubType.Light:
                    Texture = Game1.Content.Load<Texture2D>("2");
                    break;
                case SubType.Heavy:
                    Texture = Game1.Content.Load<Texture2D>("2");
                    break;
            }
        }
        

       

        
        public  void Update(KeyboardState ks)
        {
            float temp = fuel;
            

            if (ks.IsKeyDown(Keys.W)&& ks.IsKeyUp(Keys.S) && fuel >0)
            {
                if(velocity.Length() < maxspeed/2)
                velocity.Y -= 0.1f * maxspeed;
                fuel -= 0.1f;
            }
            if (ks.IsKeyUp(Keys.W) && ks.IsKeyDown(Keys.S) && fuel > 0)
            {
                if (velocity.Length() < maxspeed/2)
                    velocity.Y += 0.1f * maxspeed;
                fuel -= 0.1f;
            }
            if (ks.IsKeyUp(Keys.A) && ks.IsKeyDown(Keys.D) && fuel > 0)
            {
                if (velocity.Length() < maxspeed)
                    velocity.X += 0.02f * maxspeed;
                fuel -= 0.1f;
            }
            if (ks.IsKeyUp(Keys.D) && ks.IsKeyDown(Keys.A)&& fuel > 0)
            {
                if (velocity.Length() < maxspeed)
                    velocity.X-= 0.02f * maxspeed;
                fuel -= 0.1f;
            }
            if (temp == fuel)
            {
                Vector2 temp2 = new Vector2(velocity.X, velocity.Y);
                if(temp2 != new Vector2(0,0))
                temp2.Normalize();
                velocity.X -= temp2.X * 0.05f;
                velocity.Y -= temp2.Y * 0.04f;
                if(velocity.Length()< 0.1f)
                {
                    velocity = new Vector2(0,0);
                }
            }
            position += velocity;
        }

        
       public  void Draw()
        {
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(Texture,position,Color.White);
            Game1.spriteBatch.End();
        }

    }
}
