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
        public static Sub currentSub;
        public static Turnbase TB;
        public static LevelManager LevelManager;

        private GamePadComponent gc;
        private UI ui;
        private MenuComponent mc;
        private KeyboardState ks;
        private GamePadState gs;
        private Camera _camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Grafitti();
            FullScreen();
            LevelManager = new LevelManager(4096, 2048, new ServiceBus());
            _camera = new Camera();
        }

        protected override void Initialize()
        {
            mc = new MenuComponent(this);
            Components.Add(mc);
            Components.Add(new KeyboardComponent(this));
            Components.Add(new GamePadComponent(this));
            GS = GameState.Start;
            TB = new Turnbase("t1","t2");
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

        public static LevelManager LoadMap()
        {
            LevelManager.StartGenerateLevel(graphics.GraphicsDevice);
            return LevelManager;
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
            Submarine.LoadContent(this);
            Ship.LoadContent(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TB.AddSub(SubType.Aqua, false, "t1");
            ui.LoadContent(this);
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
            KeyboardState prevks = ks;
            GamePadState prevgs = gs;
            gs = GamePad.GetState(0);
            ks = Keyboard.GetState();

            switch (GS)
            {
                case GameState.Playing:
                    TB.Update();
                    Projectiles.Update();
                    ui.Update(gameTime);
                    if (ks.IsKeyDown(Keys.Escape) && prevks.IsKeyUp(Keys.Escape) || gs.IsButtonDown(Buttons.Start) && prevgs.IsButtonUp(Buttons.Start))
                    {
                    GS = GameState.Pause;
                    MenuComponent.gs = MenuComponent.MenyState.MainMenu;
                    }
                    _camera.UpdateCamera();
                    break;
                case GameState.Pause:
                    if (ks.IsKeyDown(Keys.Escape) && prevks.IsKeyUp(Keys.Escape) ||
                        gs.IsButtonDown(Buttons.Start) && prevgs.IsButtonUp(Buttons.Start))
                    {
                        GS = GameState.Playing;
                        MenuComponent.gs = MenuComponent.MenyState.Playing;
                    }
                    break;
                case GameState.Start:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
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
                    // Draw game content
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.get_transformation(graphics.GraphicsDevice));
                    spriteBatch.Draw(LevelManager.Level, Vector2.Zero, Color.White);
                    TB.Draw();
                    Projectiles.Draw();
                    spriteBatch.End();

                    // Draw hud content
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, SpriteScale);
                    ui.Draw();
                    spriteBatch.End();
                    break;
                case GameState.Pause:
                    mc.Draw(gameTime);
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
