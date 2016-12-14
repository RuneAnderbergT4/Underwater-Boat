using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public class UI
    {
        PlayerStat stat;
        Sub sub;
        int timer = 30;
        const float TIMER = 30;

        Vector2 fuelpos;
        Vector2 healthpos;
        Vector2 timerpos;
        Vector2 pointpos;
        Vector2 profilepos;

        Texture2D HUDBlue;
        Texture2D HUDRed;
        Texture2D FuelTank100;
        Texture2D FuelTank90;
        Texture2D FuelTank80;
        Texture2D FuelTank70;
        Texture2D FuelTank60;
        Texture2D FuelTank50;
        Texture2D FuelTank40;
        Texture2D FuelTank30;
        Texture2D FuelTank20;
        Texture2D FuelTank10;
        Texture2D FuelTank0;
        Texture2D RedHealthBar;
        Texture2D GreenHealthBar;
        Texture2D SubmarineProfile1;

        SpriteFont Timer;
        SpriteFont Points;

        Rectangle RedHealthRectangle;
        Rectangle GreenHealthRectangle;

        public UI ()
        {
            fuelpos = new Vector2(22, 857);
            healthpos = new Vector2(1074, 33);
            timerpos = new Vector2(760, 35);
            pointpos = new Vector2(47, 40);
            profilepos = new Vector2(773, 858);
        }

        public void LoadContent(Game1 game)
        {
            HUDBlue = game.Content.Load<Texture2D>("HUD watergame");
            HUDRed = game.Content.Load<Texture2D>("HUD watergame red");
            FuelTank100 = game.Content.Load<Texture2D>("FuelTank100%");
            FuelTank90 = game.Content.Load<Texture2D>("FuelTank90%");
            FuelTank80 = game.Content.Load<Texture2D>("FuelTank80%");
            FuelTank70 = game.Content.Load<Texture2D>("FuelTank70%");
            FuelTank60 = game.Content.Load<Texture2D>("FuelTAnk60%");
            FuelTank50 = game.Content.Load<Texture2D>("FuelTank50%");
            FuelTank40 = game.Content.Load<Texture2D>("FuelTank40%");
            FuelTank30 = game.Content.Load<Texture2D>("FuelTank30%");
            FuelTank20 = game.Content.Load<Texture2D>("FuelTank20%");
            FuelTank10 = game.Content.Load<Texture2D>("FuelTank10%");
            FuelTank0 = game.Content.Load<Texture2D>("FuelTank0%");
            RedHealthBar = game.Content.Load<Texture2D>("Big red healthbar");
            GreenHealthBar = game.Content.Load<Texture2D>("Big green healthbar");
            SubmarineProfile1 = game.Content.Load<Texture2D>("Submarine 1 profile");
            Timer = game.Content.Load<SpriteFont>("HUDTimer");
            Points = game.Content.Load<SpriteFont>("HUDPoints");
        }
        public void Update( GameTime gameTime)
        {
            sub = Game1.TB.currentSub;
            this.stat = sub.ps;
            timer = (Game1.TB.Timer-1)/60 +1;
        }
        public void Draw()
        {
            sub = Game1.TB.currentSub;
            stat = sub.ps;
            SpriteBatch spritebatch = Game1.spriteBatch;
            spritebatch.Draw(HUDBlue, Vector2.Zero, Color.White);
            //spritebatch.Draw(HUDBottom, new Vector2(0, 1080 - HUDBottom.Height), Color.White);
            spritebatch.DrawString(Timer, ((int)timer).ToString(), timerpos, Color.Black);
            spritebatch.DrawString(Points, "Points: ", pointpos, Color.Black);
            spritebatch.Draw(SubmarineProfile1, profilepos, Color.White);
            if(sub.Fuel <= sub.MaxFuel* 1 && sub.Fuel >= sub.MaxFuel * 0.9)
            {
                spritebatch.Draw(FuelTank100, fuelpos, Color.White);
            }
            else if(sub.Fuel <= sub.MaxFuel * 0.9 && sub.Fuel >= sub.MaxFuel * 0.8)
            {
                spritebatch.Draw(FuelTank90, fuelpos, Color.White);
            }
            else if(sub.Fuel <= sub.MaxFuel* 0.8 && sub.Fuel >= sub.MaxFuel * 0.7)
            {
                spritebatch.Draw(FuelTank80, fuelpos, Color.White);
            }
            else if(sub.Fuel <= sub.MaxFuel * 0.7 && sub.Fuel >= sub.MaxFuel * 0.6)
            {
                spritebatch.Draw(FuelTank70, fuelpos, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.6 && sub.Fuel >= sub.MaxFuel * 0.5)
            {
                spritebatch.Draw(FuelTank60, fuelpos, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.5 && sub.Fuel >= sub.MaxFuel * 0.4)
            {
                spritebatch.Draw(FuelTank50, fuelpos, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.4 && sub.Fuel >= sub.MaxFuel * 0.3)
            {
                spritebatch.Draw(FuelTank40, fuelpos, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.3 && sub.Fuel >= sub.MaxFuel * 0.2)
            {
                spritebatch.Draw(FuelTank30, fuelpos, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.2 && sub.Fuel >= sub.MaxFuel * 0.1)
            {
                spritebatch.Draw(FuelTank20, fuelpos, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.1 && sub.Fuel >= 0)
            {
                spritebatch.Draw(FuelTank10, fuelpos, Color.White);
            }
            else if (sub.Fuel <= 1)
            {
                spritebatch.Draw(FuelTank0, fuelpos, Color.White);
            }
            RedHealthRectangle = new Rectangle((int)healthpos.X,(int)healthpos.Y, 806, 46);
            GreenHealthRectangle = new Rectangle((int)healthpos.X,(int)healthpos.Y, 806, 46);
            spritebatch.Draw(RedHealthBar, RedHealthRectangle, Color.White);
            spritebatch.Draw(GreenHealthBar, GreenHealthRectangle, Color.White);
        }
    }
}
