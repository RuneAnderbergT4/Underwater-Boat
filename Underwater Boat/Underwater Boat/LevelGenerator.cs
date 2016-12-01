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

            List<List<Point>> polygons = new List<List<Point>>
            {
                GeneratePolygon(new Point(250, 250), 4)
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

            foreach (var polygon in polygons)
            {
                foreach (var point in polygon)
                {
                    // drawing points for testing purposes
                    col2d[point.X + 500, point.Y] = Color.Red;

                    col2d[point.X + 500 - 1, point.Y - 1] = Color.Red;
                    col2d[point.X + 500, point.Y - 1] = Color.Red;
                    col2d[point.X + 500 + 1, point.Y - 1] = Color.Red;
                    col2d[point.X + 500 + 1, point.Y] = Color.Red;
                    col2d[point.X + 500 + 1, point.Y + 1] = Color.Red;
                    col2d[point.X + 500, point.Y + 1] = Color.Red;
                    col2d[point.X + 500 - 1, point.Y + 1] = Color.Red;
                    col2d[point.X + 500 - 1, point.Y] = Color.Red;
                }
            }

            Color[] col = new Color[width*height];

            var p = 0;

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
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

            //Generate 4 starting points
            points.Add(new Point(center.X + rand.Next(-40, 40), center.Y + rand.Next(-240, -80)));
            points.Add(new Point(center.X + rand.Next(80, 240), center.Y + rand.Next(-40, 40)));
            points.Add(new Point(center.X + rand.Next(-40, 40), center.Y + rand.Next(80, 240)));
            points.Add(new Point(center.X + rand.Next(-240, -80), center.Y + rand.Next(-40, 40)));

            int length = 40;

            for (int i = 0; i < iterations; i++)
            {
                List<Point> tempList = new List<Point>();

                for (int j = 0; j < points.Count; j++)
                {
                    // To stop it from creating points to close to eachother
                    if (new Vector2(points[i].X - points[i + 1].X, points[i].Y - points[i + 1].Y).Length() > 10)
                        //if (Math.Abs(points[i].X - points[i + 1].X) > 8 && Math.Abs(points[i].Y - points[i + 1].Y) > 8)
                    {
                        // Creates a point inbetween with super fancy math
                        var angle =
                            Math.Atan((double) (points[(j + 1)%points.Count].X - points[j].X)/
                                      (points[(j + 1)%points.Count].Y - points[j].Y));
                        var newPoint = new Point
                        {
                            X = (points[j].X + (points[(j + 1)%points.Count].X - points[j].X)/2),
                            Y = (points[j].Y + (points[(j + 1)%points.Count].Y - points[j].Y)/2)
                        };

                        var hypotenuse = rand.Next(-(int) Math.Round(length/Math.Pow(2, i)),
                            (int) Math.Round(length/Math.Pow(2, i)));

                        var angle2 = Math.PI/2 - angle;

                        var opposite = hypotenuse*Math.Sin(angle2);
                        var adjacent = hypotenuse*Math.Cos(angle2);

                        var randPoint = new Point
                        {
                            X = (int) (newPoint.X - opposite),
                            Y = (int) (newPoint.Y + adjacent)
                        };

                        tempList.Add(randPoint);
                    }
                }

                var index = 1;

                foreach (var point in tempList)
                {
                    points.Insert(index, point);
                    index += 2;
                }
            }

            return points;
        }

        public static bool IntersectsWithPoint(List<Point> points, Vector2 point)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var norm = point - points[i].ToVector2();
                var line = points[(i + 1)%points.Count] - points[i];
                var normal = new Vector2(-line.Y, line.X);
                if ((normal.X*norm.X + normal.Y*norm.Y) < 0)
                    return false;
            }

            return true;
        }
    }
}