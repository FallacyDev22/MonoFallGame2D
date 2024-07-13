using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using MonoGame.Framework.Utilities;
using static test.Clases.Polygon4L;

namespace test.Clases
{
    internal class Polygon4L : Polygon //Esta clase es una clase hija de la clase Polygon
    {
        float X;
        float Y;
        float Width;
        float Height;
        //Tiene variables de X, Y, Width, Height (X, Y, ancho y altura)

        Vector2 Offset;
        float NewWidth;
        float NewHeight;

        public Polygon4L(Vector2 position, float width, float height, Vector2 offset, float rotate = 0) //Este es el constructor de la clase
        {
            Vector2[] Points = SetPoints(position, width, height, offset);
            //Ese Vector2[] Points es un array de vectores
            //Dentro del constructor de la clase podemos poner funciones

            this.Offset = offset;
            this.NewHeight = height;
            this.NewWidth = width;

            //El Vector2[] que devuelve se utiliza aquí
            foreach (Vector2 point in Points)
            {
                PolygonPoints.Add(point);
            } //un bucle que agrega cada punto a una lista de Vectores
        } //Todo ese es el primer constructor de Polygonos de 4 Lados

        public Polygon4L(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
        {
            Vector2[] Points = new Vector2[4]
            {
                point1,
                point2,
                point3,
                point4
            };
            //En este caso, en vez de dar la posicion, la altura, la anchura y un offset, damos directamente los 4 puntos y se crea el nuevo poligono
        } //Este es un segundo constructor de Polygonos de 4 lados 
        //Si, podemos tener varios constructores de una misma clase

        public void Update(Vector2 NewPosition)
        {
            PolygonPoints.Clear();
            Vector2[] Points = SetPoints(NewPosition, NewWidth, NewHeight, Offset);
            foreach (Vector2 point in Points)
            {
                PolygonPoints.Add(point);
            }
        } //Esta es la función Update, actualiza los poligono a una nueva posición

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

        public Rectangle ToRectangle()
        {
            X = PolygonPoints[0].X;
            Y = PolygonPoints[0].Y;
            Width = PolygonPoints[1].X - PolygonPoints[0].X;
            Height = PolygonPoints[3].Y - PolygonPoints[0].Y;
            Rectangle rectangle = new((int)X, (int)Y, (int)Width, (int)Height);
            return rectangle;
        } //Y esta es una función que transforma un poligono de 4 lados en un rectangle que admite MonoGame para dibujar sprites.
    }
}
