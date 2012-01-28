using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falling
{
    class Player
    {
        int row, col;
        int turnsLeft;

        public Player() 
        {
            this.row = C.startRow;
            this.col = C.startCol;
            this.turnsLeft = C.playerTurns;
        }

        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }

        public void setRow(int r)
        {
            this.row = r;
        }

        public void setCol(int c) 
        {
            this.col = c;
        }

        public void setPosition(int r, int c) 
        {
            setCol(c);
            setRow(r);
        }

        public void decrementTurnsLeft() 
        {
            turnsLeft--;
        }

        public int getTurnsLeft() 
        {
            return turnsLeft;
        }
    }
}
