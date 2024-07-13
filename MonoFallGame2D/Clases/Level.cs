using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFallGame2D.Clases
{
    public class Level //La clase Level
    {
        private int x = 0;
        private int y = 0;
        private List<Rectangle> rectangles = new();
        readonly private string level =
            "XXXXXXXXXXXXXXXXXXXXXXXXX/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "X                       X/" +
            "XXXXXXXXXXXXXXXXXXXXXXXXX/"; //Este es el nivel

        public List<Rectangle> CreateLevel()
        {
            foreach (char c in level.ToCharArray())
            {
                if (c.ToString() == "X")
                {
                    rectangles.Add(new Rectangle(x * 32, y * 32, 32, 32));
                    Console.WriteLine("lol");
                    x++;
                }
                else if (c.ToString() == " ")
                {
                    x++;
                }
                else if (c.ToString() == "/")
                {
                    x = 0;
                    y++;
                }
            }
            return rectangles;
        } //Este es el metodo que crea el nivel

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            foreach (Rectangle r in rectangles)
            {
                sb.Draw(texture, r, Color.White);
            }
        } //Este es el metodo que dibuja el nivel
    }

}
