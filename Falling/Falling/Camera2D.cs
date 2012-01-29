using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Falling
{
    class Camera2D
    {
        private SpriteBatch spriteRenderer;
        private Vector2 cameraPosition;


        public Vector2 cameraSpeedVector = new Vector2(500, 500);

        public Vector2 Position
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        public Camera2D(SpriteBatch renderer)
        {
            spriteRenderer = renderer;
            cameraPosition = new Vector2(0, 0);
        }

        public void DrawTile(Tile tile) 
        {
            Vector2 drawPosition = ApplyTransformations(tile.Position);
            tile.Draw(spriteRenderer, drawPosition);
        }

        public void DrawPlayer(Player player) 
        {
            Vector2 drawPosition = ApplyTransformations(player.Position);
            player.Draw(spriteRenderer, drawPosition);
        }

        public void DrawItem(Item i) 
        {
            Vector2 drawPosition = ApplyTransformations(i.Position);
            i.Draw(spriteRenderer, drawPosition);
        }

        public Vector2 ApplyTransformations(Vector2 nodePosition)
        {
            // apply translation
            Vector2 finalPosition = nodePosition - cameraPosition;
            // you can apply scaling and rotation here also
            //.....
            //--------------------------------------------
            return finalPosition;
        }

        public Vector2 TransformMouse(Vector2 mousePosition) 
        {
            Vector2 ret = mousePosition + cameraPosition;
            return ret;
        }

        public void Translate(Vector2 moveVector, GameTime theGameTime, int rows, int cols)
        {
            cameraPosition += moveVector * cameraSpeedVector * (float)theGameTime.ElapsedGameTime.TotalSeconds;;
            //cameraPosition.X = MathHelper.Clamp(cameraPosition.X, -440, rows * C.tileHeight);
            //cameraPosition.Y = MathHelper.Clamp(cameraPosition.Y, -50, cols * C.tileWidth);
        }

    }
}
