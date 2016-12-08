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
        Texture2D HUD;
        Texture2D FuelTank;
        Texture2D RedHealthBar;
        Texture2D GreenHealthBar;



        public void LoadContent(Game1 game)
        {
            HUD = game.Content.Load<Texture2D>("HUD watergame");
            FuelTank = game.Content.Load<Texture2D>("FuelTank100%");
            RedHealthBar = game.Content.Load<Texture2D>("Big red healthbar");
            GreenHealthBar = game.Content.Load<Texture2D>("Big green healthbar");
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(HUD);
            spritebatch.Draw(FuelTank);
        }
    }
}
