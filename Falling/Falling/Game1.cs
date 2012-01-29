using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Falling
{

    enum GameState { title, playing, gameover, victory}

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FrameImageManager frameImageManager;
        TurnString turnString;

        int gameTurns = C.gameTurns;

        List<LevelLibrary.Level> levels = new List<LevelLibrary.Level>();

        GameState state;
        MouseState oldMouse;
        KeyboardState oldKeyboard;
        Grid grid;
        Camera2D camera;
        Vector2 cameraDirection = Vector2.Zero;

        int currentLevel;

        Player currentPlayer;
        Texture2D currentBackground;
        Texture2D currentFrame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            this.Window.Title = "Falling World - GGJ12 - Finland - Oulu - Stage gamedev";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.IsMouseVisible = true;
            state = GameState.title;
            currentPlayer = new Player();
            
            frameImageManager = new FrameImageManager();

            base.Initialize();
            camera = new Camera2D(spriteBatch);
            grid = new Grid(C.xMarginLeft, C.yMargin, C.tileWidth, C.tileHeight, camera);
            grid.setCurrentPlayer(currentPlayer);
            grid.initJewels(frameImageManager);
            turnString = new TurnString(TextureRefs.turnsTexture, 0, C.turnsX, C.turnsY);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            levels.Add(Content.Load<LevelLibrary.Level>("level2"));
            levels.Add(Content.Load<LevelLibrary.Level>("level3"));
            levels.Add(Content.Load<LevelLibrary.Level>("level4"));

            TextureRefs.tileLevel0 = this.Content.Load<Texture2D>("Textures/Tiles/1");
            TextureRefs.tileLevel1 = this.Content.Load<Texture2D>("Textures/Tiles/laatta4");
            TextureRefs.tileLevel2 = this.Content.Load<Texture2D>("Textures/Tiles/laatta3");
            TextureRefs.tileLevel3 = this.Content.Load<Texture2D>("Textures/Tiles/laatta2");
            TextureRefs.tileLevel4 = this.Content.Load<Texture2D>("Textures/Tiles/laatta1");
            TextureRefs.tileLevel5 = this.Content.Load<Texture2D>("Textures/Tiles/laatta1");

            TextureRefs.turnsTexture = this.Content.Load<Texture2D>("Textures/atlas_score");

            TextureRefs.jewel1 = this.Content.Load<Texture2D>("Textures/Jewels/jkeltainen");
            TextureRefs.jewel2 = this.Content.Load<Texture2D>("Textures/Jewels/joranssi");
            TextureRefs.jewel3 = this.Content.Load<Texture2D>("Textures/Jewels/jpunainen");
            TextureRefs.jewel4 = this.Content.Load<Texture2D>("Textures/Jewels/jturkoosi");
            TextureRefs.jewel5 = this.Content.Load<Texture2D>("Textures/Jewels/jvihrea");
            TextureRefs.jewel6 = this.Content.Load<Texture2D>("Textures/Jewels/jvioletti");
            TextureRefs.jewel7 = this.Content.Load<Texture2D>("Textures/Jewels/jpinkki");

            TextureRefs.player = this.Content.Load<Texture2D>("Textures/hahmo");
            TextureRefs.playerWings = this.Content.Load<Texture2D>("Textures/hahmo");
            TextureRefs.playerBridge = this.Content.Load<Texture2D>("Textures/silta");

            TextureRefs.bird = this.Content.Load<Texture2D>("7");

            TextureRefs.background = this.Content.Load<Texture2D>("Textures/bg1");
            TextureRefs.frame = this.Content.Load<Texture2D>("Textures/frame1");

            TextureRefs.backgroundGood = this.Content.Load<Texture2D>("Textures/bg2");
            TextureRefs.frameGood = this.Content.Load<Texture2D>("Textures/frame2");

            TextureRefs.introPage = this.Content.Load<Texture2D>("Textures/bg1");
            TextureRefs.gameOver = this.Content.Load<Texture2D>("Textures/bg1");
            TextureRefs.victoryScreen = this.Content.Load<Texture2D>("Textures/bg2");

            TextureRefs.turnTextBackground = this.Content.Load<Texture2D>("textBG");
            TextureRefs.selector = this.Content.Load<Texture2D>("selector");

            TextureRefs.frameImage1 = this.Content.Load<Texture2D>("Textures/Animals/kettu1");
            TextureRefs.frameImage11 = this.Content.Load<Texture2D>("Textures/Animals/kettu2");

            TextureRefs.frameImage2 = this.Content.Load<Texture2D>("Textures/Animals/kukka1");
            TextureRefs.frameImage22 = this.Content.Load<Texture2D>("Textures/Animals/kukka2");

            TextureRefs.frameImage3 = this.Content.Load<Texture2D>("Textures/Animals/lintu1");
            TextureRefs.frameImage33 = this.Content.Load<Texture2D>("Textures/Animals/lintu2");

            TextureRefs.frameImage4 = this.Content.Load<Texture2D>("Textures/Animals/pilvi1a");
            TextureRefs.frameImage44 = this.Content.Load<Texture2D>("Textures/Animals/pilvi1b");

            TextureRefs.frameImage5 = this.Content.Load<Texture2D>("Textures/Animals/aurinko1");
            TextureRefs.frameImage55 = this.Content.Load<Texture2D>("Textures/Animals/aurinko2");

            TextureRefs.frameImage6 = this.Content.Load<Texture2D>("Textures/Animals/pupu1");
            TextureRefs.frameImage66 = this.Content.Load<Texture2D>("Textures/Animals/pupu2");

            TextureRefs.frameImage7 = this.Content.Load<Texture2D>("Textures/Animals/puu1");
            TextureRefs.frameImage77 = this.Content.Load<Texture2D>("Textures/Animals/puu2");

            
            //Need 7 of these
            frameImageManager.addFrameImage(TextureRefs.frameImage5, TextureRefs.frameImage55, new Vector2(1000, 30)); //aurinko
            frameImageManager.addFrameImage(TextureRefs.frameImage4, TextureRefs.frameImage44, new Vector2(1000, 10)); //pilvi
            frameImageManager.addFrameImage(TextureRefs.frameImage7, TextureRefs.frameImage77, new Vector2(1000, 200)); //puu
            frameImageManager.addFrameImage(TextureRefs.frameImage3, TextureRefs.frameImage33, new Vector2(100, 50)); //lintu
            frameImageManager.addFrameImage(TextureRefs.frameImage1, TextureRefs.frameImage11, new Vector2(30, 550)); //Kettu
            frameImageManager.addFrameImage(TextureRefs.frameImage6, TextureRefs.frameImage66, new Vector2(1000, 500)); // pupu
            frameImageManager.addFrameImage(TextureRefs.frameImage2, TextureRefs.frameImage22, new Vector2(200,600)); //kukka
            
            
            
            
            
             
            
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            switch (state)
            {
                case GameState.title:
                    titleScreen(mouse);
                    break;

                case GameState.playing:

                    PlayingGame(gameTime, mouse, keyboard);
                    break;

                case GameState.gameover:
                    gameOver(mouse);
                    break;

                case GameState.victory:
                    victory(mouse);
                    break;

            }
            oldMouse = mouse;
            oldKeyboard = keyboard;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (state)
            {
                case GameState.title:
                    spriteBatch.Draw(TextureRefs.introPage, new Vector2(0.0f, 0.0f), Color.White);
                    break;

                case GameState.playing:
                    spriteBatch.Draw(currentBackground, new Vector2(0.0f, 0.0f), Color.White);

                    grid.Draw(spriteBatch);
                    

                    spriteBatch.Draw(currentFrame, new Vector2(0.0f, 0.0f), Color.White);
                    frameImageManager.Draw(spriteBatch);

                    spriteBatch.Draw(TextureRefs.turnTextBackground, new Vector2(100, 50), Color.White);
                    turnString.Draw(spriteBatch);

                    break;

                case GameState.gameover:
                    spriteBatch.Draw(TextureRefs.gameOver, new Vector2(0.0f, 0.0f), Color.White);
                    break;

                case GameState.victory:
                    spriteBatch.Draw(TextureRefs.victoryScreen, new Vector2(0.0f, 0.0f), Color.White);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void PlayingGame(GameTime gametime, MouseState mouse, KeyboardState keyboard)
        {
            turnString.SetValue(currentPlayer.getTurnsLeft());
            cameraDirection = Vector2.Zero;

            if (keyboard.IsKeyDown(Keys.Left) == true)
            {
                cameraDirection.X = -1;
            }
            else if (keyboard.IsKeyDown(Keys.Right) == true)
            {
                cameraDirection.X = 1;
            }

            if (keyboard.IsKeyDown(Keys.Up) == true)
            {
                cameraDirection.Y = -1;
            }
            else if (keyboard.IsKeyDown(Keys.Down) == true)
            {
                cameraDirection.Y = 1;
            }

            camera.Translate(cameraDirection, gametime, grid.getLevelRows(), grid.getLevelCols());

            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                //if is a valid tile
                if (grid.mouseClicked(camera.TransformMouse(new Vector2( mouse.X, mouse.Y))))
                {
                    if (currentPlayer.isBridge())
                    {
                        currentPlayer = new Player();
                        currentPlayer.Position = grid.getSpawnPoint();
                        currentPlayer.setPosition(grid.getSpawnRow(), grid.getSpawnCol());
                        camera.Position = grid.getCameraCenter();
                        grid.setCurrentPlayer(currentPlayer);
                        grid.decreaseTileLevels();
                        gameTurns--;
                        if (gameTurns <= 0)
                            state =  GameState.gameover;
                    }
                    else { 
                        currentPlayer.decrementTurnsLeft();
                        if (currentPlayer.getTurnsLeft() <= 0)
                        {
                            grid.addDeadPlayer(currentPlayer);
                            currentPlayer = new Player();
                            currentPlayer.Position = grid.getSpawnPoint();
                            currentPlayer.setPosition(grid.getSpawnRow(), grid.getSpawnCol());
                            camera.Position = grid.getCameraCenter();
                            grid.setCurrentPlayer(currentPlayer);
                            grid.decreaseTileLevels();
                            gameTurns--;
                            if (gameTurns <= 0)
                                state = GameState.gameover;
                        }
                    }
                }
            }
            else if (keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyUp(Keys.Space))
            {
                
                    grid.addDeadPlayer(currentPlayer);
                    int passedTurns = currentPlayer.getTurnsLeft();
                    currentPlayer = new Player();
                    currentPlayer.Position=grid.getSpawnPoint();
                    currentPlayer.setPosition(grid.getSpawnRow(), grid.getSpawnCol());
                    camera.Position = grid.getCameraCenter();
                    grid.setCurrentPlayer(currentPlayer);
                    grid.decreaseTileLevels();
                    currentPlayer.addTurnsLeft(passedTurns);
                    gameTurns--;
                    if (gameTurns <= 0)
                        state = GameState.gameover;
                
            }

            if (grid.isWin()) 
            {
                currentLevel++;
                if (currentLevel < levels.Count)
                {
                    gameTurns = C.gameTurns;
                    grid.unloadLevel();
                    grid.initJewels(frameImageManager);
                    grid.LoadLevel(levels[currentLevel]);
                    frameImageManager.resetFrameImages();
                    currentBackground = TextureRefs.background;
                    currentFrame = TextureRefs.frame;

                    
                }
                else
                {
                    state = GameState.victory;
                }

                
            }
            else if (grid.isCloseWin()) 
            {
                currentBackground = TextureRefs.backgroundGood;
                currentFrame = TextureRefs.frameGood;
            }
               
        }

        private void titleScreen(MouseState mouse)
        {
            currentLevel = 0;
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                grid.LoadLevel(levels[currentLevel]);
                state = GameState.playing;
                currentBackground = TextureRefs.background;
                currentFrame = TextureRefs.frame;
            }
        }

        private void gameOver(MouseState mouse) 
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                this.Exit();
            }
        }

        private void victory(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                this.Exit();
            }
        }

    }
}
