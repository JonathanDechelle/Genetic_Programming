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
            GeneticOperator.SetMutationProbability(0.05f);
            GeneticOperator.SetCrossOverOnChromosomeProbability(0.5f);
            GeneticOperator.SetReproductionOnChromosomeProbability(0.65f);
            GeneticOperator.SetSelectionForReproductionProbability(0.95f);

            Population population = new Population();
            population.GeneratePopulation(10, 4);

            population.ComputeAdaptation();
            population.ToString();

            if (population.GetMaxAdaptation() >= double.MaxValue) //arbitrary value for now
            {
				//IA BEAT THE GAME
                //break;
            }

			//Reproduction
            /*Chromosome[] currentChromosomes = population.GetChromosomes();
            currentChromosomes = GeneticOperator.ReproductionByTournamenent(population);
            if (currentChromosomes != null)
            {
                population.SetChromosomes(currentChromosomes);
                population.ToString();
            }*/

            //Variation Possibility
            GeneticOperator.CrossOver1Point(population);
            GeneticOperator.Mutate(population);
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
