using System;

namespace GeneticProgramming
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (GeneticStarter game = new GeneticStarter())
            {
                game.Run();
            }
        }
    }
#endif
}

