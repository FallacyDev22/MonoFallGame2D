using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFallGame2D
{
    public class Loader
    {
        public static Dictionary<String, Texture2D> TexturesPlayer = new();
        public static Dictionary<String, Vector2> InfoPlayer = new();

        ContentManager Content;

        public Loader(ContentManager content)
        {
            this.Content = content;
        }

        public void Load()
        {
            AddAnimation(TexturesPlayer, InfoPlayer, "Idle", new Vector2(2, 3));
            AddAnimation(TexturesPlayer, InfoPlayer, "RightWalk", new Vector2(2, 5));
            AddAnimation(TexturesPlayer, InfoPlayer, "LeftWalk", new Vector2(4, 3));

        }

        private void AddAnimation(Dictionary<String, Texture2D> DictionaryAnimation, Dictionary<String, Vector2> DictionaryInfo, String AnimationName, Vector2 ColumnsAndRows)
        {
            DictionaryAnimation.Add(AnimationName, Content.Load<Texture2D>(AnimationName));
            DictionaryInfo.Add(AnimationName, new Vector2(ColumnsAndRows.X - 1, ColumnsAndRows.Y - 1));
        }
    }
}
