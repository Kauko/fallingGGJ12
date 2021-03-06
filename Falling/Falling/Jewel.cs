﻿using System;
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
        FrameImage frameImage;

        public Jewel(Texture2D text, FrameImage img) 
        {
            this.texture = text;
            this.frameImage = img;
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
