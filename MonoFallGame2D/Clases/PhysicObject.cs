using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Statics;
using System;
using System.Collections.Generic;

namespace MonoFallGame2D.Clases
{
    internal abstract class PhysicObject
    {
        protected Vector2 Velocity = new(0f, 0f);
        readonly protected float Friction = 30;
        readonly protected float Gravity = GameStatics.Gravity;
        readonly protected GraphicsDevice graphicsDevice = new(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
        protected Texture2D PolygonTexture;

        public Vector2 Position;
        protected Dictionary<String, Polygon4L> CollisionList;

        public virtual void Update(float delta) { }
        public virtual void Draw(SpriteBatch sb) { }
    }
}
