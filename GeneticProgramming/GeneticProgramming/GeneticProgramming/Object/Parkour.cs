using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeneticProgramming
{
    /// <summary>
    /// This object represent the surface where the player is allow to pass
    /// </summary>
    public class Parkour:Floor
    {
        public Parkour(Vector2 aPosition)
            : base(aPosition)
        {
           /* used only for fitness */
        }
    }
}
