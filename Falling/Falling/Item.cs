using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Falling
{
    class Item
    {
        private Vector2 worldPosition;
        protected int row, col;

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        public void setRow(int r)
        {
            this.row = r;
        }

        public void setCol(int c)
        {
            this.col = c;

        }

    }
}
