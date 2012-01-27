using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Falling
{
    class Grid
    {
        int x, y, tileSize;
        int rows, cols;

        Tile[,] tiles;
        Player currentPlayer;
        Player[,] otherPlayers;

        public Grid(int gridX, int gridY, int gridSize)
        {
            x = gridX;
            y = gridY;
            tileSize = gridSize;
        }

        public void mouseClicked(int x, int y)
        {
            if (tileAdjacentToPlayer(x, y)) 
            {
                currentPlayer.setPosition(x, y);
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public bool tileAdjacentToPlayer(int x, int y)
        {
            int px = currentPlayer.getX();
            int py = currentPlayer.getY();
            if (y == py && px == x - 1 || px == x + 1)
                return true;
            else if (x == px && py == y - 1 || py == y + 1)
                return true;
            else
                return false;
        }


    }
}
