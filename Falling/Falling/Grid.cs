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

        Tile[,] tiles;
        Player currentPlayer;
        List<Player> deadPlayers = new List<Player>();
        List<Item> items = new List<Item>();
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
            items.Add(new Jewel(TextureRefs.jewel1, frameImageManager.getFrameImage(0)));
            items.Add(new Jewel(TextureRefs.jewel2, frameImageManager.getFrameImage(1)));
            items.Add(new Jewel(TextureRefs.jewel3, frameImageManager.getFrameImage(2)));
            items.Add(new Jewel(TextureRefs.jewel4, frameImageManager.getFrameImage(3)));
            items.Add(new Jewel(TextureRefs.jewel5, frameImageManager.getFrameImage(4)));
            items.Add(new Jewel(TextureRefs.jewel6, frameImageManager.getFrameImage(5)));
            items.Add(new Jewel(TextureRefs.jewel7, frameImageManager.getFrameImage(6)));
        }

        public void LoadLevel(LevelLibrary.Level level) 
        {
            rows = level.Rows;
            cols = level.Columns;
            tiles = new Tile[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int symbol = level.GetValue(r, c);
                    int jewelcount = 0;
                    switch (symbol) {
                        case 0:
                            tiles[r, c] = new Tile(0);
                            break;
                        case 1:
                            tiles[r, c] = new Tile(1);
                            break;
                        case 2:
                            tiles[r, c] = new Tile(2);
                            break;
                        case 3:
                            tiles[r, c] = new Tile(3);
                            break;
                        case 4:
                            tiles[r, c] = new Tile(4);
                            break;
                        case 5:
                            //Spawnpoint
                            spawn = new Vector2(r, c);
                            tiles[r, c] = new Tile(5);
                            break;
                        case 6:
                            //Jewel
                            items[jewelcount].Position = new Vector2(r, c);
                            tiles[r, c] = new Tile(6);
                            break;
                        case 7:
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
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Tile t = tiles[r, c];
                    if(!t.isBridge() && t.getLevel() > 0 )
                        t.decreaseLevel();
                }
            }
        }

        public bool mouseClicked(int x, int y)
        {
            //Kato mitä tileä hiirellä klikattiin
            int row = (int)Math.Floor((float)(x / tileWidth));
            int col = (int)Math.Floor((float)(y / tileHeight));

            int tempCol, tempRow;

            if (tileAdjacentToPlayer(row, col))
            {
                int previousCol = currentPlayer.getCol();
                int previousRow = currentPlayer.getRow();
                currentPlayer.setPosition(row, col);

                foreach (Jewel j in items) 
                {
                    if (currentPlayer.getRow() == j.getRow() && currentPlayer.getCol() == j.getCol()) 
                    {
                        j.setCollected(true);
                        currentPlayer.killPlayer();
                    }
                }
                
                /* Vuoro loppuu niin pitää tarkastaa vaikuttavatko vanhat pelaajat aktiiviseen pelaajaan. Oletuksena while (deadPLayers lista) käydään kerran läpi
                 * Jos löytyy jotain vaikutusta niin stop = false, jolloin while käydään uudestaan läpi
                 * 
                 */
                while (true)
                {
                    bool stop = true;
                    foreach (Player p in deadPlayers)
                    {
                        if (p.getRow() == currentPlayer.getRow() && p.getCol() == currentPlayer.getCol())
                        {
                            //Tällä kikalla saadaan pelaaja liikkumaan siihen suuntaan mihin se aikasemminki liikku ( toivottavasti )
                            tempCol = currentPlayer.getCol() - previousCol;
                            tempRow = currentPlayer.getRow() - previousRow;

                            previousCol = currentPlayer.getCol();
                            previousRow = currentPlayer.getRow();

                            currentPlayer.setPosition(currentPlayer.getRow() + tempRow,currentPlayer.getCol() + tempCol);
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
                                        currentPlayer.setPosition(currentPlayer.getRow() - 3,currentPlayer.getCol());
                                }
                            }

                            stop = false;

                        }
                }
                    if (stop)
                        break;
                }
                if (tiles[currentPlayer.getRow(), currentPlayer.getCol()].getLevel() <= 0) 
                {
                    tiles[currentPlayer.getRow(), currentPlayer.getCol()].setBridge(true);
                    currentPlayer.killPlayer();
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

            foreach (Player d in deadPlayers) 
            {
                camera.DrawPlayer(d);
            }

            camera.DrawPlayer(currentPlayer);
            
        }

        public bool tileAdjacentToPlayer(int row, int col)
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
