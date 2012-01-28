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

        GameState state;
        MouseState oldMouse;
        Grid grid;
        Camera2D camera;
        Vector2 cameraDirection = Vector2.Zero;

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

            base.Initialize();

            camera = new Camera2D(spriteBatch);
            grid = new Grid(C.xMarginLeft, C.yMargin, C.tileWidth, C.tileHeight, camera);
            frameImageManager = new FrameImageManager();

            this.IsMouseVisible = true;
            state = GameState.playing;
            currentPlayer = new Player();
            grid.setCurrentPlayer(currentPlayer);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureRefs.tileLevel0 = this.Content.Load<Texture2D>("1");
            TextureRefs.tileLevel1 = this.Content.Load<Texture2D>("2");
            TextureRefs.tileLevel2 = this.Content.Load<Texture2D>("3");
            TextureRefs.tileLevel3 = this.Content.Load<Texture2D>("4");
            TextureRefs.tileLevel4 = this.Content.Load<Texture2D>("5");

            TextureRefs.player = this.Content.Load<Texture2D>("hahmo");
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
                    break;

                case GameState.playing:
                    PlayingGame(gameTime, mouse, keyboard);
                    break;

                case GameState.gameover:
                    break;
            }
            oldMouse = mouse;
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
                    spriteBatch.Draw(TextureRefs.frameBackground, new Vector2(0.0f, 0.0f), Color.White);
                    frameImageManager.Draw(spriteBatch);


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
                if (grid.mouseClicked(mouse.X, mouse.Y))
                {
                    currentPlayer.decrementTurnsLeft();
                    if (currentPlayer.getTurnsLeft() <= 0)
                    {
                        grid.addDeadPlayer(currentPlayer);
                        currentPlayer = new Player();
                        grid.setCurrentPlayer(currentPlayer);
                        grid.decreaseTileLevels();
                    }
                }
            }
            else if (keyboard.IsKeyDown(Keys.Space))
            {
                if (currentPlayer.getTurnsLeft() >= 5)
                {
                    grid.addDeadPlayer(currentPlayer);
                    currentPlayer = new Player();
                    grid.setCurrentPlayer(currentPlayer);
                    grid.decreaseTileLevels();
                    currentPlayer.addTurnsLeft(5);
                }
                else 
                {
                    grid.addDeadPlayer(currentPlayer);
                    currentPlayer = new Player();
                    grid.setCurrentPlayer(currentPlayer);
                    grid.decreaseTileLevels();
                }
            }
        }

    }
}
