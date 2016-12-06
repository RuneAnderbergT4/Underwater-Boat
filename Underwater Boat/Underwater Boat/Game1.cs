using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
        public static GameState GS;
        public static GraphicsDeviceManager graphics;

        Sub sub;
        Sub sub2;
        Sub sub3;
        Team t1;
        Team t2;

        MenuComponent mc;
        MouseState ms;

        private Rectangle _cameraRect;
        private Texture2D _level;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Grafitti();
            FullScreen();
            
            _cameraRect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }
        protected override void Initialize()
        {
            mc = new MenuComponent(this);
            Components.Add(mc);
            GS = GameState.Start;
            sub = new Sub(new Team("Team"),SubType.Light,false);
            sub2 = new Sub(new Team("Team"), SubType.Heavy, false);
            sub3 = new Sub(new Team("Team"), SubType.Highdmg, false);
            sub.Initialize();
            sub2.Initialize();
            sub3.Initialize();
            base.Initialize();
        }
        public void Restart()
        {
            
        }
        public void Grafitti()
        {
            switch (Settings.Default.Grafik)
            {
                case "1920 * 1080":
                    graphics.PreferredBackBufferWidth = 1920;
                    graphics.PreferredBackBufferHeight = 1080;
                    graphics.ApplyChanges();
                    break;
                case "1024 * 700":
                    graphics.PreferredBackBufferWidth = 1024;
                    graphics.PreferredBackBufferHeight = 700;
                    graphics.ApplyChanges();
                    break;
                case "1366 * 768":
                    graphics.PreferredBackBufferWidth = 1366;
                    graphics.PreferredBackBufferHeight = 768;
                    graphics.ApplyChanges();
                    break;
                case "1440 * 900":
                    graphics.PreferredBackBufferWidth = 1440;
                    graphics.PreferredBackBufferHeight = 900;
                    graphics.ApplyChanges();
                    break;
                case "1600 * 900":
                    graphics.PreferredBackBufferWidth = 1600;
                    graphics.PreferredBackBufferHeight = 900;
                    graphics.ApplyChanges();
                    break;
                default:
                    graphics.PreferredBackBufferWidth = 1600;
                    graphics.PreferredBackBufferHeight = 900;
                    graphics.ApplyChanges();
                    break;
            }
        }

        public void LoadMap()
        {
            
        }

        public void FullScreen()
        {
            if (Settings.Default.IsFullScreen)
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
                Settings.Default.Grafik = "1920 * 1080";
                Settings.Default.Save();
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
            sub2.LoadContent(this);
            sub3.LoadContent(this);
            _level = new Texture2D(GraphicsDevice, 1, 1);
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
            sub2.Update(ks, gs);
            sub3.Update(ks, gs);

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && _cameraRect.Top > 0)
            {
                _cameraRect.Y -= 20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && _cameraRect.Bottom < _level.Height)
            {
                _cameraRect.Y += 20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _cameraRect.Left > 0)
            {
                _cameraRect.X -= 20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && _cameraRect.Right < _level.Width)
            {
                _cameraRect.X += 20;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            switch (GS)
            {
                case GameState.Start:
                    mc.Draw(gameTime);
                    break;
                case GameState.Playing:
                    sub.Draw();
                    sub2.Draw();
                    sub3.Draw();
                    spriteBatch.Begin();
                    spriteBatch.Draw(_level, Vector2.Zero, _cameraRect, Color.White);
                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
