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
        bool movingrignt;
        public bool gamepad;//= true;
        private bool movingUp;
        private Texture2D red;
        private Texture2D hit;
        private Rectangle collision;
        private bool bHit = false;
        

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
                    maxspeed = 3;
                    break;
                case SubType.Light:
                    HP = 100;
                    dmgMultiplyer = 1f;
                    armor = 20;
                    maxspeed = 5;
                    break;
                case SubType.Heavy:
                    HP = 200;
                    dmgMultiplyer = 0.5f;
                    armor = 50;
                    maxspeed = 1;
                    break;
                    
            }
            fuel = 300888;
            position = new Vector2(500,500);
            velocity = new Vector2(0,0);
        }

        
        public new void LoadContent(Game Game1)
        {
            hit = Game1.Content.Load<Texture2D>("hit");
            red = Game1.Content.Load<Texture2D>("red");

            switch (subtype)
            {
                case SubType.Highdmg:
                    Texture = Game1.Content.Load<Texture2D>("submarine");
                    break;
                case SubType.Light:
                    Texture = Game1.Content.Load<Texture2D>("submarine 2");
                    break;
                case SubType.Heavy:
                    Texture = Game1.Content.Load<Texture2D>("submarine 3");
                    break;
            }
            
        }
        

       

        
        public  void Update(KeyboardState ks,GamePadState gs)
        {
            float temp = fuel;

            if (gamepad)
            {
                if (gs.ThumbSticks.Left.Length() != 0)
                {
                    if (-gs.ThumbSticks.Left.Y < 0)
                    {
                        movingUp = true;
                        if (velocity.Y > -maxspeed / 2)
                            velocity.Y += 0.1f * maxspeed * -gs.ThumbSticks.Left.Y;
                    }
                    if (-gs.ThumbSticks.Left.Y > 0)

                    {
                        movingUp = true;
                        if (velocity.Y < maxspeed / 2)
                            velocity.Y += 0.1f * maxspeed * -gs.ThumbSticks.Left.Y;
                    }
                    if (gs.ThumbSticks.Left.X < 0)
                    {
                        movingrignt = true;
                        if (velocity.X > -maxspeed)
                            velocity.X += 0.02f * maxspeed * gs.ThumbSticks.Left.X;
                    }
                    if (gs.ThumbSticks.Left.X > 0)
                    {
                        movingrignt = true;
                        if (velocity.X < maxspeed)
                            velocity.X += 0.02f * maxspeed * gs.ThumbSticks.Left.X;
                    }
                    if (gs.ThumbSticks.Left.X !=0)
                        fuel -= 0.1f;
                    if (gs.ThumbSticks.Left.Y != 0)
                        fuel -= 0.1f;
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.W) && ks.IsKeyUp(Keys.S) && fuel > 0)
                {
                    movingUp = true;
                    if (velocity.Y > -maxspeed / 2)
                        velocity.Y -= 0.1f * maxspeed;
                    fuel -= 0.1f;
                }
                if (ks.IsKeyUp(Keys.W) && ks.IsKeyDown(Keys.S) && fuel > 0)
                {
                    movingUp = true;
                    if (velocity.Y < maxspeed / 2)
                        velocity.Y += 0.1f * maxspeed;
                    fuel -= 0.1f;
                }
                if (ks.IsKeyUp(Keys.A) && ks.IsKeyDown(Keys.D) && fuel > 0)
                {
                    movingrignt = true;
                    if (velocity.X < maxspeed)
                        velocity.X += 0.02f * maxspeed;
                    fuel -= 0.1f;
                }
                if (ks.IsKeyUp(Keys.D) && ks.IsKeyDown(Keys.A) && fuel > 0)
                {
                    movingrignt = true;
                    if (velocity.X > -maxspeed)
                        velocity.X -= 0.02f * maxspeed;
                    fuel -= 0.1f;
                }
            }
            if (velocity != new Vector2(0, 0))
                if (temp == fuel)
                {

                    velocity.X -= 0.05f * Math.Sign(velocity.X);
                    velocity.Y -= 0.04f * Math.Sign(velocity.Y);
                    if (velocity.Length() < 0.1f)
                        velocity = new Vector2(0, 0);

                }
                else
                {
                    if (!movingrignt)
                    {
                        velocity.X -= 0.05f * Math.Sign(velocity.X);
                        if (Math.Abs(velocity.X) < 0.1f)
                            velocity.X = 0;

                    }
                    if (!movingUp)
                    {
                        velocity.Y -= 0.04f * Math.Sign(velocity.Y);
                        if (Math.Abs(velocity.Y) < 0.1f)
                            velocity.Y = 0;

                    }
                }
            
            position += velocity;
            movingrignt = false;
            movingUp = false;

            Rectangle subBox = new Rectangle((int)position.X, (int)position.Y, 115, 60);
            Rectangle subBox2 = new Rectangle((int)position.X, (int)position.Y, 115, 60);
            Rectangle subBox3 = new Rectangle((int)position.X, (int)position.Y, 115, 60);


            collision = Intersection(subBox, subBox2);

            if (collision.Width > 0 && collision.Height > 0)
            {
                Rectangle r1 = Normalize(subBox, collision);
                Rectangle r2 = Normalize(subBox2, collision);
                Rectangle r3 = Normalize(subBox3, collision);
                bHit = TestCollision(Texture,Texture, r1, r2);
            }
            else
                bHit = false;
        }

        
       public  void Draw()
        {
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(Texture,position,Color.White);

            if (collision.Width > 0 && collision.Width > 0)
            {
                Game1.spriteBatch.Draw(red, collision, new Rectangle(0, 0, collision.Width, collision.Height), Color.White);
            }
            //Har vi en kollision?
            if (bHit)
            {
                Game1.spriteBatch.Draw(hit, new Rectangle(150, 10, 150, 50), Color.White);
            }
            Game1.spriteBatch.End();
        }

        public static Rectangle Intersection(Rectangle r1, Rectangle r2)
        {
            int x1 = Math.Max(r1.Left, r2.Left);
            int y1 = Math.Max(r1.Top, r2.Top);
            int x2 = Math.Min(r1.Right, r2.Right);
            int y2 = Math.Min(r1.Bottom, r2.Bottom);

            if ((x2 >= x1) && (y2 >= y1))
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return Rectangle.Empty;
        }

        public static Rectangle Normalize(Rectangle reference, Rectangle overlap)
        {
            //Räkna ut en rektangel som kan användas relativt till referensrektangeln
            return new Rectangle(
              overlap.X - reference.X,
              overlap.Y - reference.Y,
              overlap.Width,
              overlap.Height);
        }

        public static bool TestCollision(Texture2D t1, Texture2D t2, Rectangle r1, Rectangle r2)
        {
            //Beräkna hur många pixlar som finns i området som ska undersökas
            int pixelCount = r1.Width * r1.Height;
            uint[] texture1Pixels = new uint[pixelCount];
            uint[] texture2Pixels = new uint[pixelCount];

            //Kopiera ut pixlarna från båda områdena
            //t1.GetData(0, r1, texture1Pixels, 0, pixelCount);
            //t2.GetData(0, r2, texture2Pixels, 0, pixelCount);


            //Jämför om vi har några pixlar som överlappar varandra i områdena
            for (int i = 0; i < pixelCount; ++i)
            {
                if (((texture1Pixels[i] & 0xff000000) > 0) && ((texture2Pixels[i] & 0xff000000) > 0) && ((texture2Pixels[i] & 0xff000000) > 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
