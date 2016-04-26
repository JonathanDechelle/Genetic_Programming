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
        OutlineMap m_Map;
        AIOutline m_IA;

        List<Chromosome> m_BestChromosomes;
        private int m_CurrentIndexChromosome = 0;
        private int m_LastIndexChromosome = -1;

        private const float IA_SPEED = 7f; 

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
            m_Map = new OutlineMap("Map_1_Easy");

            m_IA = new AIOutline(m_Map.GetIAStartPosition());
            m_IA.m_MovementSpeed = IA_SPEED;

            Example.GenerateSimpleGPExample(m_Map);
            m_BestChromosomes = Example.m_BestChromosomesInGenerations;
        }

		protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (m_CurrentIndexChromosome >= m_BestChromosomes.Count)
            {
                return;
            }

            if (m_CurrentIndexChromosome != m_LastIndexChromosome)
            {
                m_Map.ResetAllColor();
                m_IA.ResetBasePosition();
                m_IA.m_Movements = m_BestChromosomes[m_CurrentIndexChromosome].GetGenes();
            }

            m_IA.Update(m_Map);
            m_LastIndexChromosome = m_CurrentIndexChromosome;

            if (m_IA.m_HasFinish)
            {
                m_CurrentIndexChromosome++;
            }
            Console.Read();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_SpriteBatch.Begin();

            m_Map.Draw(m_SpriteBatch);
            m_IA.Draw(m_SpriteBatch);
            m_SpriteBatch.DrawString(Ressources.m_Font, "CHROMOSOME #" + (m_CurrentIndexChromosome + 1), new Vector2(500, 50), Color.White);

            m_SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
