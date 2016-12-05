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

            Color[,] col2D = new Color[level.Width, level.Height];

            List<List<Point>> polygons = new List<List<Point>>
            {
                GeneratePolygon(new Point(250, 250), 8)
            };

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    foreach (var polygon in polygons)
                    {
                        if (IsPointInPolygon(polygon, new Point(w, h)))
                        {
                            col2D[w, h] = Color.Red;
                        }
                    }
                }
            }

            foreach (var polygon in polygons)
            {
                foreach (var point in polygon)
                {
                    // drawing points for testing purposes
                    col2D[point.X + 500, point.Y + 200] = Color.Red;

                    col2D[point.X + 500 - 1, point.Y - 1 + 200] = Color.Red;
                    col2D[point.X + 500, point.Y - 1 + 200] = Color.Red;
                    col2D[point.X + 500 + 1, point.Y - 1 + 200] = Color.Red;
                    col2D[point.X + 500 + 1, point.Y + 200] = Color.Red;
                    col2D[point.X + 500 + 1, point.Y + 1 + 200] = Color.Red;
                    col2D[point.X + 500, point.Y + 1 + 200] = Color.Red;
                    col2D[point.X + 500 - 1, point.Y + 1 + 200] = Color.Red;
                    col2D[point.X + 500 - 1, point.Y + 200] = Color.Red;
                }
            }

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

            level.SetData(col);

            return level;
        }

        public static List<Point> GeneratePolygon(Point center, int iterations)
        {
            List<Point> points = new List<Point>();

            Random rand = new Random();

            //Generate 4 starting points
            points.Add(new Point(center.X + rand.Next(-80, 80), center.Y + rand.Next(-300, -100)));
            points.Add(new Point(center.X + rand.Next(100, 300), center.Y + rand.Next(-80, 80)));
            points.Add(new Point(center.X + rand.Next(-80, 80), center.Y + rand.Next(100, 300)));
            points.Add(new Point(center.X + rand.Next(-300, -100), center.Y + rand.Next(-80, 80)));

            int length = 60;

            for (int i = 0; i < iterations; i++)
            {
                List<Point> tempList = new List<Point>();

                for (int j = 0; j < points.Count; j++)
                {
                    // To stop it from creating points to close to eachother
                    if (new Vector2(points[i].X - points[i + 1].X, points[i].Y - points[i + 1].Y).Length() > 10)
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

        public static bool IsPointInPolygon(List<Point> polygon, Point point)
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

        #region IsPointInPolygon
        // Return true if the point is in the polygon.
        public static bool IsPointInPolygon(float x, float y, List<Point> polygon)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int maxPoint = polygon.Count - 1;
            float totalAngle = GetAngle(
                polygon[maxPoint].X, polygon[maxPoint].Y,
                x, y,
                polygon[0].X, polygon[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < maxPoint; i++)
            {
                totalAngle += GetAngle(
                    polygon[i].X, polygon[i].Y,
                    x, y,
                    polygon[i + 1].X, polygon[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(totalAngle) > 0.000001);
        }

        // Return the angle ABC.
        // Return a value between PI and -PI.
        // Note that the value is the opposite of what you might
        // expect because Y coordinates increase downward.
        public static float GetAngle(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the dot product.
            float dotProduct = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            float crossProduct = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return (float)Math.Atan2(crossProduct, dotProduct);
        }

        // Return the cross product AB x BC.
        // The cross product is a vector perpendicular to AB
        // and BC having length |AB| * |BC| * Sin(theta) and
        // with direction given by the right-hand rule.
        // For two vectors in the X-Y plane, the result is a
        // vector with X and Y components 0 so the Z component
        // gives the vector's length and direction.
        public static float CrossProductLength(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }

        // Return the dot product AB · BC.
        // Note that AB · BC = |AB| * |BC| * Cos(theta).
        private static float DotProduct(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }
        #endregion
    }
}