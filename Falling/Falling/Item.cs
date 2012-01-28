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

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

    }
}
