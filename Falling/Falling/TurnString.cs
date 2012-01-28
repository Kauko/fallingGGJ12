using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Falling
{
    class TurnString : StringClass
    {
        protected int value;
        int i;
        int x;
        int y;

        public TurnString(Texture2D inTexture, int initialValue, int x, int y) :
            base(inTexture)
        {
            value = initialValue;
            this.x = x;
            this.y = y;
        }

        public void SetValue(int a)
        {
            value = a;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            char[] charArray = value.ToString().ToCharArray();

            for (i = 0; i <= charArray.Length - 1; i++)
            {
                switch (charArray[i])
                {
                    case '0':
                        DrawNumber(spriteBatch, 0, charArray.Length, i);
                        break;
                    case '1':
                        DrawNumber(spriteBatch, 1, charArray.Length, i);
                        break;
                    case '2':
                        DrawNumber(spriteBatch, 2, charArray.Length, i);
                        break;
                    case '3':
                        DrawNumber(spriteBatch, 3, charArray.Length, i);
                        break;
                    case '4':
                        DrawNumber(spriteBatch, 4, charArray.Length, i);
                        break;
                    case '5':
                        DrawNumber(spriteBatch, 5, charArray.Length, i);
                        break;
                    case '6':
                        DrawNumber(spriteBatch, 6, charArray.Length, i);
                        break;
                    case '7':
                        DrawNumber(spriteBatch, 7, charArray.Length, i);
                        break;
                    case '8':
                        DrawNumber(spriteBatch, 8, charArray.Length, i);
                        break;
                    case '9':
                        DrawNumber(spriteBatch, 9, charArray.Length, i);
                        break;
                }
            }

        }

        public void DrawNumber(SpriteBatch spriteBatch, int n, int digits, int i)
        {
            int width = spriteTexture.Width / C.stringAtlasLenght;
            int height = spriteTexture.Height;

            int column = n % C.stringAtlasLenght;

            Rectangle sourceRectangle = new Rectangle(width * column, 0, width, height);

            Vector2 position = new Vector2((float)(x - digits * C.letterSize + i * C.letterSize), (float)y);
            spriteBatch.Draw(spriteTexture, position, sourceRectangle, Color.Black);
        }
    }
}
