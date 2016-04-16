using System;

namespace GeneticProgramming
{
#if WINDOWS || XBOX
    static class Program
    {
        private static bool GeneticProgramming = true;

        static void Main(string[] args)
        {
            Type typeUsed = GeneticProgramming ?
                typeof(GeneticStarter) :
                typeof(Game1);

            /*using (Game1 game = new Game1())
            {
                game.Run();
            }*/

            typeUsed.GetConstructor(new Type[]{}).Invoke(new object[]{});
        }
    }
#endif
}

