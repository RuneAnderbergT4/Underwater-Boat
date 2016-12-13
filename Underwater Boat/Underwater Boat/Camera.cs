using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Underwater_Boat;

namespace Underwater_Boat
{
    public class Camera
    {
        protected float zoom; // Camera Zoom
        public Matrix transform; // Matrix Transform
        public Vector2 pos; // Camera Position
        protected float rotation; // Camera Rotation

        public bool FreeCamera { get; private set; }
        private KeyboardState _previousKeyboardState;
        
        public Camera()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
        }

        public void UpdateCamera(Sub currentSub)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.C) && _previousKeyboardState.IsKeyUp(Keys.C))
            {
                FreeCamera = !FreeCamera;
            }

            if (FreeCamera)
            {
                Move(keyboardState);

                if (keyboardState.IsKeyDown(Keys.PageUp))
                {
                    Zoom -= 0.1f;
                }
                else if (keyboardState.IsKeyDown(Keys.PageDown))
                {
                    Zoom += 0.1f;
                }
            }
            else
            {
                Zoom = 1.0f;
                Pos = currentSub.Position;
            }

            _previousKeyboardState = keyboardState;
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                zoom = MathHelper.Clamp(zoom, 0.5f, 1.2f);
            } // Negative zoom will flip image}
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public void Move(Vector2 amount)
        {
            pos += amount;

            pos.X = MathHelper.Clamp(pos.X, 0 + Game1.graphics.PreferredBackBufferWidth / 2, 4096 - Game1.graphics.PreferredBackBufferWidth / 2);
            pos.Y = MathHelper.Clamp(pos.Y, 0 + Game1.graphics.PreferredBackBufferHeight / 2, 2048 - Game1.graphics.PreferredBackBufferHeight / 2);
        }

        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Move(new Vector2(0, -10));
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            { 
                Move(new Vector2(0, 10));
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Move(new Vector2(-10, 0));
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                Move(new Vector2(10, 0));
            }
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            transform = Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width *
                0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return transform;
        }
    }
}
