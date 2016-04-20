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
            const int CHROMOSOMES_PER_GENERATION = 10;
            const int PARAMATERS_PER_CHROMOSOMES = 4;

            GeneticOperator.SetMutationProbability(0.05f);
            GeneticOperator.SetCrossOverOnChromosomeProbability(0.5f);
            GeneticOperator.SetReproductionOnChromosomeProbability(0.65f);
            GeneticOperator.SetSelectionForReproductionProbability(0.95f);

            Population population = new Population(CHROMOSOMES_PER_GENERATION);
            population.GeneratePopulation(PARAMATERS_PER_CHROMOSOMES);


            population.ComputeAdaptation();
            population.ToString();

            if (population.GetMaxAdaptation() >= double.MaxValue) //arbitrary value for now
            {
				//IA BEAT THE GAME
                //break;
            }

			//Reproduction
            Population newPopulation = new Population(CHROMOSOMES_PER_GENERATION);

            Chromosome[] chromosomesWinner = GeneticOperator.ReproductionByTournamenent(population);
            if (chromosomesWinner != null)
            {
                newPopulation.AddChromosomes(chromosomesWinner);
            }

            newPopulation.ToString();
            
            //Variation Possibility
            GeneticOperator.CrossOver1Point(newPopulation);
            GeneticOperator.Mutate(newPopulation);
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
