using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Underwater_Boat
{
    public class Game1 : Game
    {
        public enum GameState
        {
            Start,
            Playing,
            Pause,
            GameOver
        }
        public static GameState GS;
        public static GraphicsDeviceManager Graphics;
        MenuComponent mc;
        MouseState ms;
        SpriteBatch spriteBatch;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.IsFullScreen = false;

        }
        protected override void Initialize()
        {
            mc = new MenuComponent(this);
            Components.Add(mc);
            GS = GameState.Start;
            base.Initialize();
        }
        public void Restart()
        {
            
        }
        public void Grafitti()
        {
            if (MenuComponent.GR == MenuComponent.Graphics.set1)
            {
                Graphics.PreferredBackBufferWidth = 1920;
                Graphics.PreferredBackBufferHeight = 1080;
                Graphics.ApplyChanges();
            }
            if (MenuComponent.GR == MenuComponent.Graphics.set2)
            {
                Graphics.PreferredBackBufferWidth = 1024;
                Graphics.PreferredBackBufferHeight = 700;
                Graphics.ApplyChanges();
            }
            if (MenuComponent.GR == MenuComponent.Graphics.set3)
            {
                Graphics.PreferredBackBufferWidth = 1366;
                Graphics.PreferredBackBufferHeight = 768;
                Graphics.ApplyChanges();
            }
            if (MenuComponent.GR == MenuComponent.Graphics.set4)
            {
                Graphics.PreferredBackBufferWidth = 1440;
                Graphics.PreferredBackBufferHeight = 900;
                Graphics.ApplyChanges();
            }
            if (MenuComponent.GR == MenuComponent.Graphics.set5)
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
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);


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
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch (GS)
            {
                case GameState.Start:
                    mc.Draw(gameTime);
                    IsMouseVisible = true;
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
