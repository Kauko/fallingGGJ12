using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Falling
{
    class Tile
    {
        int level;
        bool bridge = false;

        private Vector2 worldPosition;

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        public Tile(int strength) 
        {
            if (strength > 4)
                setBridge(true);
            level = strength * 5;
        }

        public int getLevel() 
        {
            return this.level;
        }

        public bool isBridge() 
        {
            return this.bridge;
        }

        public void setBridge(bool b) 
        {
            this.bridge = b;
            level = 99;
        }

        public void decreaseLevel() 
        {
            if(!isBridge())
                level--;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) 
        {
            spriteBatch.Draw(checkTexture(), position, Color.White);
        }

        public Texture2D checkTexture() 
        {
            if (level >= 0)
                return TextureRefs.tileLevel0;
            else if (level > 0 && level < 7)
                return TextureRefs.tileLevel1;
            else if (level > 6 && level < 13)
                return TextureRefs.tileLevel2;
            else if (level > 12 && level < 20)
                return TextureRefs.tileLevel3;
            else if (isBridge())
                return TextureRefs.jewel7;
            else
                return TextureRefs.tileLevel4;
        }
    }
}
