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
            population.GeneratePopulation();
            population.ComputeAdaptation();

            population.ToString();
            Chromosome bestByRoulette = GeneticOperator.ReproductionByRoulette(
                population.m_Chromosomes,
                population.GetCurrentPopulationAdaptation(), 
                population.m_AdaptationSum);

            Chromosome bestByRank = GeneticOperator.ReproductionByRank(population);
        }
    }
}
