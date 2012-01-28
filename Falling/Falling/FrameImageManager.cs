using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Falling
{
    class FrameImageManager
    {
        List<FrameImage> frameImages;

        public void addFrameImage(Texture2D texture, Vector2 pos) 
        { 
            frameImages.Add(new FrameImage(texture, pos));
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            foreach (FrameImage i in frameImages) 
            {
                i.Draw(spriteBatch);
            }
        }
    }
}
