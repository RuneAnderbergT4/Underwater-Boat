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
        public static SpriteBatch SpriteBatch;
        public static GameState GameState;
        public static GraphicsDeviceManager Graphics;
        public static Turnbase TB;
        public static LevelManager LevelManager;

        private GamePadComponent gamePadComponent;
        private UI uI;
        private MenuComponent menuComponent;
        private KeyboardState keyboardState;
        private GamePadState gamePadState;
        private Camera _camera;
        private Matrix _spriteScale;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Grafitti();
            FullScreen();
            LevelManager = new LevelManager(4096, 2048, new ServiceBus());
            _camera = new Camera();
        }

        protected override void Initialize()
        {
            menuComponent = new MenuComponent(this);
            Components.Add(menuComponent);
            Components.Add(new KeyboardComponent(this));
            Components.Add(new GamePadComponent(this));
            GameState = GameState.Start;
            TB = new Turnbase("t1","t2");
            uI = new UI();

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
                    Graphics.PreferredBackBufferWidth = 1920;
                    Graphics.PreferredBackBufferHeight = 1080;
                    Graphics.ApplyChanges();
                    break;
                case "1600 * 900":
                    Graphics.PreferredBackBufferWidth = 1600;
                    Graphics.PreferredBackBufferHeight = 900;
                    Graphics.ApplyChanges();
                    break;
                case "1366 * 768":
                    Graphics.PreferredBackBufferWidth = 1366;
                    Graphics.PreferredBackBufferHeight = 768;
                    Graphics.ApplyChanges();
                    break;
                case "1280 * 720":
                    Graphics.PreferredBackBufferWidth = 1280;
                    Graphics.PreferredBackBufferHeight = 720;
                    Graphics.ApplyChanges();
                    break;
                default:
                    Graphics.PreferredBackBufferWidth = 1600;
                    Graphics.PreferredBackBufferHeight = 900;
                    Graphics.ApplyChanges();
                    break;
            }

            if (Graphics.GraphicsDevice != null)
            {
                // Default resolution is 1920x1080; scale sprites up or down based on current viewport
                var scale = Graphics.PreferredBackBufferWidth / 1920.0;

                // Create the scale transform for Draw. 
                // Do not scale the sprite depth (Z=1).
                _spriteScale = Matrix.CreateScale((float) scale, (float) scale, 1);
            }
        }

        public static LevelManager LoadMap()
        {
            LevelManager.StartGenerateLevel(Graphics.GraphicsDevice);
            return LevelManager;
        }

        public void FullScreen()
        {
            if (Settings.Default.IsFullScreen)
            {
                Graphics.IsFullScreen = true;
                Graphics.ApplyChanges();
            }
            else if (Settings.Default.IsFullScreen == false)
            {
                Graphics.IsFullScreen = false;
                Graphics.ApplyChanges();
            }
        }

        protected override void LoadContent()
        {
            Submarine.LoadContent(this);
            Ship.LoadContent(this);
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TB.AddSub(SubType.Aqua, false, "t1");
            uI.LoadContent(this);
            Projectiles.LoadContent(this);

            // Default resolution is 1920x1080; scale sprites up or down based on current viewport
            var scale = Graphics.PreferredBackBufferWidth / 1920.0;

            // Create the scale transform for Draw. 
            // Do not scale the sprite depth (Z=1).
            _spriteScale = Matrix.CreateScale((float) scale, (float) scale, 1);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState prevks = keyboardState;
            GamePadState prevgs = gamePadState;
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            switch (GameState)
            {
                case GameState.Playing:
                    TB.Update();
                    Projectiles.Update();
                    uI.Update(gameTime);
                    if (keyboardState.IsKeyDown(Keys.Escape) && prevks.IsKeyUp(Keys.Escape) || gamePadState.IsButtonDown(Buttons.Start) && prevgs.IsButtonUp(Buttons.Start))
                    {
                        GameState = GameState.Pause;
                        MenuComponent.gs = MenuComponent.MenyState.MainMenu;
                    }
                    _camera.UpdateCamera();
                    break;
                case GameState.Pause:
                    if (keyboardState.IsKeyDown(Keys.Escape) && prevks.IsKeyUp(Keys.Escape) ||
                        gamePadState.IsButtonDown(Buttons.Start) && prevgs.IsButtonUp(Buttons.Start))
                    {
                        GameState = GameState.Playing;
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
           
            switch (GameState)
            {
                case GameState.Start:
                    menuComponent.Draw(gameTime);
                    break;
                case GameState.Playing:
                    // Draw game content
                    SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.get_transformation(Graphics.GraphicsDevice));
                    SpriteBatch.Draw(LevelManager.Level, Vector2.Zero, Color.White);
                    TB.Draw();
                    Projectiles.Draw();
                    SpriteBatch.End();

                    // Draw hud content
                    SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _spriteScale);
                    uI.Draw();
                    SpriteBatch.End();
                    break;
                case GameState.Pause:
                    menuComponent.Draw(gameTime);
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
