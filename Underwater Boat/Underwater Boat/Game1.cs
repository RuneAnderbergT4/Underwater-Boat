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
    enum SubType
    {
        Heavy,
        Highdmg,
        Light
    }
    public class Game1 : Game
    {
        public enum GameState
        {
            Start,
            Playing,
        public static SpriteBatch spriteBatch;
        public static Random r = new Random();
        Sub sub;
            Pause,
            GameOver
        }
        public static GameState GS;
        public static GraphicsDeviceManager Graphics;
        MenuComponent mc;
        MouseState ms;
        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
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
             sub = new Sub(new Team("Team"),SubType.Heavy,false);
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
                Graphics.PreferredBackBufferWidth = 1920;
                Graphics.PreferredBackBufferHeight = 1080;
                Graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1024 * 700")
            {
                Graphics.PreferredBackBufferWidth = 1024;
                Graphics.PreferredBackBufferHeight = 700;
                Graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1366 * 768")
            {
                Graphics.PreferredBackBufferWidth = 1366;
                Graphics.PreferredBackBufferHeight = 768;
                Graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1440 * 900")
            {
                Graphics.PreferredBackBufferWidth = 1440;
                Graphics.PreferredBackBufferHeight = 900;
                Graphics.ApplyChanges();
            }
            if (Settings.Default.Grafik == "1600 * 900")
            {
                Graphics.PreferredBackBufferWidth = 1600;
                Graphics.PreferredBackBufferHeight = 900;
                Graphics.ApplyChanges();
            }
        }
        public void LoadMap(MenuComponent.SelMap selectedMap)
        {
            switch (selectedMap)
            {
                
            }
        }
        public void FullScreen()
        {
            if (Settings.Default.IsFullScreen)
            {
                Graphics.IsFullScreen = true;
                Graphics.ApplyChanges();
                Settings.Default.Grafik = "1920 * 1080";
                Settings.Default.Save();
                Grafitti();
            }
            else if (Settings.Default.IsFullScreen == false)
            {
                Graphics.IsFullScreen = false;
                Graphics.ApplyChanges();
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
            switch (GS)
            sub.Draw();
            {
                case GameState.Start:
                    mc.Draw(gameTime);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
