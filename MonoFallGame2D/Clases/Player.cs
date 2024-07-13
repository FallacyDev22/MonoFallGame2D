using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Clases
{
    internal class Player //Clase Player
    {
        private Vector2 Velocity = new Vector2(0f, 0f);
        private Vector2 PositionVector;
        private Vector2 PreviousPositionVector; //Se crean unos vectores de velocidad y posiciones

        private float Friction = 30;

        private bool LeftCollision = false;
        private bool RightCollision = false;

        private Texture2D PolygonTexture;
        private GraphicsDevice graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
        private Dictionary<String, Polygon4L> CollisionList; //Se crea un diccionario de String y PolygonL4


        public Rectangle Rectangle
        {
            get => new Rectangle((int)PositionVector.X, (int)PositionVector.Y, 64, 64);
        }

        private float Gravity = 20f;

        private bool SpaceJustPressed = true;

        public Player(Vector2 position) //Este es el constructor del Player, recibe la posicion 
        {
            this.PositionVector = position; //Aqui asignamos la posicion al objeto

            this.CollisionList = new Dictionary<String, Polygon4L>
            {
                { "Down", new Polygon4L(PositionVector, 32, 16, new Vector2(16, 48)) },
                { "Left", new Polygon4L(PositionVector, 16, 32, new Vector2(0, 16)) },
                { "Right", new Polygon4L(PositionVector, 16, 32, new Vector2(48, 16)) },
                { "Up", new Polygon4L(PositionVector, 32, 16, new Vector2(16, 0)) },
            };
            //Aquí se crean los polygonos de 4 lados, con esas caracteristicas
            //ahora revisemos la clase Polygon4L
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            
            PolygonTexture = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = new Color[1] { Color.White };
            PolygonTexture.SetData<Color>(data);
            sb.Draw(texture, new Rectangle((int)PositionVector.X, (int)PositionVector.Y, texture.Width * 4, texture.Height * 4), Color.White);

            foreach (string Polygon in CollisionList.Keys)
            {
                sb.Draw(PolygonTexture, CollisionList[Polygon].ToRectangle(), Color.Black);
            }
            //Toda esta es la función de dibujo
        }

        public void Update(float delta)
        {
            if (IsOnFloor())
            {
                Velocity.Y = 0;
            }
            else
            {
                Velocity.Y += Gravity/2;
            }
            if (IsOnWall())
            {
                Velocity.X = 0;
            }

            HandledInputs(delta);

            PreviousPositionVector = PositionVector;
            PositionVector += (Velocity * delta);

            foreach (Polygon4L Raycast in CollisionList.Values)
            {
                Raycast.Update(PositionVector);
            }
            //Toda esta es la función de Actualización/Update

            //lo que sigue son los comprobadores de si se está en el suelo o contra una pared
        }

        private bool IsOnFloor()
        {
            foreach (var r in GameStatics.RectanglesList.Where(r => Polygon.IsColliding(CollisionList["Down"], Polygon.FromRectangle(r))))
            {
                PositionVector.Y = r.Y - 64;
                return true;
            }
            return false;
        }
        

        private bool IsOnWall()
        {
            foreach (Rectangle r in GameStatics.RectanglesList)
            {
                if (Polygon.IsColliding(CollisionList["Left"], Polygon.FromRectangle(r)))
                {
                    PositionVector.X = r.X + 32;
                    return true;
                }
                else if (Polygon.IsColliding(CollisionList["Right"], Polygon.FromRectangle(r)))
                {
                    PositionVector.X = r.X - 64;
                    RightCollision = true;
                    return true;
                }
            }
            LeftCollision = false;
            RightCollision = false;
            return false;
        }

        private void HandledInputs(float delta)
        {
            KeyboardState Input = Keyboard.GetState();

            if (Input.IsKeyDown(Keys.A) && !LeftCollision)
            {
                Velocity.X -= 64f;
            }
            if (Input.IsKeyDown(Keys.D) && !RightCollision)
            {
                Velocity.X += 64f;
            }
            else
            {
                Velocity.X -= MathF.Min(MathF.Abs(Velocity.X), Friction) * MathF.Sign(Velocity.X);
            }

            if (Input.IsKeyDown(Keys.Space) && SpaceJustPressed && IsOnFloor())
            {
                Velocity.Y -= 600f;
                SpaceJustPressed = false;
            }
            if (Input.IsKeyUp(Keys.Space))
            {
                SpaceJustPressed = true;
            }
        }
        //Aqui se manejan los inputs
    }
}
