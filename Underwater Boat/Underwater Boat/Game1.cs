using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnderWater_Boat;

namespace Underwater_Boat
{
    public enum GameState
    {
        Start,
        Playing,
        Pause,
        GameOver
    }

    public class Game1 : Game
    {
        public static SpriteBatch spriteBatch;
        public static GameState GS;
        public static GraphicsDeviceManager graphics;
        public static Matrix SpriteScale;
        public static double HeightScale;
        public static double WidthScale;

        Turnbase tb;
        UI ui;
        MenuComponent mc;
        
        private Rectangle _cameraRect;
        private Texture2D _level;
        public static Sub currentSub;

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
            Components.Add(new KeyboardComponent(this));
            Components.Add(new GamePadComponent(this));
            GS = GameState.Start;
            tb = new Turnbase("t1","t2");
            tb.AddSub(SubType.Aqua,false,"t1");
            tb.AddSub(SubType.Megalodon, false, "t1");
            tb.AddSub(SubType.Standard, false, "t1");
            tb.AddSub(SubType.X_1, false, "t2");
            tb.AddSub(SubType.YellowSubmarine, false, "t2");
            tb.AddSub(SubType.Standard, false, "t2");
            tb.AddSub(SubType.shipCamoflage, false, "t1");
            tb.AddSub(SubType.shipCarrier, false, "t1");
            tb.AddSub(SubType.shipPansar, false, "t1");
            tb.AddSub(SubType.shipTradional, false, "t2");
            tb.AddSub(SubType.shipVintage, false, "t2");
            tb.AddSub(SubType.shipPansar, false, "t2");
            ui = new UI();

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
                case "1600 * 900":
                    graphics.PreferredBackBufferWidth = 1600;
                    graphics.PreferredBackBufferHeight = 900;
                    graphics.ApplyChanges();
                    break;
                case "1366 * 768":
                    graphics.PreferredBackBufferWidth = 1366;
                    graphics.PreferredBackBufferHeight = 768;
                    graphics.ApplyChanges();
                    break;
                case "1280 * 720":
                    graphics.PreferredBackBufferWidth = 1280;
                    graphics.PreferredBackBufferHeight = 720;
                    graphics.ApplyChanges();
                    break;
                default:
                    graphics.PreferredBackBufferWidth = 1600;
                    graphics.PreferredBackBufferHeight = 900;
                    graphics.ApplyChanges();
                    break;
            }

            if (graphics.GraphicsDevice != null)
            {
                // Default resolution is 1920x1080; scale sprites up or down based on current viewport
                WidthScale = graphics.PreferredBackBufferWidth / 1920.0;
                HeightScale = graphics.PreferredBackBufferHeight / 1080.0;

                // Create the scale transform for Draw. 
                // Do not scale the sprite depth (Z=1).
                SpriteScale = Matrix.CreateScale((float) WidthScale, (float)WidthScale, 1);
            }
        }

        public LevelGenerator LoadMap()
        {
            var lvlgen = new LevelGenerator();
            lvlgen.StartGenerateLevel(GraphicsDevice, 4096, 2048, new ServiceBus());
            return lvlgen;
        }

        public void UpdateLevel()
        {
            _level = LevelGenerator.Result;
        }

        public void FullScreen()
        {
            if (Settings.Default.IsFullScreen)
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
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
            tb.LoadContent(this);
            ui.LoadContent(this);
            _level = new Texture2D(GraphicsDevice, 1, 1);
            Projectiles.LoadContent(this);
            // Default resolution is 1920x1080; scale sprites up or down based on current viewport
            WidthScale = graphics.PreferredBackBufferWidth / 1920.0;
            HeightScale = graphics.PreferredBackBufferHeight / 1080.0;

            // Create the scale transform for Draw. 
            // Do not scale the sprite depth (Z=1).
            SpriteScale = Matrix.CreateScale((float)WidthScale, (float)WidthScale, 1);
        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            currentSub = tb.currentSub;
            ui.Update(currentSub, gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            
            KeyboardState ks = Keyboard.GetState();
            GamePadState gs = GamePad.GetState(0);
            tb.Update();

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
                    // Draw non scaled content
                    spriteBatch.Begin();
                    Projectiles.Draw();
                    tb.Draw();
                    spriteBatch.Draw(_level, Vector2.Zero, _cameraRect, Color.White);
                    Projectiles.Draw();
                    spriteBatch.End();

                    // Draw scaled content
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, SpriteScale);
                    ui.Draw(spriteBatch, graphics);
                    spriteBatch.End();
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
