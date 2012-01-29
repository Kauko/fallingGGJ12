using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Falling
{
    class Item
    {
        private Vector2 worldPosition;
        protected int row, col;
        protected Texture2D texture;

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void setRow(int r)
        {
            this.row = r;
        }

        public void setCol(int c)
        {
            this.col = c;

        }

        public int getRow()
        {
            return this.row;
        }

        public int getCol()
        {
            return this.col;
        }

    }
}
