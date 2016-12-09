using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public enum GameState
    {
        Start,
        Playing,
        Pause,
        GameOver
    }
    enum SubType
    {
        Heavy,
        Highdmg,
        Light
    }
    public class Game1 : Game
    {
         public static SpriteBatch spriteBatch;
        public static Random r = new Random();
        Sub sub;
        public static GameState GS;
        public static GraphicsDeviceManager graphics;
        MenuComponent mc;
        MouseState ms;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Grafitti();
            FullScreen();
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
        }
        protected override void Initialize()
        {
            mc = new MenuComponent(this);
            Components.Add(mc);
            GS = GameState.Start;
            sub = new Sub(new Team("Team"),SubType.Light,false);
            sub.Initialize();
            base.Initialize();
        }
        public void Restart()
        {
            
        }
        public void Grafitti()
        {
            if (Settings.Default.Grafik == "1920 * 1080")
            {
                graphics.PreferredBackBufferWidth = 1920;
                graphics.PreferredBackBufferHeight = 1080;
                graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1024 * 700")
            {
                graphics.PreferredBackBufferWidth = 1024;
                graphics.PreferredBackBufferHeight = 700;
                graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1366 * 768")
            {
                graphics.PreferredBackBufferWidth = 1366;
                graphics.PreferredBackBufferHeight = 768;
                graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1440 * 900")
            {
                graphics.PreferredBackBufferWidth = 1440;
                graphics.PreferredBackBufferHeight = 900;
                graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1600 * 900")
            {
                graphics.PreferredBackBufferWidth = 1600;
                graphics.PreferredBackBufferHeight = 900;
                graphics.ApplyChanges();
            }
        }
        public void ChooseSprites()
        {
            if (MenuComponent.SP == MenuComponent.SelShip.Ship)
            {

            }
            else
            {
                
            }
        }
        public void FullScreen()
        {
            if (Settings.Default.IsFullScreen)
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
                Grafitti();
            }
            else if (Settings.Default.IsFullScreen == false)
            {
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
            }
        }
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sub.LoadContent(this);

        }
        protected override void UnloadContent()
        {
            
            
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            switch (GS)
            {
                case GameState.Start:
                    break;
            }
            KeyboardState ks = Keyboard.GetState();
            GamePadState gs = GamePad.GetState(0);
            sub.Update(ks,gs);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            sub.Draw();
            switch (GS)
            
            {
                case GameState.Start:
                    mc.Draw(gameTime);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
