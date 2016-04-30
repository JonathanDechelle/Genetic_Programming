using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class Example
    {
        public static Situation m_Situation;
        private static Chromosome m_BestChromosome;

        public static void GenerateSimpleGPExample(OutlineMap aMap, SituationData aSituationData)
        {
            GeneticOperator.SetOperatorPercents(aSituationData);

            //Generate the first population 
            OutlinePopulation population = new OutlinePopulation(aSituationData, aMap);
            population.GenerateAdditionalPopulation(aSituationData);
            population.ComputeAdaptation();
            aSituationData.m_CurrentMaxAdaptation = population.GetMaxAdaptation();

            m_Situation = new Situation(aSituationData);

            int nbGenerations = 0;
            while (!m_Situation.HasSucceeded) 
            {
                population.ToString();

                /*** REPRODUCTION START ***/
                OutlinePopulation newPopulation = new OutlinePopulation(aSituationData.m_ChromosomesPerGeneration, aMap);
                
                /******** GET THE BEST CHROMOSOME ********/
                Chromosome[] parents = GeneticOperator.GetElites(population);
                newPopulation.AddChromosomes(parents);

                Chromosome[] childrens = GeneticOperator.CrossOver1Point(parents); // parents are 100% crossOver 
                newPopulation.AddChromosomes(childrens);
                newPopulation.ComputeAdaptation();
                /****************************************/

                //Be sure population is equal to start generation
                newPopulation.GenerateAdditionalPopulation(aSituationData);

                //Add random mutation
                GeneticOperator.Mutate(newPopulation);
                
                //Replace old generation by the new one
                population.SetChromosomes(newPopulation.GetChromosomes()); 
                population.ComputeAdaptation();
                
                //update the actual situation (who is the best chromsome and how he is adapted)
                m_BestChromosome = population.GetBestChromosome();
                aSituationData.m_CurrentMaxAdaptation = m_BestChromosome.m_Adaptation;

                //Use for external usage get list of X best chromoses during the experience
                nbGenerations++;
                if (nbGenerations % aSituationData.m_BestChromosomeByXGeneration == 0)
                {
                    double bestAdaptation = m_Situation.GetBestAdaptation();
                    if (bestAdaptation < m_BestChromosome.m_Adaptation)
                    {
                        m_Situation.AddABestChromosome(m_BestChromosome.Clone());
                    }
                }
            }

            // Add the best chromose of all
            m_Situation.AddABestChromosome(m_BestChromosome.Clone());
            Console.WriteLine("Nb Generation = " + nbGenerations);
        }

        #region algorithme explanation
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
        #endregion
    }
}
