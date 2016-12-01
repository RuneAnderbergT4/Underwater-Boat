using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Underwater_Boat
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Water water;
        KeyboardState keyState, lastKeyState;
        MouseState mouseState, lastMouseState;
        SpriteFont font;
        Texture2D particleImage, backgroundImage, rockImage;
        Rock rock;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferMultiSampling = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            particleImage = Content.Load<Texture2D>("metaparticle");
            backgroundImage = Content.Load<Texture2D>("sky");
            rockImage = Content.Load<Texture2D>("rock");
            water = new Water(GraphicsDevice, particleImage);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            lastKeyState = keyState;
            keyState = Keyboard.GetState();
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            water.Update();

            // Allow user to adjust the water's properties:
            // Q and W change the Tension
            // A and S change the Dampering
            // Z and X change the Spred
            const float factor = 63f / 64f;
            if (keyState.IsKeyDown(Keys.Q))
                water.Tension *= factor;
            if (keyState.IsKeyDown(Keys.W))
                water.Tension /= factor;
            if (keyState.IsKeyDown(Keys.A))
                water.Dampening *= factor;
            if (keyState.IsKeyDown(Keys.S))
                water.Dampening /= factor;
            if (keyState.IsKeyDown(Keys.Z))
                water.Spread *= factor;
            if (keyState.IsKeyDown(Keys.X))
                water.Spread /= factor;

            Vector2 mousePos = new Vector2(mouseState.X, mouseState.Y);

            if (lastMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                rock = new Rock
                {
                    Position = mousePos,
                    Velocity = (mousePos - new Vector2(lastMouseState.X, lastMouseState.Y)) / 5f
                };
            }


            if (rock != null)
            {
                if (rock.Position.Y < 240 && rock.Position.Y + rock.Velocity.Y >= 240)
                    water.Splash(rock.Position.X, rock.Velocity.Y * rock.Velocity.Y * 5);

                rock.Update(water);

                if (rock.Position.Y > GraphicsDevice.Viewport.Height + rockImage.Height)
                    rock = null;
            }

            base.Update(gameTime);
        }

        bool WasPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && !lastKeyState.IsKeyDown(key);
        }

        protected override void Draw(GameTime gameTime)
        {

            water.DrawToRenderTargets();

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            if (rock != null)
                rock.Draw(spriteBatch, rockImage);

            spriteBatch.End();
            water.Draw();

            base.Draw(gameTime);
        }
    }
}
