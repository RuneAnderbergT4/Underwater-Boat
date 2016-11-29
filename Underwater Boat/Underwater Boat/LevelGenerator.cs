using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Underwater_Boat
{
    public class LevelGenerator
    {
        public LevelGenerator()
        {
            
        }

        public static Texture2D GenerateLevel(GraphicsDevice gD, int width, int height)
        {
            Texture2D level = new Texture2D(gD, width, height, false, SurfaceFormat.Color);

            Color[,] col2d = new Color[width, height];

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    col2d[w, h] = new Color(w % 256, h % 256, 100);
                }
            }

            Color[] col = new Color[width * height];

            var p = 0;

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    col[p] = col2d[w, h];
                    p++;
                }
            }
            
            level.SetData(col);
            
            return level;
        }
    }
}