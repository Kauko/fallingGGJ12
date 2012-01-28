using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falling
{
    public class Level
    {
        public string[,] tiles;

        public Level(string[,] levelData)
        {
            tiles = levelData;
        }

        public string GetTileContent(int row, int col)
        {
            return tiles[row, col];
        }

        public int GetRows()
        {
            return tiles.GetLength(0);
        }

        public int GetCols()
        {
            return tiles.GetLength(1);
        }
    }
}
