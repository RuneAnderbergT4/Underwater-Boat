using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Underwater_Boat
{
    public class LevelGenerator
    {
        public static Texture2D Result { get; private set; }
        public string Progress { get; private set; }
        public bool IsAlive { get { return _thread != null && _thread.IsAlive; } }

        private Thread _thread;

        public LevelGenerator()
        {
            Progress = "";
        }
        
        public void StartGenerateLevel(GraphicsDevice gD, int width, int height, IServiceBus iSB)
        {
            if (IsAlive)
                throw new Exception("Thread already working!");

            _thread = new Thread(() => GenerateLevel(gD, width, height, iSB));
            _thread.Start();
        }

        private void GenerateLevel(GraphicsDevice gD, int width, int height, IServiceBus iSB)
        {
            Texture2D level = new Texture2D(gD, width, height, false, SurfaceFormat.Color);
            
            List<List<Point>> polygons = new List<List<Point>>
            {
                GeneratePolygon(new Point(width * 1/8 + iSB.Next(-200, 200), height * 1/3 + iSB.Next(-200, 200)), 6, iSB),
                GeneratePolygon(new Point(width * 3/8 + iSB.Next(-200, 200), height * 1/3 + iSB.Next(-200, 200)), 6, iSB),
                GeneratePolygon(new Point(width * 5/8 + iSB.Next(-200, 200), height * 1/3 + iSB.Next(-200, 200)), 6, iSB),
                GeneratePolygon(new Point(width * 7/8 + iSB.Next(-200, 200), height * 1/3 + iSB.Next(-200, 200)), 6, iSB),
                GenerateBottom(width, height, 8, iSB)
            };
            
            Color[,] col2D = new Color[level.Width, level.Height];

            List<Boundaries> boundsToCheck = new List<Boundaries>();

            Progress = "Setting boundaries...";

            foreach (var polygon in polygons)
            {
                Boundaries b = new Boundaries(polygon[0].Y, polygon[0].Y + 1, polygon[0].X, polygon[0].X + 1);

                foreach (var point in polygon)
                {
                    if (point.X < b.Left || point.X > b.Right || point.Y < b.Top || point.Y > b.Bottom)
                    {
                        if (point.X < b.Left)
                        {
                            b.Left = point.X;
                        }
                        else if (point.X > b.Right)
                        {
                            b.Right = point.X;
                        }

                        if (point.Y < b.Top)
                        {
                            b.Top = point.Y;
                        }
                        else if (point.Y > b.Bottom)
                        {
                            b.Bottom = point.Y;
                        }
                    }
                }

                boundsToCheck.Add(b);
            }

            Progress = "Converting polygons to 2D color array...";
            
            Parallel.For(0, width, w =>
            {
                for (int h = 0; h < height; h++)
                {
                    for (int i = 0; i < boundsToCheck.Count; i++)
                    {
                        if (w >= boundsToCheck[i].Left && w <= boundsToCheck[i].Right && h >= boundsToCheck[i].Top &&
                            h <= boundsToCheck[i].Bottom)
                        {
                            if (IsPointInPolygon(polygons[i], new Point(w, h)))
                            {
                                col2D[w, h] = Color.DarkSeaGreen;
                            }
                        }
                    }
                }
            });

            Progress = "Converting to 1D color array...";

            Color[] col = new Color[level.Width*level.Height];

            var p = 0;

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    col[p] = col2D[w, h];
                    p++;
                }
            }

            Progress = "Setting Texture2D data...";

           level.SetData(col);

            Result = level;

            Progress = "Job's done...";
        }

        private List<Point> GeneratePolygon(Point center, int iterations, IServiceBus iSB)
        {
            Progress = "Generating polygons...";

            List<Point> points = new List<Point>
            {
                new Point(center.X + iSB.Next(-80, 80), center.Y + iSB.Next(-400, -200)),
                new Point(center.X + iSB.Next(200, 400), center.Y + iSB.Next(-80, 80)),
                new Point(center.X + iSB.Next(-80, 80), center.Y + iSB.Next(200, 400)),
                new Point(center.X + iSB.Next(-400, -200), center.Y + iSB.Next(-80, 80))
            };
            
            int length = 120;

            for (int i = 0; i < iterations; i++)
            {
                List<Point> tempList = new List<Point>();

                for (int j = 0; j < points.Count; j++)
                {
                    // To stop it from creating points to close to eachother
                    if (new Vector2(points[j].X - points[(j + 1) % points.Count].X, points[j].Y - points[(j + 1) % points.Count].Y).Length() > 10)
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

                        var hypotenuse = iSB.Next(-(int) Math.Round(length/Math.Pow(2, i)),
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

        private List<Point> GenerateBottom(int width, int height, int iterations, IServiceBus iSB)
        {
            Progress = "Generating bottom...";

            List<Point> points = new List<Point>
            {
                new Point(0, iSB.Next(height - 400, height - 100)),
                new Point(width/2, iSB.Next(height - 600, height - 200)),
                new Point(width, iSB.Next(height - 400, height - 100))
            };

            int length = 400;

            for (int i = 0; i < iterations; i++)
            {
                List<Point> tempList = new List<Point>();

                for (int j = 0; j < points.Count - 1; j++)
                {
                    // To stop it from creating points to close to eachother
                    if (new Vector2(points[j].X - points[j + 1].X, points[j].Y - points[j + 1].Y).Length() > 10)
                    {
                        var newPoint = new Point
                        {
                            X = points[j].X + (points[j + 1].X - points[j].X) / 2,
                            Y = iSB.Next((points[j].Y + (points[j + 1].Y - points[j].Y) / 2) - (int) Math.Round(length / Math.Pow(1.8, i)), (points[j].Y + (points[j + 1].Y - points[j].Y) / 2 + (int)Math.Round(length / Math.Pow(1.8, i))))
                        };

                        if (newPoint.Y > height - 50)
                        {
                            newPoint.Y = height - 50;
                        }

                        tempList.Add(newPoint);
                    }
                }

                var index = 1;

                foreach (var point in tempList)
                {
                    points.Insert(index, point);
                    index += 2;
                }
            }

            points.Insert(0, new Point(0, height));
            points.Add(new Point(width, height));

            return points;
        }

        //public static bool IntersectsWithPoint(List<Point> points, Vector2 point)
        //{
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        var norm = point - points[i].ToVector2();
        //        var line = points[(i + 1)%points.Count] - points[i];
        //        var normal = new Vector2(-line.Y, line.X);
        //        if ((normal.X*norm.X + normal.Y*norm.Y) < 0)
        //            return false;
        //    }

        //    return true;
        //} 

        private bool IsPointInPolygon(List<Point> polygon, Point point)
        {
            bool isInside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }
    }
}