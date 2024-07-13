using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MonoFallGame2D;
using Statics;
using Microsoft.Xna.Framework;
using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace MonoFallGame2D.Clases
{
    public class AnimatedSprite
    {
        Dictionary<String, Texture2D> AtlasAnimacion;
        Dictionary<String, Vector2> AnimacionInfo;

        String AnimationName = "";

        float Counter = 0f;
        float Velocity = 8f / 60f;

        Point PositionFrame = new Point(0, 0);
        Point TotalFrames = new Point(0, 0);
        Point FrameSize;

        public AnimatedSprite(Dictionary<String, Texture2D> atlasAnimacion, Dictionary<String, Vector2> animacionInfo, String animationname, Point frameSize)
        {
            this.AtlasAnimacion = atlasAnimacion;
            this.AnimacionInfo = animacionInfo;
            this.AnimationName = animationname;
            this.TotalFrames = AnimacionInfo[AnimationName].ToPoint();
            this.FrameSize = frameSize;

        }

        public void SetAnimation(String animationname, float velocity)
        {
            if (this.AnimationName != animationname)
            {
                this.AnimationName = animationname;
                PositionFrame = new Point(0, 0);
                this.TotalFrames = AnimacionInfo[AnimationName].ToPoint();
                this.Velocity = velocity;
                this.Counter = 0f;
            }
        }

        public void Draw(SpriteBatch sb, Vector2 position)
        {
            Debug.WriteLine(PositionFrame);

            Counter += Velocity;
            if (Counter >= 1f)
            {
                if (PositionFrame == TotalFrames)
                {
                    PositionFrame = new Point(0, 0);
                    Counter = 0;
                }
                else if (PositionFrame.X == TotalFrames.X)
                {
                    PositionFrame.Y += 1;
                    PositionFrame.X = 0;
                    Counter = 0;

                }
                else
                {
                    Counter = 0;
                    PositionFrame.X += 1;
                }
            }
            sb.Draw(AtlasAnimacion[AnimationName], position, new(new Point(FrameSize.X * PositionFrame.X, FrameSize.Y * PositionFrame.Y), FrameSize), Color.White);
        }
    }
}
