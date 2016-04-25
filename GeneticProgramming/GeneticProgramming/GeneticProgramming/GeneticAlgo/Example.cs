﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Example
    {
        public static List<Chromosome> m_BestChromosomesInGenerations = new List<Chromosome>();

        private static Chromosome m_BestChromosome;

        public static void GenerateSimpleGPExample(Map aMap)
        {
            const int CHROMOSOMES_PER_GENERATION = 100;
            const int PARAMATERS_PER_CHROMOSOMES = 4;
            const int NUMBERS_PAIR_FOR_REPRODUCTION = 1;
            const int MAX_ADDITIONAL_TRY = 5;
            const double BEST_CHROMOSOME_BY_X_GENERATION = 25;
            int m_MaximumFitness = aMap.GetMaximumFitness();
            int m_MaxTry = m_MaximumFitness + MAX_ADDITIONAL_TRY;
            double currentPopulationMaxAdaptation;

            GeneticOperator.m_MutationPercent = 0.03f;
            GeneticOperator.m_CrossOverPercent = 0.15f;
            GeneticOperator.m_SelectionForReproductionPercent = 1.00f;

            PopulationContour population = new PopulationContour(CHROMOSOMES_PER_GENERATION, aMap);
            population.GenerateAdditionalPopulation(m_MaxTry, PARAMATERS_PER_CHROMOSOMES);
            population.ComputeAdaptation();
            currentPopulationMaxAdaptation = population.GetMaxAdaptation();

            int nbGenerations = 0;
            while (currentPopulationMaxAdaptation < m_MaximumFitness) 
            {
                population.ToString();

                //Reproduction
                PopulationContour newPopulation = new PopulationContour(CHROMOSOMES_PER_GENERATION, aMap);
                
                #region elitisme
                Chromosome[] parents = GeneticOperator.GetElites(population, GeneticOperator.m_CrossOverPercent);
                newPopulation.AddChromosomes(parents);

                Chromosome[] childrens = GeneticOperator.CrossOver1Point(parents); // parents are 100% crossOver 
                newPopulation.AddChromosomes(childrens);
                newPopulation.ComputeAdaptation();
                #endregion

                #region Tournament
                /*bool hasChromosomesInPopulation = true;
                while (hasChromosomesInPopulation)
                {
                    Chromosome[] chromosomesWinner = GeneticOperator.ReproductionByTournamenent(population, NUMBERS_PAIR_FOR_REPRODUCTION);
                    if (chromosomesWinner == null)
                    {
                        hasChromosomesInPopulation = false;

                        for (int i = 0; i < population.GetCount(); i++)
                        {
                            newPopulation.AddChromosomes(population.GetChromosomes());
                        }
                        continue;
                    }

                    if (chromosomesWinner.Length > 0)
                    {
                        newPopulation.AddChromosomes(chromosomesWinner);
                    }
                }
                */
                #endregion

                newPopulation.GenerateAdditionalPopulation(m_MaxTry, PARAMATERS_PER_CHROMOSOMES);
                GeneticOperator.Mutate(newPopulation);

                population.SetChromosomes(newPopulation.GetChromosomes()); 
                population.ComputeAdaptation();

                m_BestChromosome = population.GetBestChromosome();
                currentPopulationMaxAdaptation = m_BestChromosome.m_Adaptation;

                nbGenerations++;
                if (nbGenerations % BEST_CHROMOSOME_BY_X_GENERATION == 0)
                {
                    double bestAdaptation = GetBestAdaptation();
                    if(bestAdaptation < m_BestChromosome.m_Adaptation)
                    {
                        m_BestChromosomesInGenerations.Add(m_BestChromosome.Clone());
                    }
                }
            }

            if (!m_BestChromosomesInGenerations.Contains(m_BestChromosome))
            {
                m_BestChromosomesInGenerations.Add(m_BestChromosome);
            }

            Console.WriteLine("Nb Generation = " + nbGenerations); 
        }

        private static double GetBestAdaptation()
        {
            double max = double.MinValue;
            int count = m_BestChromosomesInGenerations.Count;
            for (int i = 0; i < count; i++)
            {
                double adaptation = m_BestChromosomesInGenerations[i].m_Adaptation;
                if (adaptation > max)
                {
                    max = adaptation;
                }
            }

            return max;
        }

        /*
         * Algorithme génétique générique
1-	Générer une population de individus de taille N : x1, x2, x3,…, xN.
2-	Calculer les chances de survie (qualité ou encore fitness) de chaque individu. f(x1), f(x2), f(x3), …, f(xN).
3-	Vérifier si le critère de terminaison est atteint. Si oui, terminer.
4-	Choisir une paire d’individus pour la reproduction (selon les chances de survie de chaque individu).
5-	Selon les probabilités associées à chaque opérateur génétique, appliquer ces opérateurs.
6-	Placer les individus produits dans la nouvelle population.
7-	Vérifier si la taille de la nouvelle population est correcte.  Si non, retourner à l’étape 4.
8-	Remplacer l’ancienne population d’individus par la nouvelle.
9-	Retourner à l’étape 2.
         */
    }
}
