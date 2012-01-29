using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Falling
{
    class Bird : Item
    {
        bool collected = false;

        public Bird(Texture2D text,Vector2 position, int col, int row) 
        {
            Position = position;
            setCol(col);
            setRow(row);
            this.texture = text;
        }

        public bool isCollected() 
        {
            return this.collected;
        }

        public void setCollected(bool c) 
        {
            this.collected = c;
        }
    }
}
