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

        private Vector2 ApplyTransformations(Vector2 nodePosition)
        {
            // apply translation
            Vector2 finalPosition = nodePosition - cameraPosition;
            // you can apply scaling and rotation here also
            //.....
            //--------------------------------------------
            return finalPosition;
        }

        public void Translate(Vector2 moveVector)
        {
            cameraPosition += moveVector;
        }

    }
}
