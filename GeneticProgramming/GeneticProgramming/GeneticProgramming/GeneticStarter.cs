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
    public class GeneticStarter : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;
        Map m_Map;
        IA m_IA;

        private const float IA_SPEED = 1f; 

        public GeneticStarter()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            m_Graphics.PreferredBackBufferWidth = 1000;
            m_Graphics.PreferredBackBufferHeight = 1000;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.Load(Content);
            m_Map = new Map("Map_1_Easy");
            
            m_IA = new IA(new Vector2());
            m_IA.m_MovementSpeed = IA_SPEED;

            //Example.GenerateSimpleGPExample();
        }

		protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            m_IA.Update();

            Console.Read();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_SpriteBatch.Begin();

            m_Map.Draw(m_SpriteBatch);
            m_IA.Draw(m_SpriteBatch);

            m_SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
