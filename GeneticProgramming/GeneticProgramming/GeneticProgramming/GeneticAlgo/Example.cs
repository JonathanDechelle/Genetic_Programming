﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Example
    {
        public static Chromosome bestChromosomeOfAll;
        public static void GenerateSimpleGPExample(Map aMap)
        {
            const int CHROMOSOMES_PER_GENERATION = 10;
            const int PARAMATERS_PER_CHROMOSOMES = 4;
            const int NUMBERS_PAIR_FOR_REPRODUCTION = 1;
            const int MAX_ADDITIONAL_TRY = 8;
            int m_MaximumFitness = aMap.GetMaximumFitness();
            int m_MaxTry = m_MaximumFitness + MAX_ADDITIONAL_TRY;
            double currentPopulationMaxAdaptation;

            GeneticOperator.SetMutationProbability(0.05f);
            GeneticOperator.SetCrossOverOnChromosomeProbability(0.50f);
            GeneticOperator.SetSelectionForReproductionProbability(0.95f);

            PopulationContour population = new PopulationContour(CHROMOSOMES_PER_GENERATION, aMap);
            population.GenerateAdditionalPopulation(m_MaxTry, PARAMATERS_PER_CHROMOSOMES);
            population.ComputeAdaptation();
            currentPopulationMaxAdaptation = population.GetCurrentMaxAdaptation();

            while (currentPopulationMaxAdaptation < m_MaximumFitness)
            {
                population.ToString();

                //Reproduction
                PopulationContour newPopulation = new PopulationContour(CHROMOSOMES_PER_GENERATION, aMap);

                #region elitisme
                Chromosome[] chromosomes = GeneticOperator.GetElites(population);
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
                //Variation Possibility
                GeneticOperator.CrossOver1Point(population);
                GeneticOperator.Mutate(population);

                //newPopulation.GenerateAdditionalPopulation(m_MaxTry, PARAMATERS_PER_CHROMOSOMES);
                //population.SetChromosomes(newPopulation.GetChromosomes()); 
                population.ComputeAdaptation();

                currentPopulationMaxAdaptation = population.GetCurrentMaxAdaptation();
            }

            bestChromosomeOfAll = population.GetBestChromosome();
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
