using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Example
    {
        public static Situation m_Situation = new Situation();
        private static Chromosome m_BestChromosome;

        public static void GenerateSimpleGPExample(OutlineMap aMap)
        {
            SituationData situationData = new SituationData();
            situationData.m_ParametersPerChromosomes = 4;
            situationData.m_ChromosomesPerGeneration = 100;
            situationData.m_NumbersPairForReproduction = 1;
            situationData.m_AdditionalTry = 3;
            situationData.m_BestChromosomeByXGeneration = 35f;
            situationData.m_MaximumFitness = aMap.GetMaximumFitness();
            situationData.m_MutationPercent = 0.03f;
            situationData.m_CrossOverPercent = 0.15f;
            situationData.m_ReproductionPercent = 1.00f;

            GeneticOperator.SetOperatorPercents(situationData);

            OutlinePopulation population = new OutlinePopulation(situationData, aMap);
            population.GenerateAdditionalPopulation(situationData);
            population.ComputeAdaptation();
            situationData.m_CurrentMaxAdaptation = population.GetMaxAdaptation();

            int nbGenerations = 0;
            while (situationData.m_CurrentMaxAdaptation < situationData.m_MaximumFitness) 
            {
                population.ToString();

                //Reproduction
                OutlinePopulation newPopulation = new OutlinePopulation(situationData.m_ChromosomesPerGeneration, aMap);
                
                #region elitisme
                Chromosome[] parents = GeneticOperator.GetElites(population);
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

                newPopulation.GenerateAdditionalPopulation(situationData);
                GeneticOperator.Mutate(newPopulation);

                population.SetChromosomes(newPopulation.GetChromosomes()); 
                population.ComputeAdaptation();

                m_BestChromosome = population.GetBestChromosome();
                situationData.m_CurrentMaxAdaptation = m_BestChromosome.m_Adaptation;

                nbGenerations++;
                if (nbGenerations % situationData.m_BestChromosomeByXGeneration == 0)
                {
                    double bestAdaptation = m_Situation.GetBestAdaptation();
                    if (bestAdaptation < m_BestChromosome.m_Adaptation)
                    {
                        m_Situation.AddABestChromosome(m_BestChromosome.Clone());
                    }
                }
            }

            m_Situation.AddABestChromosome(m_BestChromosome.Clone());
            Console.WriteLine("Nb Generation = " + nbGenerations); 
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
