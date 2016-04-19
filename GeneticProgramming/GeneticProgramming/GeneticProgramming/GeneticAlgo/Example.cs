using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Example
    {
        public static void GenerateSimpleGPExample()
        {
            Population population = new Population();
            population.GeneratePopulation(10, 4);
            population.ComputeAdaptation();

            population.ToString();
            Chromosome bestByRoulette = GeneticOperator.ReproductionByRoulette(population);

            Chromosome bestByRank = GeneticOperator.ReproductionByRank(population);


            //Chromosome[] currentChromosomes = population.GetChromosomes();
            //int populationHalfCount = population.GetCount() / 2;
            //currentChromosomes = GeneticOperator.ReproductionByTournamenent(population, populationHalfCount);
            //population.SetChromosomes(currentChromosomes);

            population.ToString();

            //Evolution
            GeneticOperator.CrossOver1Point(population, 0.5f);
        }
    }
}
