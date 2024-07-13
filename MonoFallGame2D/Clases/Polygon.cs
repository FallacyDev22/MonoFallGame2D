using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MonoFallGame2D.Clases
{
    internal class Polygon
    {
        public List<Vector2> PolygonPoints = new();

        //Constructor base.
        protected Polygon() { }
        
        //Constructor
        public Polygon(Vector2[] Points)
        {
            foreach (Vector2 point in Points)
            {
                PolygonPoints.Add(point);
            }
        }

        //Funciones publicas estaticas
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

        //Funciones privadas
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
        }

    }
}
