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

namespace WindowsGame10
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private static Game1 instance;
        LifeBoard lifeBoard;
        Texture2D mouseSprite;
        

        private List<GameEntity> entities = new List<GameEntity>();

        public List<GameEntity> Entities
        {
            get { return entities; }
        }

        public int Width
        {
            get
            {
                return GraphicsDevice.Viewport.Width;
            }
        }

        public int Height
        {
            get
            {
                return GraphicsDevice.Viewport.Height;
            }
        }

        public static Game1 Instance
        {
            get
            {
                return instance;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            instance = this;
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
            lifeBoard = new LifeBoard();
            entities.Add(lifeBoard);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mouseSprite = Content.Load<Texture2D>("cursor");
            
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].LoadContent();
            }

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

        bool wasPressed = false;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState state  = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if (state.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (state.IsKeyDown(Keys.Space) && !wasPressed)
            {
                lifeBoard.Paused = !lifeBoard.Paused;
                wasPressed = true;
            }

            if (state.IsKeyDown(Keys.F1) && !wasPressed)
            {
                lifeBoard.Clear();
                lifeBoard.Paused = true;
                wasPressed = true;
            }

            if (state.IsKeyDown(Keys.F2) && !wasPressed)
            {
                lifeBoard.Clear();
                lifeBoard.RandomBoard();
                lifeBoard.Paused = true;
                wasPressed = true;
            }

            if (state.IsKeyDown(Keys.F3) && !wasPressed)
            {
                Vector2 cellPos = lifeBoard.ScreenToCell(mouseState.X, mouseState.Y);
                lifeBoard.MakeGlider((int) cellPos.X, (int) cellPos.Y);

                wasPressed = true;
            }

            if (state.IsKeyDown(Keys.F4) && !wasPressed)
            {
                Vector2 cellPos = lifeBoard.ScreenToCell(mouseState.X, mouseState.Y);
                lifeBoard.MakeTumbler((int)cellPos.X, (int)cellPos.Y);

                wasPressed = true;
            }

            if (state.IsKeyDown(Keys.F5) && !wasPressed)
            {
                Vector2 cellPos = lifeBoard.ScreenToCell(mouseState.X, mouseState.Y);
                lifeBoard.MakeLightWeightSpaceShip((int)cellPos.X, (int)cellPos.Y);

                wasPressed = true;
            }

            if (state.IsKeyDown(Keys.F6) && !wasPressed)
            {
                Vector2 cellPos = lifeBoard.ScreenToCell(mouseState.X, mouseState.Y);
                lifeBoard.MakeGosperGun((int)cellPos.X, (int)cellPos.Y);

                wasPressed = true;
            }


            if (state.GetPressedKeys().Length == 0)
            {
                wasPressed = false;
            }

            
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 cellPos = lifeBoard.ScreenToCell(mouseState.X, mouseState.Y);
                lifeBoard.On((int) cellPos.X, (int) cellPos.Y);
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                Vector2 cellPos = lifeBoard.ScreenToCell(mouseState.X, mouseState.Y);
                lifeBoard.Off((int)cellPos.X, (int)cellPos.Y);
            }

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
                if (entities[i].Alive == false)
                {
                    entities.Remove(entities[i]);
                }
            }
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(gameTime);
            }

            MouseState mouseState = Mouse.GetState();
            spriteBatch.Draw(mouseSprite, new Vector2(mouseState.X, mouseState.Y), Color.White);    
            
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
