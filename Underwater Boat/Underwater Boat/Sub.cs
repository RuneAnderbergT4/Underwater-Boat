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
    public class Sub : PlayerStatBase
    {
        public PlayerStat ps;
        Team Team;
        bool isBot;
        float HP;
        Vector2 velocity = new Vector2(0,0);
        bool movingrignt;
        public bool gamepad; // = true;
        private bool movingUp;
        int currentweapon;
        public List<Weapon> Weapons { get; private set; }

        public Sub(Team Team,PlayerStat ps,bool isBot)
        {
            this.ps = ps;
            this.Team = Team;
            this.isBot = isBot;
           // PlayerId = ps.PlayerId;
            Name = ps.Name;
            Resistance = ps.Resistance;
            ExplosionResistence = ps.ExplosionResistence;
            Damage = ps.Damage;
            //Speed = 1;
            //Size = 1;
            MaxHealth = ps.MaxHealth;
            MaxFuel = ps.MaxFuel;
            Maxspeed = 5;
            Texturestring = ps.Texturestring;
            Fuel = MaxFuel;
            Health = MaxHealth;
            color = Color.White;
            Weapons = new List<Weapon>();
            Weapons.Add(new Weapon(Weap.Nuke));
            currentweapon = 0;
            Initialize();
            
        }
        public  void Initialize()
        {
            

           // Fuel = 300888;
            Position = new Vector2(500,500);
            velocity = new Vector2(0,0);
            
        }

        
        public  void LoadContent(Game Game1)
        {
            
                    Texture = Game1.Content.Load<Texture2D>(Texturestring);
              
        }
        
        public  void Update()
        {
            KeyboardState ks = Keyboard.GetState();
            GamePadState gs = GamePad.GetState(0);
            float temp = Fuel;

            if (gamepad)
            {
                if (gs.ThumbSticks.Left.Length() != 0)
                {
                    if (-gs.ThumbSticks.Left.Y < 0)
                    {
                        movingUp = true;
                        if (velocity.Y > -Maxspeed / 2)
                            velocity.Y += 0.1f * Maxspeed * -gs.ThumbSticks.Left.Y;
                    }
                    if (-gs.ThumbSticks.Left.Y > 0)

                    {
                        movingUp = true;
                        if (velocity.Y < Maxspeed / 2)
                            velocity.Y += 0.1f * Maxspeed * -gs.ThumbSticks.Left.Y;
                    }
                    if (gs.ThumbSticks.Left.X < 0)
                    {
                        movingrignt = true;
                        if (velocity.X > -Maxspeed)
                            velocity.X += 0.02f * Maxspeed * gs.ThumbSticks.Left.X;
                    }
                    if (gs.ThumbSticks.Left.X > 0)
                    {
                        movingrignt = true;
                        if (velocity.X < Maxspeed)
                            velocity.X += 0.02f * Maxspeed * gs.ThumbSticks.Left.X;
                    }
                    if (gs.ThumbSticks.Left.X !=0)
                        Fuel -= 0.1f;
                    if (gs.ThumbSticks.Left.Y != 0)
                        Fuel -= 0.1f;
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.W) && ks.IsKeyUp(Keys.S) && Fuel > 0)
                {
                    movingUp = true;
                    if (velocity.Y > -Maxspeed / 2)
                        velocity.Y -= 0.1f * Maxspeed;
                    Fuel -= 0.1f;
                }
                if (ks.IsKeyUp(Keys.W) && ks.IsKeyDown(Keys.S) && Fuel > 0)
                {
                    movingUp = true;
                    if (velocity.Y < Maxspeed / 2)
                        velocity.Y += 0.1f * Maxspeed;
                   Fuel -= 0.1f;
                }
                if (ks.IsKeyUp(Keys.A) && ks.IsKeyDown(Keys.D) && Fuel > 0)
                {
                    movingrignt = true;
                    if (velocity.X < Maxspeed)
                        velocity.X += 0.02f * Maxspeed;
                    Fuel -= 0.1f;
                }
                if (ks.IsKeyUp(Keys.D) && ks.IsKeyDown(Keys.A) && Fuel > 0)
                {
                    movingrignt = true;
                    if (velocity.X > -Maxspeed)
                        velocity.X -= 0.02f * Maxspeed;
                    Fuel -= 0.1f;
                }
            }
            if (velocity != new Vector2(0, 0))
                if (temp == Fuel)
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

            Position += velocity;
            movingrignt = false;
            movingUp = false;
        }

        public Weapon CurrentWeapon()
        {
            return Weapons[currentweapon];
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(Texture, Position, color);
        }
        public void ResetVel()
        {
            color = Color.White;
            velocity = Vector2.Zero;
            Fuel = MaxFuel;
        }
    }
}
