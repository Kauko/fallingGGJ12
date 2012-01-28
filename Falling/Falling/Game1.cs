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

        GameState state;
        MouseState oldMouse;
        Grid grid;

        Player currentPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            int gridSize = C.screenWidth - C.xMarginLeft - C.xMarginRight;
            grid = new Grid(C.xMarginLeft, C.yMargin, gridSize / C.gridCols);

            this.IsMouseVisible = true;
            state = GameState.title;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState mouse = Mouse.GetState();


            switch (state)
            {
                case GameState.title:
                    break;

                case GameState.playing:
                    PlayingGame(gameTime, mouse);
                    break;

                case GameState.gameover:
                    break;
            }
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
                    spriteBatch.Draw(TextureRefs.title, new Vector2(0.0f, 0.0f), Color.White);
                    break;

                case GameState.playing:
                    spriteBatch.Draw(TextureRefs.background, new Vector2(0.0f, 0.0f), Color.White);

                    grid.Draw(spriteBatch);


                    break;

                case GameState.gameover:
                    spriteBatch.Draw(TextureRefs.highscore, new Vector2(0.0f, 0.0f), Color.White);
                    break;
            }

            base.Draw(gameTime);
        }

        protected void PlayingGame(GameTime gametime, MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                if (grid.mouseClicked(mouse.X, mouse.Y))
                {
                    currentPlayer.decrementTurnsLeft();
                    if (currentPlayer.getTurnsLeft() <= 0)
                    {
                        grid.addDeadPlayer(currentPlayer);
                        currentPlayer = new Player();
                    }
                }
            }
        }

    }
}
