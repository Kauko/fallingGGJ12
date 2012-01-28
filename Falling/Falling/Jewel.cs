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
            return this.row;
        }

        public int getCol() 
        {
            return this.col;
        }

        public bool isCollected() 
        {
            return collected;
        }



        public void setCollected(bool c) 
        {
            this.collected = c;
            if(c)
                frameImage.fadeOut();
        }
    }
}
