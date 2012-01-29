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
            //if (strength > 4)
                //setBridge(true);
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
            if (level <= 0)
                return TextureRefs.tileLevel0;
            else if (level > 0 && level < 6) //1,2,3,4,5
                return TextureRefs.tileLevel1;
            else if (level > 5 && level < 11) //6,7,8,9,10
                return TextureRefs.tileLevel2;
            else if (level > 10 && level < 16) //11,12,13,14,15
                return TextureRefs.tileLevel3;
            else if (level > 20 && level < 99)
                return TextureRefs.tileLevel5;
            else if (isBridge())
                return TextureRefs.playerBridge;
            else
                return TextureRefs.tileLevel4; //16,17,18,19,20
        }
    }
}
