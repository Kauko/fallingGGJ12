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

    enum GameState { title, playing, gameover }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FrameImageManager frameImageManager;
        TurnString turnString;

        LevelLibrary.Level level;

        GameState state;
        MouseState oldMouse;
        KeyboardState oldKeyboard;
        Grid grid;
        Camera2D camera;
        Vector2 cameraDirection = Vector2.Zero;
        //tee paremmaksi jos on aikaa
        bool starting;

        Player currentPlayer;

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






            
            starting = true;

            this.IsMouseVisible = true;
            state = GameState.playing;
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

            level = Content.Load<LevelLibrary.Level>("level2");

            TextureRefs.tileLevel0 = this.Content.Load<Texture2D>("1");
            TextureRefs.tileLevel1 = this.Content.Load<Texture2D>("2");
            TextureRefs.tileLevel2 = this.Content.Load<Texture2D>("3");
            TextureRefs.tileLevel3 = this.Content.Load<Texture2D>("4");
            TextureRefs.tileLevel4 = this.Content.Load<Texture2D>("5");

            TextureRefs.turnsTexture = this.Content.Load<Texture2D>("atlas_score");

            TextureRefs.jewel1 = this.Content.Load<Texture2D>("6");
            TextureRefs.jewel2 = this.Content.Load<Texture2D>("7");
            TextureRefs.jewel3 = this.Content.Load<Texture2D>("8");
            TextureRefs.jewel4 = this.Content.Load<Texture2D>("9");
            TextureRefs.jewel5 = this.Content.Load<Texture2D>("6");
            TextureRefs.jewel6 = this.Content.Load<Texture2D>("7");
            TextureRefs.jewel7 = this.Content.Load<Texture2D>("8");

            TextureRefs.player = this.Content.Load<Texture2D>("hahmo");

            TextureRefs.background = this.Content.Load<Texture2D>("BG01");
            TextureRefs.frame = this.Content.Load<Texture2D>("BG02");

            TextureRefs.frameImage1 = this.Content.Load<Texture2D>("Otus01");
            TextureRefs.frameImage11 = this.Content.Load<Texture2D>("Otus02");

            
            //Need 7 of these
            frameImageManager.addFrameImage(TextureRefs.frameImage11, TextureRefs.frameImage1, new Vector2(15, 15));
            frameImageManager.addFrameImage(TextureRefs.frameImage11, TextureRefs.frameImage1, new Vector2(30, 30));
            frameImageManager.addFrameImage(TextureRefs.frameImage11, TextureRefs.frameImage1, new Vector2(1000, 1000));
            frameImageManager.addFrameImage(TextureRefs.frameImage11, TextureRefs.frameImage1, new Vector2(700, 700));
            frameImageManager.addFrameImage(TextureRefs.frameImage11, TextureRefs.frameImage1, new Vector2(200, 200));
            frameImageManager.addFrameImage(TextureRefs.frameImage11, TextureRefs.frameImage1, new Vector2(350, 350));
            frameImageManager.addFrameImage(TextureRefs.frameImage11, TextureRefs.frameImage1, new Vector2(100, 100));

            
            
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

            if (starting)
            {
                grid.LoadLevel(this.level);
                starting = false;
            }


            switch (state)
            {
                case GameState.title:
                    break;

                case GameState.playing:
                    PlayingGame(gameTime, mouse, keyboard);
                    break;

                case GameState.gameover:
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
                    //spriteBatch.Draw(null, new Vector2(0.0f, 0.0f), Color.White);
                    break;

                case GameState.playing:
                    spriteBatch.Draw(TextureRefs.background, new Vector2(0.0f, 0.0f), Color.White);

                    grid.Draw(spriteBatch);
                    

                    spriteBatch.Draw(TextureRefs.frame, new Vector2(0.0f, 0.0f), Color.White);
                    frameImageManager.Draw(spriteBatch);
                    turnString.Draw(spriteBatch);
                    break;

                case GameState.gameover:
                    //spriteBatch.Draw(null, new Vector2(0.0f, 0.0f), Color.White);
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

            camera.Translate(cameraDirection, gametime);

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
                
            }
        }

    }
}
