using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuttoPuputLevel
{
    public class Level
    {
        public string[,] tiles;
        private int target;
        private int cycle;

        public Level(string[,] levelData, int levelTarget, int levelCycle)
        {
            tiles = levelData;
            target = levelTarget;
            cycle = levelCycle;
        }

        public string GetTileContent(int row, int col)
        {
            return tiles[row, col];
        }

        public int GetTarget()
        {
            return target;
        }

        public int GetCycle()
        {
            return cycle;
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
