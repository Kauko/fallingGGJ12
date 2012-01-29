using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Falling
{
    class FrameImage
    {
        Texture2D clearTexture;
        Texture2D fadeTexture;

        Texture2D activeTexture;

        private Vector2 worldPosition;

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        public FrameImage(Texture2D c, Texture2D f, Vector2 p) 
        {
            this.clearTexture = c;
            this.fadeTexture = f;
            this.activeTexture = this.clearTexture;
            Position = p;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(activeTexture, Position, Color.White);
        }

        public void fadeOut() 
        {
            this.activeTexture = this.fadeTexture;
        }

        public void resetTexture() 
        {
            this.activeTexture = this.clearTexture;
        }
    }
}
