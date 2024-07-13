using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace test.Clases
{
    internal class Polygon //Esta es la clase poligono, no la hija, sino la padre
    {
        public List<Vector2> PolygonPoints = new();

        public Polygon() { }
        public Polygon(Vector2[] Points) //Este es el constructor, recibe un array de Vectores y los agrega a una lista
        {
            foreach (Vector2 point in Points)
            {
                PolygonPoints.Add(point);
            }
        }

        public static bool IsColliding(Polygon polygon1, Polygon polygon2) //Debo hacer las conversiones porque el metodo de colision 
            //Solamente toma poligonos como argumentos, no rectangulos (Debido a que para la colision necesito los vertices de cada
            //figura, no sus caracteristicas
        {
            for (int i = 0; i < polygon1.PolygonPoints.Count; i++)
            {
                Vector2 VertexA = polygon1.PolygonPoints[i];
                Vector2 VertexB = polygon1.PolygonPoints[(i + 1) % polygon1.PolygonPoints.Count()];

                Vector2 VertexBA = VertexB - VertexA;
                Vector2 Axis = new Vector2(-VertexBA.Y, VertexBA.X);


                Polygon.ProjectVertice(polygon1, Axis, out float minA, out float maxA);
                Polygon.ProjectVertice(polygon2, Axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }
            }

            for (int i = 0; i < polygon2.PolygonPoints.Count; i++)
            {
                Vector2 VertexA = polygon2.PolygonPoints[i];
                Vector2 VertexB = polygon2.PolygonPoints[(i + 1) % polygon2.PolygonPoints.Count()];

                Vector2 VertexBA = VertexB - VertexA;
                Vector2 Axis = new Vector2(-VertexBA.Y, VertexBA.X);


                Polygon.ProjectVertice(polygon1, Axis, out float minA, out float maxA);
                Polygon.ProjectVertice(polygon2, Axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }
            }
            return true;
        }

        private static void ProjectVertice(Polygon polygon, Vector2 axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < polygon.PolygonPoints.Count(); i++)
            {
                float Value = Vector2.Dot(polygon.PolygonPoints[i], axis);

                if (Value < min) { min = Value; }
                if (Value > max) { max = Value; }
            }
        } //Todas esas dos funciones es el sistema de colisiones que utilizan los poligonos, llamado SAT, no entraré en profundidad

        public static Polygon FromRectangle(Rectangle rectangle)
        {
            Vector2[] points = new Vector2[4]
            {
                new Vector2(rectangle.X, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
                new Vector2(rectangle.X, rectangle.Y + rectangle.Height)
            };

            Polygon polygon = new Polygon(points);
            return polygon;
        } //Esta es una función que crea un Poligono a partir de un rectangulo

        //mi forma de definir un poligono es mediante 4 puntos en un plano, un rectangulo según MonoGame se define
        //por su altura, anchura, y un origen (un vector2) por lo que tengo que realizar algunos calculos para 
        //Transformar un Poligono de 4 lados a un rectangle o de un rectangle a un poligono de 4 lados.
    }
}
