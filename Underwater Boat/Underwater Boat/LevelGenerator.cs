using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            Random rand = new Random();

            List<List<Point>> polygons = new List<List<Point>>
            {
                GeneratePolygon(new Point(200, 200), 1)
            };

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    if (IntersectsWithPoint(polygons[0], new Vector2(w, h)))
                    {
                        col2d[w, h] = Color.Red;
                    }
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

        public static List<Point> GeneratePolygon(Point center, int iterations)
        {
            List<Point> points = new List<Point>();

            Random rand = new Random();

            // Generate 4 starting points
            points.Add(new Point(center.X + rand.Next(-20, 20), center.Y + rand.Next(-120, -60)));
            points.Add(new Point(center.X + rand.Next(60, 120), center.Y + rand.Next(-20, 20)));
            points.Add(new Point(center.X + rand.Next(-20, 20), center.Y + rand.Next(60, 120)));
            points.Add(new Point(center.X + rand.Next(-120, -60), center.Y + rand.Next(-20, 20)));

            for (int i = 0; i < iterations; i++)
            {
                // Insert new points inbetween the others
            }

            return points;
        }

        public static bool IntersectsWithPoint(List<Point> points, Vector2 point)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var norm = point - points[i].ToVector2();
                var line = points[(i + 1) % points.Count] - points[i];
                var normal = new Vector2(-line.Y, line.X);
                if ((normal.X * norm.X + normal.Y * norm.Y) < 0)
                    return false;
            }

            return true;
        }
    }
}