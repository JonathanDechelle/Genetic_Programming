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

namespace GeneticProgramming
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map m_Map;
        Enemy[,] m_Enemy_Position;
        Player m_Player;
        List<Light> ListLight; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            Content.RootDirectory = "Content";
            Run();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.Load(Content);
            m_Map = new Map("Map_1_Easy");
            m_Map.Generate_Enemy(ref m_Enemy_Position);
            m_Map.Generate_Player(ref m_Player);
            ListLight = new List<Light>();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
           
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                m_Player.ExitGame)
                this.Exit();

            foreach (Enemy Enemy in m_Enemy_Position)
                if (Enemy != null) Enemy.Update(m_Map.Grid);

            KeyboardHelper.PlayerState = Keyboard.GetState();
            m_Map.Update(m_Enemy_Position);
            m_Player.Update(m_Map, m_Enemy_Position);
            Light_Management.Update(m_Map, m_Enemy_Position);
            KeyboardHelper.PlayerStateLast = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BasicFloor.FloorColor);
            spriteBatch.Begin();
            m_Map.Draw(spriteBatch);

            m_Player.Draw(spriteBatch);
            foreach (Enemy Enemy in m_Enemy_Position)
                if (Enemy != null) Enemy.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
