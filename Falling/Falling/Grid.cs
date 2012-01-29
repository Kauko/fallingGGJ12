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
        int x, y, tileWidth, tileHeight;
        int rows, cols;
        int spawnRow, spawnCol;

        bool win;
        int collectedCount;

        Tile[,] tiles;
        Player currentPlayer;
        List<Player> deadPlayers = new List<Player>();
        List<Jewel> jewels = new List<Jewel>();
        List<Bird> birds = new List<Bird>();

        Camera2D camera;
        Vector2 spawn;

        public Grid(int gridX, int gridY, int w, int h, Camera2D c)
        {
            x = gridX;
            y = gridY;
            tileWidth = w;
            tileHeight = h;
            this.camera = c;

            
        }

        public void initJewels(FrameImageManager frameImageManager) 
        {
            jewels.Add(new Jewel(TextureRefs.jewel1, frameImageManager.getFrameImage(0)));
            jewels.Add(new Jewel(TextureRefs.jewel2, frameImageManager.getFrameImage(4)));
            jewels.Add(new Jewel(TextureRefs.jewel3, frameImageManager.getFrameImage(6)));
            jewels.Add(new Jewel(TextureRefs.jewel4, frameImageManager.getFrameImage(1)));
            jewels.Add(new Jewel(TextureRefs.jewel5, frameImageManager.getFrameImage(2)));
            jewels.Add(new Jewel(TextureRefs.jewel6, frameImageManager.getFrameImage(3)));
            jewels.Add(new Jewel(TextureRefs.jewel7, frameImageManager.getFrameImage(5)));
        }

        public void unloadLevel() 
        {
            win = false;
            deadPlayers.Clear();
            jewels.Clear();
            collectedCount = 0;
            

        }

        public void LoadLevel(LevelLibrary.Level level) 
        {
            collectedCount = 0;
            rows = level.Rows;
            cols = level.Columns;
            tiles = new Tile[rows, cols];
            int jewelcount = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int symbol = level.GetValue(r, c);
                    
                    switch (symbol) {
                        case 0:
                            tiles[r, c] = new Tile(0);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            break;
                        case 1:
                            tiles[r, c] = new Tile(1);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            break;
                        case 2:
                            tiles[r, c] = new Tile(2);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            break;
                        case 3:
                            tiles[r, c] = new Tile(3);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            break;
                        case 4:
                            tiles[r, c] = new Tile(4);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            break;
                        case 5:
                            //Spawnpoint
                            spawn = new Vector2(r, c);
                            tiles[r, c] = new Tile(7);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            spawnRow = r;
                            spawnCol = c;
                            spawn = new Vector2(r * tileHeight, c * tileWidth);
                            camera.Position=getCameraCenter();
                            currentPlayer.Position=spawn;
                            currentPlayer.setPosition(r,c);
                            break;
                        case 6:
                            //Jewel
                            jewels[jewelcount].Position = new Vector2(r * tileHeight, c * tileWidth);
                            jewels[jewelcount].setCol(c);
                            jewels[jewelcount].setRow(r);
                            tiles[r, c] = new Tile(7);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            jewelcount++;
                            break;
                        case 7:
                            //Bird
                            birds.Add(new Bird(TextureRefs.bird,new Vector2(r * tileHeight, c * tileWidth),c,r));
                            tiles[r, c] = new Tile(7);
                            tiles[r, c].Position = new Vector2(r * tileHeight, c * tileWidth);
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    
                    }
                    /*char cType = symbol[0];
                    if (cType != '-')
                    {
                        LoadCritter(cType, Convert.ToInt32(symbol[1].ToString()), r, c);
                    }*/
                }
            }
        }

        public void decreaseTileLevels() 
        {
            List<int> removablePlayers = new List<int>();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Tile t = tiles[r, c];
                    if(!t.isBridge() && t.getLevel() > 0 )
                        t.decreaseLevel();

                    if (t.getLevel() == 0) 
                    {
                        foreach (Player p in deadPlayers) 
                        {
                            if (p.getCol() == c && p.getRow() == r) 
                            {
                                t.setBridge(true);
                                p.becomeBridge();
                                removablePlayers.Add(deadPlayers.IndexOf(p));
                            }

                        }

                        


                    }
                }
            }

            foreach (int i in removablePlayers)
            {
                deadPlayers.RemoveAt(i);
            }


        }

        public Vector2 getSpawnPoint() 
        {
            return spawn;
        }

        public Vector2 getCameraCenter() 
        {
            Vector2 ret = spawn - new Vector2(640, 360);
            return ret;
        }

        public bool mouseClicked(Vector2 pos)
        {
            //Kato mitä tileä hiirellä klikattiin
            int row = (int)Math.Floor((float)(pos.X / tileHeight));
            int col = (int)Math.Floor((float)(pos.Y / tileWidth));

            

            int tempCol, tempRow;

            if (tileAdjacentToPlayer(row, col))
            {
                int previousCol = currentPlayer.getCol();
                int previousRow = currentPlayer.getRow();
                currentPlayer.setPosition(row, col);
                win = true;
                

                
                
                /* Vuoro loppuu niin pitää tarkastaa vaikuttavatko vanhat pelaajat aktiiviseen pelaajaan. Oletuksena while (deadPLayers lista) käydään kerran läpi
                 * Jos löytyy jotain vaikutusta niin stop = false, jolloin while käydään uudestaan läpi
                 * 
                 */
                while (true)
                {
                    bool stop = true;

                    foreach (Jewel j in jewels)
                    {
                        if (currentPlayer.getRow() == j.getRow() && currentPlayer.getCol() == j.getCol())
                        {
                            j.setCollected(true);
                            collectedCount++;
                            currentPlayer.killPlayer();
                        }
                    }

                    foreach (Bird b in birds) 
                    {
                        if (currentPlayer.getRow() == b.getRow() && currentPlayer.getCol() == b.getCol() && !b.isCollected())
                        {
                            b.setCollected(true);
                            currentPlayer.setFlying(true);
                        }
                    }

                    foreach (Player p in deadPlayers)
                    {
                        if (p.getRow() == currentPlayer.getRow() && p.getCol() == currentPlayer.getCol())
                        {
                            //Tällä kikalla saadaan pelaaja liikkumaan siihen suuntaan mihin se aikasemminki liikku ( toivottavasti )
                            tempCol = currentPlayer.getCol() - previousCol;
                            tempRow = currentPlayer.getRow() - previousRow;

                            previousCol = currentPlayer.getCol();
                            previousRow = currentPlayer.getRow();

                            if (tempRow > 0)
                                currentPlayer.setPosition(currentPlayer.getRow() + 1, currentPlayer.getCol());
                            if (tempRow < 0)
                                currentPlayer.setPosition(currentPlayer.getRow() - 1, currentPlayer.getCol());
                            if (tempCol > 0)
                                currentPlayer.setPosition(currentPlayer.getRow(), currentPlayer.getCol() + 1);
                            if (tempCol < 0)
                                currentPlayer.setPosition(currentPlayer.getRow(), currentPlayer.getCol() - 1);
                            stop = false;

                        }

                        if (p.isThrower())
                        {
                            foreach (Player t in p.getPartners())
                            {
                                if (tileAdjacentToPlayer(p.getRow(),p.getCol()) && tileAdjacentToPlayer(t.getRow(),t.getCol()))
                                {
                                    //lasketaan taas suuntaa
                                    tempCol = currentPlayer.getCol() - previousCol;
                                    tempRow = currentPlayer.getRow() - previousRow;

                                    previousCol = currentPlayer.getCol();
                                    previousRow = currentPlayer.getRow();

                                    //Nakataan pelaajaa kolme eteenpäin taas
                                    if (tempRow > 0)
                                        currentPlayer.setPosition(currentPlayer.getRow() + 3,currentPlayer.getCol());
                                    if (tempRow < 0)
                                        currentPlayer.setPosition(currentPlayer.getRow() - 3,currentPlayer.getCol());
                                    if (tempCol > 0)
                                        currentPlayer.setPosition(currentPlayer.getRow(),currentPlayer.getCol() + 3);
                                    if (tempCol < 0)
                                        currentPlayer.setPosition(currentPlayer.getRow(),currentPlayer.getCol() - 3);

                                    stop = false;
                                }
                            }

                            

                        }
                }
                    if (stop)
                        break;
                }
                //jos on vielä keräämättämiä jeweleitä niin ei voida voittaa
                foreach (Jewel j in jewels)
                {
                    if (!j.isCollected())
                        win = false;
                }

                if (tiles[currentPlayer.getRow(), currentPlayer.getCol()].getLevel() <= 0 && !currentPlayer.isFlying())
                {
                    tiles[currentPlayer.getRow(), currentPlayer.getCol()].setBridge(true);
                    currentPlayer.becomeBridge();
                }
                else if (tiles[currentPlayer.getRow(), currentPlayer.getCol()].getLevel() > 0 && currentPlayer.isFlying())
                    if(currentPlayer.getFlyTurnsLeft() < 10)
                        currentPlayer.setFlying(false);

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
            foreach (Player t in deadPlayers) 
            { 
                if(p.getRow() - 2 == t.getRow() && p.getCol() == t.getCol())
                {
                    p.setThrower(true);
                    p.addPartner(t);
                    t.setThrower(true);
                    t.addPartner(p);
                }
                else if(p.getRow() + 2 == t.getRow() && p.getCol() == t.getCol())
                {
                    p.setThrower(true);
                    p.addPartner(t);
                    t.setThrower(true);
                    t.addPartner(p);
                }
                else if(p.getRow() == t.getRow() && p.getCol() - 2 == t.getCol())
                {
                    p.setThrower(true);
                    p.addPartner(t);
                    t.setThrower(true);
                    t.addPartner(p);
                }
                else if (p.getRow() == t.getRow() && p.getCol() + 2 == t.getCol()) 
                {
                    p.setThrower(true);
                    p.addPartner(t);
                    t.setThrower(true);
                    t.addPartner(p);
                }
            }

            deadPlayers.Add(p);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Tile t = tiles[r, c];
                    camera.DrawTile(t);
                }
            }

            foreach (Jewel j in jewels) 
            {
                if(!j.isCollected())
                    camera.DrawItem(j);
            }

            foreach (Bird b in birds) 
            {
                if(!b.isCollected())
                    camera.DrawItem(b);
            }

            foreach (Player d in deadPlayers) 
            {
                camera.DrawPlayer(d);
            }

            camera.DrawPlayer(currentPlayer);
            
            spriteBatch.Draw(TextureRefs.selector, camera.ApplyTransformations(tiles[currentPlayer.getRow() - 1, currentPlayer.getCol() - 1].Position), Color.White);
            
        }

        public bool tileAdjacentToPlayer(int row, int col)
        {
            int pcol = currentPlayer.getCol();
            int prow = currentPlayer.getRow();
            if (prow == row || pcol == col)
            {
                if (col == pcol && prow == row - 1 || prow == row + 1)
                    return true;
                else if (row == prow && pcol == col - 1 || pcol == col + 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public int getSpawnRow() 
        {
            return this.spawnRow;
        }

        public int getSpawnCol()
        {
            return this.spawnCol;
        }

        public bool isWin() 
        {
            return win;
        }

        public bool isCloseWin() 
        {
            if (win == false)
            {
                if (collectedCount > 4)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public int getLevelRows() 
        {
            return this.rows;
        }

        public int getLevelCols()
        {
            return this.cols;
        }
    }

}
