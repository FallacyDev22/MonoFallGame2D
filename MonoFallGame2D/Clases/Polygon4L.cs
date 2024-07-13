using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using MonoGame.Framework.Utilities;
using static MonoFallGame2D.Clases.Polygon4L;

namespace MonoFallGame2D.Clases
{
    internal class Polygon4L : Polygon //Esta clase es una clase hija de la clase Polygon
    {
        float X;
        float Y;
        float Width;
        float Height;

        Vector2 Offset;
        float NewWidth;
        float NewHeight;

        //Constructores.
        public Polygon4L(Vector2 position, float width, float height, Vector2 offset, float rotate = 0)
        {
            Vector2[] Points = SetPoints(position, width, height, offset);

            this.Offset = offset;
            this.X = position.X;
            this.Y = position.Y;
            this.NewHeight = height;
            this.NewWidth = width;

            PolygonPoints = Constructor4L(Points);
        }

        public Polygon4L(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4, Vector2 offset)
        {
            Vector2[] Points = new Vector2[4]
            {
                point1 + offset,
                point2 + offset,
                point3 + offset,
                point4 + offset
            };

            PolygonPoints = Constructor4L(Points);

            this.Offset = offset;
            this.X = this.PolygonPoints[0].X;
            this.Y = this.PolygonPoints[0].Y;
            this.NewWidth = this.PolygonPoints[1].X - this.PolygonPoints[0].X;
            this.NewHeight = this.PolygonPoints[3].Y - this.PolygonPoints[0].Y;
        }

        public Polygon4L(Vector2[] Points, Vector2 offset) 
        {
            PolygonPoints = Constructor4L(Points);

            this.Offset = offset;
            this.X = this.PolygonPoints[0].X;
            this.Y = this.PolygonPoints[0].Y;
            this.NewWidth = this.PolygonPoints[1].X - this.PolygonPoints[0].X;
            this.NewHeight = this.PolygonPoints[3].Y - this.PolygonPoints[0].Y;
        }

        //Funciones privadas
        private static List<Vector2> Constructor4L(Vector2[] points)
        {
            List<Vector2> polygonpoints = new();
            foreach (Vector2 point in points)
            {
                polygonpoints.Add(point);
            }
            return polygonpoints;
        }

        private Vector2[] SetPoints(Vector2 position, float width, float height, Vector2 offset)
        { //Esta es la función que calcula las posiciones de los 4 vectores
            Vector2[] Points = new Vector2[4]
            {
                new Vector2(position.X + offset.X, position.Y + offset.Y),
                new Vector2(position.X + offset.X + width, position.Y + offset.Y),
                new Vector2(position.X + offset.X + width, position.Y + offset.Y + height),
                new Vector2(position.X + offset.X, position.Y + offset.Y + height)
            };
            //Devuelve un Vector2[]
            return Points;
        }

        //Funciones publicas
        public void Update(Vector2 NewPosition)
        {
            PolygonPoints.Clear();
            Vector2[] Points = SetPoints(NewPosition, NewWidth, NewHeight, Offset);
            foreach (Vector2 point in Points)
            {
                PolygonPoints.Add(point);
            }
        }

        //Funciones publicas estaticas
        public static Rectangle ToRectangle(Polygon4L polygon4L)
        {
            polygon4L.X = polygon4L.PolygonPoints[0].X;
            polygon4L.Y = polygon4L.PolygonPoints[0].Y;
            polygon4L.Width = polygon4L.PolygonPoints[1].X - polygon4L.PolygonPoints[0].X;
            polygon4L.Height = polygon4L.PolygonPoints[3].Y - polygon4L.PolygonPoints[0].Y;
            Rectangle rectangle = new((int)polygon4L.X, (int)polygon4L.Y, (int)polygon4L.Width, (int)polygon4L.Height);
            return rectangle;
        }
        public static Polygon4L FromRectangle(Rectangle rectangle)
        {
            Vector2[] points = new Vector2[4]
            {
                new Vector2(rectangle.X, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
                new Vector2(rectangle.X, rectangle.Y + rectangle.Height)
            };

            Polygon4L polygon = new(points, Vector2.Zero);
            return polygon;
        }
    }
}
