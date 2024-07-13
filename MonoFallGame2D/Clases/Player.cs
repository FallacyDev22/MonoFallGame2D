using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Statics;
using MonoFallGame2D.Clases;

namespace MonoFallGame2D.Clases
{
    internal class Player : PhysicObject
    {

        private bool LeftCollision = false;
        private bool RightCollision = false;

        private float AcelerationX = 64f;
        private float JumpForce = 600f;

        private AnimatedSprite Sprite;

        public Rectangle Rectangle
        {
            get => new((int)Position.X, (int)Position.Y, 64, 64);
        }

        private bool SpaceJustPressed = true;

        public Player(Vector2 position)
        {
            this.Position = position;

            this.CollisionList = new Dictionary<String, Polygon4L>
            {
                { "Down", new(Position, 32, 16, new Vector2(16, 48)) },
                { "Left", new(Position, 16, 32, new Vector2(0, 16)) },
                { "Right", new(Position, 16, 32, new Vector2(48, 16)) },
                { "Up", new(Position, 32, 16, new Vector2(16, 0)) },
            };
        }

        public void LoadSprite()
        {
            Sprite = new(Loader.TexturesPlayer, Loader.InfoPlayer, "Idle", new Point(64, 64));
        }

        public override void Draw(SpriteBatch sb)
        {
            
            PolygonTexture = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = new Color[1] { Color.White };
            PolygonTexture.SetData<Color>(data);

            Sprite.Draw(sb, Position);

            foreach (string Polygon in CollisionList.Keys)
            {
                sb.Draw(PolygonTexture, Polygon4L.ToRectangle(CollisionList[Polygon]), Color.Black);
            }
            //Toda esta es la función de dibujo
        }

        public override void Update(float delta)
        {
            if (IsOnFloor())
            {
                Velocity.Y = 0;
            }
            else
            {
                Velocity.Y += Gravity;
            }
            if (IsOnWall())
            {
                Velocity.X = 0;
            }

            HandledInputs();
            SelectorAnimation();

            Position += (Velocity * delta);

            foreach (Polygon4L Raycast in CollisionList.Values)
            {
                Raycast.Update(Position);
            }
        }

        private void SelectorAnimation()
        {
            KeyboardState Input = Keyboard.GetState();

            if (!Input.IsKeyDown(Keys.D) && !Input.IsKeyDown(Keys.A)) { Sprite.SetAnimation("Idle", 8f / 60f); }

            else if (Input.IsKeyDown(Keys.D)) { Sprite.SetAnimation("RightWalk", 8f / 60f); }

            else if (Input.IsKeyDown(Keys.A)) { Sprite.SetAnimation("LeftWalk", 8f / 60f); }
        }

        private bool IsOnFloor()
        {
            if (GameStatics.RectanglesList.Count != 0)
            {
                foreach (var r in GameStatics.RectanglesList.Where(r => Polygon.IsColliding(CollisionList["Down"], Polygon4L.FromRectangle(r))))
                {
                    Position.Y = r.Y - 64;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        

        private bool IsOnWall()
        {
            if (GameStatics.RectanglesList.Count != 0)
            {
                foreach (Rectangle r in GameStatics.RectanglesList)
                {
                    if (Polygon.IsColliding(CollisionList["Left"], Polygon4L.FromRectangle(r)))
                    {
                        Position.X = r.X + 32;
                        LeftCollision = true;
                        return true;
                    }
                    else if (Polygon.IsColliding(CollisionList["Right"], Polygon4L.FromRectangle(r)))
                    {
                        Position.X = r.X - 64;
                        RightCollision = true;
                        return true;
                    }
                }
                LeftCollision = false;
                RightCollision = false;
                return false;
            }
            LeftCollision = false;
            RightCollision = false;
            return false;
        }

        private void HandledInputs()
        {
            KeyboardState Input = Keyboard.GetState();

            if (Input.IsKeyDown(Keys.A) && !LeftCollision)
            {
                Velocity.X -= AcelerationX;
            }
            if (Input.IsKeyDown(Keys.D) && !RightCollision)
            {
                Velocity.X += AcelerationX;
            }
            else
            {
                Velocity.X -= MathF.Min(MathF.Abs(Velocity.X), Friction) * MathF.Sign(Velocity.X);
            }

            if (Input.IsKeyDown(Keys.Space) && SpaceJustPressed && IsOnFloor())
            {
                Velocity.Y -= JumpForce;
                SpaceJustPressed = false;
            }

            if (Input.IsKeyUp(Keys.Space))
            {
                SpaceJustPressed = true;
            }
        }
    }
}
