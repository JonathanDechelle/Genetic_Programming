using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GeneticProgramming
{
    /// <summary>
    /// Manage all the textures sound and other stuffs
    /// </summary>
    class Ressources
    {
        public static Texture2D Base_Texture, Outline_Texture, Gazon_Texture, Rock_Texture;
        public static void Load(ContentManager Content)
        {
            Base_Texture = Content.Load<Texture2D>("Object_Texture");
            Outline_Texture = Content.Load<Texture2D>("Object_Texture_outline");
            Gazon_Texture = Content.Load<Texture2D>("Gazon_Texture");
            Rock_Texture = Content.Load<Texture2D>("Rock_Texture");
        }
    }
}
