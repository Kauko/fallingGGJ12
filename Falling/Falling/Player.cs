using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falling
{
    class Player
    {
        int x, y;

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y) 
        {
            this.y = y;
        }

        public void setPosition(int x, int y) 
        {
            setY(y);
            setX(x);
        }
    }
}
