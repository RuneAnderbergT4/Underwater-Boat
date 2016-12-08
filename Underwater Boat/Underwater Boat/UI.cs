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

        Texture2D HUD;
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

        Rectangle RedHealthRectangle;
        Rectangle GreenHealthRectangle;

        int MaxHealth;
        public UI ()
        {
           // MaxHealth = stat.Health;
        }

        public void LoadContent(Game1 game)
        {
            HUD = game.Content.Load<Texture2D>("HUD watergame");
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
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(HUD, sub.position, Color.White);
            if(stat.Fuel <= stat.Fuel* 1)
            {
                spritebatch.Draw(FuelTank100/*, (int)sub.position.X + 75, (int)sub.position.Y + 75, Color.White*/);
            }
            else if(stat.Fuel <= stat.Fuel * 0.9)
            {
                spritebatch.Draw(FuelTank90);
            }
            else if(stat.Fuel <= stat.Fuel* 0.8)
            {
                spritebatch.Draw(FuelTank80);
            }
            else if(stat.Fuel <= stat.Fuel * 0.7)
            {
                spritebatch.Draw(FuelTank70);
            }
            else if (stat.Fuel <= stat.Fuel * 0.6)
            {
                spritebatch.Draw(FuelTank60);
            }
            else if (stat.Fuel <= stat.Fuel * 0.5)
            {
                spritebatch.Draw(FuelTank50);
            }
            else if (stat.Fuel <= stat.Fuel * 0.4)
            {
                spritebatch.Draw(FuelTank40);
            }
            else if (stat.Fuel <= stat.Fuel * 0.3)
            {
                spritebatch.Draw(FuelTank30);
            }
            else if (stat.Fuel <= stat.Fuel * 0.2)
            {
                spritebatch.Draw(FuelTank20);
            }
            else if (stat.Fuel <= stat.Fuel * 0.1)
            {
                spritebatch.Draw(FuelTank10);
            }
            else if (stat.Fuel == 0)
            {
                spritebatch.Draw(FuelTank0);
            }
            RedHealthRectangle = new Rectangle(((int)sub.position.X - 50), ((int)sub.position.Y + 35), 10, 1);
            GreenHealthRectangle = new Rectangle(((int)sub.position.X - 50), ((int)sub.position.Y + 35), stat.Health / MaxHealth * 10, 1);
            spritebatch.Draw(RedHealthBar, RedHealthRectangle, Color.White);
            spritebatch.Draw(GreenHealthBar, GreenHealthRectangle, Color.White);
        }
    }
}
