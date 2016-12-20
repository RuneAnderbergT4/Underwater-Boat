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

        Vector2 fuelPosition;
        Vector2 healthPosition;
        Vector2 timerPosition;
        Vector2 pointPosition;
        Vector2 profilePosition;

        Texture2D HUDBlue;
        Texture2D HUDRed;
        Texture2D fuelTank100;
        Texture2D fuelTank90;
        Texture2D fuelTank80;
        Texture2D fuelTank70;
        Texture2D fuelTank60;
        Texture2D fuelTank50;
        Texture2D fuelTank40;
        Texture2D fuelTank30;
        Texture2D fuelTank20;
        Texture2D fuelTank10;
        Texture2D fuelTank0;
        Texture2D redHealthBar;
        Texture2D greenHealthBar;
        Texture2D submarineProfile1;

        SpriteFont timerFont;
        SpriteFont pointsFont;

        Rectangle redHealthRectangle;
        Rectangle greenHealthRectangle;

        public UI ()
        {
            fuelPosition = new Vector2(22, 857);
            healthPosition = new Vector2(1074, 33);
            timerPosition = new Vector2(760, 35);
            pointPosition = new Vector2(47, 40);
            profilePosition = new Vector2(773, 858);
        }

        public void LoadContent(Game1 game)
        {
            HUDBlue = game.Content.Load<Texture2D>("HUD watergame");
            HUDRed = game.Content.Load<Texture2D>("HUD watergame red");
            fuelTank100 = game.Content.Load<Texture2D>("FuelTank100%");
            fuelTank90 = game.Content.Load<Texture2D>("FuelTank90%");
            fuelTank80 = game.Content.Load<Texture2D>("FuelTank80%");
            fuelTank70 = game.Content.Load<Texture2D>("FuelTank70%");
            fuelTank60 = game.Content.Load<Texture2D>("FuelTAnk60%");
            fuelTank50 = game.Content.Load<Texture2D>("FuelTank50%");
            fuelTank40 = game.Content.Load<Texture2D>("FuelTank40%");
            fuelTank30 = game.Content.Load<Texture2D>("FuelTank30%");
            fuelTank20 = game.Content.Load<Texture2D>("FuelTank20%");
            fuelTank10 = game.Content.Load<Texture2D>("FuelTank10%");
            fuelTank0 = game.Content.Load<Texture2D>("FuelTank0%");
            redHealthBar = game.Content.Load<Texture2D>("Big red healthbar");
            greenHealthBar = game.Content.Load<Texture2D>("Big green healthbar");
            submarineProfile1 = game.Content.Load<Texture2D>("Submarine 1 profile");
            timerFont = game.Content.Load<SpriteFont>("HUDTimer");
            pointsFont = game.Content.Load<SpriteFont>("HUDPoints");
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
            SpriteBatch spritebatch = Game1.SpriteBatch;
            spritebatch.Draw(HUDBlue, Vector2.Zero, Color.White);
            //spritebatch.Draw(HUDBottom, new Vector2(0, 1080 - HUDBottom.Height), Color.White);
            spritebatch.DrawString(timerFont, ((int)timer).ToString(), timerPosition, Color.Black);
            spritebatch.DrawString(pointsFont, "Points: ", pointPosition, Color.Black);
            spritebatch.Draw(submarineProfile1, profilePosition, Color.White);
            
            if(sub.Fuel <= sub.MaxFuel* 1 && sub.Fuel >= sub.MaxFuel * 0.9)
            {
                spritebatch.Draw(fuelTank100, fuelPosition, Color.White);
            }
            else if(sub.Fuel <= sub.MaxFuel * 0.9 && sub.Fuel >= sub.MaxFuel * 0.8)
            {
                spritebatch.Draw(fuelTank90, fuelPosition, Color.White);
            }
            else if(sub.Fuel <= sub.MaxFuel* 0.8 && sub.Fuel >= sub.MaxFuel * 0.7)
            {
                spritebatch.Draw(fuelTank80, fuelPosition, Color.White);
            }
            else if(sub.Fuel <= sub.MaxFuel * 0.7 && sub.Fuel >= sub.MaxFuel * 0.6)
            {
                spritebatch.Draw(fuelTank70, fuelPosition, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.6 && sub.Fuel >= sub.MaxFuel * 0.5)
            {
                spritebatch.Draw(fuelTank60, fuelPosition, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.5 && sub.Fuel >= sub.MaxFuel * 0.4)
            {
                spritebatch.Draw(fuelTank50, fuelPosition, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.4 && sub.Fuel >= sub.MaxFuel * 0.3)
            {
                spritebatch.Draw(fuelTank40, fuelPosition, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.3 && sub.Fuel >= sub.MaxFuel * 0.2)
            {
                spritebatch.Draw(fuelTank30, fuelPosition, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.2 && sub.Fuel >= sub.MaxFuel * 0.1)
            {
                spritebatch.Draw(fuelTank20, fuelPosition, Color.White);
            }
            else if (sub.Fuel <= stat.MaxFuel * 0.1 && sub.Fuel >= 0)
            {
                spritebatch.Draw(fuelTank10, fuelPosition, Color.White);
            }
            else if (sub.Fuel <= 1)
            {
                spritebatch.Draw(fuelTank0, fuelPosition, Color.White);
            }

            redHealthRectangle = new Rectangle((int)healthPosition.X,(int)healthPosition.Y, 806, 46);
            greenHealthRectangle = new Rectangle((int)healthPosition.X,(int)healthPosition.Y, 806, 46);
            spritebatch.Draw(redHealthBar, redHealthRectangle, Color.White);
            spritebatch.Draw(greenHealthBar, greenHealthRectangle, Color.White);
        }
    }
}
