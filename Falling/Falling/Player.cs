using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Falling
{
    class Player
    {
        int row, col;
        int turnsLeft;

        private Vector2 worldPosition;

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        bool thrower;
        List<Player> partner;

        public Player() 
        {
            this.row = C.startRow;
            this.col = C.startCol;
            this.turnsLeft = C.playerTurns;
            thrower = false;
            partner = new List<Player>();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) 
        {
            spriteBatch.Draw(TextureRefs.jewel7, position, Color.White);  
        }

        public void addTurnsLeft(int i) 
        {
            this.turnsLeft = turnsLeft + i;
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

        public void setPosition(int c, int r) 
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

        public bool isThrower() 
        {
            return thrower;
        }

        public List<Player> getPartners() 
        {
            return partner;
        }

        public void setThrower(bool b) 
        {
            this.thrower = b;
        }

        public void addPartner(Player p) 
        {
            partner.Add(p);
 
        }

        public void killPlayer() 
        {
            this.turnsLeft = 0;
        }
    }
}
