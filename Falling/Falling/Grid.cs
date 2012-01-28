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
        Player[,] deadPlayers;

        public Grid(int gridX, int gridY, int gridSize)
        {
            x = gridX;
            y = gridY;
            tileSize = gridSize;
        }

        public bool mouseClicked(int x, int y)
        {
            if (tileAdjacentToPlayer(x, y))
            {
                currentPlayer.setPosition(x, y);
                foreach(Tile in tiles)
                {
                    
                }

                return true;
            }
            else
                return false;


        }

        public void setCurrentPlayer(Player player)
        {
            currentPlayer = player;
        }

        public void addDeadPlayer(Player p)
        {
            deadPlayers[p.getX(), p.getY()] = p; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public bool tileAdjacentToPlayer(int col, int row)
        {
            int pcol = currentPlayer.getCol();
            int prow = currentPlayer.getRow();
            if (col == pcol && prow == row - 1 || prow == row + 1)
                return true;
            else if (row == prow && pcol == col - 1 || pcol == y + 1)
                return true;
            else
                return false;
        }


    }
}
