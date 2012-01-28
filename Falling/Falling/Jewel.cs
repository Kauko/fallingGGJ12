using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Falling
{
    class Jewel : Item
    {
        int row, col;
        bool collected = false;
        Texture2D texture;
        FrameImage frameImage;

        public Jewel(Texture2D text, FrameImage img) 
        {
            this.texture = text;
            this.frameImage = img;
        }

        public int getRow() 
        {
            return row;
        }

        public int getCol() 
        {
            return col;
        }

        public bool isCollected() 
        {
            return collected;
        }

        public void setRow(int r) 
        {
            this.row = r;
        }

        public void setCol(int c) 
        {
            this.col = c;
            frameImage.fadeOut();
        }

        public void setCollected(bool c) 
        {
            this.collected = c;
        }
    }
}
