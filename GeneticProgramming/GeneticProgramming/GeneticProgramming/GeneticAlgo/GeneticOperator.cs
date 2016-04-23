﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class GeneticOperator
    {
        private static float m_MutationPercent;
        private static float m_CrossOverPercent;
        private static float m_SelectionForReproductionPercent;
        private const int PAIR_GAP = 2;

        public static void SetMutationProbability(float aPercent)
        {
            m_MutationPercent = aPercent;
        }

        public static void SetCrossOverOnChromosomeProbability(float aPercent)
        {
            m_CrossOverPercent = aPercent;
        }

        public static void SetSelectionForReproductionProbability(float aPercent)
        {
            m_SelectionForReproductionPercent = aPercent;
        }

        public static Chromosome GetRandomChromosome(Population aPopulation)
        {
            int nbChromosomes = aPopulation.GetCount();
            int aIndex = Ressources.m_Random.Next(nbChromosomes);
            return aPopulation.GetChromosomeAt(aIndex);
        }

        public static Chromosome GetRandomChromosome(Chromosome[] aChromosomes)
        {
            int aIndex = Ressources.m_Random.Next(aChromosomes.Length);
            return aChromosomes[aIndex];
        }

        /*enjambement : Possibilité de chance qu’un bit soit
                        échangé avec son homologue sur l’autre chromosome */
        public static void CrossOver1Point(Population aPopulation)
        {
            int affectedChromosomes = (int)(aPopulation.GetCount() * m_CrossOverPercent);
            while (affectedChromosomes > PAIR_GAP - 1)
            {
                Chromosome chromosome1 = GetRandomChromosome(aPopulation);
                Chromosome chromosome2 = null;

                while (chromosome2 == null || chromosome2 == chromosome1)
                {
                    chromosome2 = GetRandomChromosome(aPopulation);
                }

                ExchangeGene(chromosome1, chromosome2);

                affectedChromosomes -= PAIR_GAP;
            }
        }

        private static void ExchangeGene(Chromosome aChromosome1, Chromosome aChromosome2)
        {
            //get cross Point
            int maxPoint = aChromosome1.GetLenght();
            int crossPoint = Ressources.m_Random.Next(0, maxPoint);

            for (int i = crossPoint; i < maxPoint; i++)
            {
                int c1Gene = aChromosome1.GetGeneAt(i);
                int c2Gene = aChromosome2.GetGeneAt(i);
                aChromosome1.SetGeneAt(i, c2Gene);
                aChromosome2.SetGeneAt(i, c1Gene);
            }
        }

        public static Chromosome[] GetElites(Population aPopulation)
        {
            Chromosome[] chromosomes = aPopulation.GetChromosomesOrderedByHighestPerformance();
            int affectedChromosomes = (int)(aPopulation.GetCount() * m_CrossOverPercent);

            Chromosome[] chromosomesElites = new Chromosome[affectedChromosomes];
            for (int i = 0; i < affectedChromosomes; i++)
            {
                chromosomesElites[i] = chromosomes[i];
            }

            CrossOver1PointForElites(chromosomesElites);
            return chromosomesElites;
        }

        private static void CrossOver1PointForElites(Chromosome[] aChromosomesElite) // A OPTIMISER
        {
            int eliteStartIndex = aChromosomesElite.Length - 1;
            int eliteIndex;
            int maximumLoop = PAIR_GAP - 1;

            for (eliteIndex = eliteStartIndex; eliteIndex > maximumLoop; eliteIndex -= PAIR_GAP)
            {
                Chromosome chromosome1 = GetRandomChromosome(aChromosomesElite);
                Chromosome chromosome2 = null;

                while (chromosome2 == null || chromosome2 == chromosome1)
                {
                    chromosome2 = GetRandomChromosome(aChromosomesElite);
                }

                ExchangeGene(chromosome1, chromosome2);
            }
        }

        //mutation : Possibilité de chance q’un bit mute (passer de 1 à 0 ou de 0 à 1)
        public static void Mutate(Population aPopulation)
        {
            for (int i = 0; i < aPopulation.GetCount(); i++)
            {
                Chromosome chromosome = aPopulation.GetChromosomeAt(i);
                for (int j = 0; j < chromosome.GetLenght(); j++)
                {
                    double mutationPossibility = Ressources.m_Random.NextDouble();
                    if (mutationPossibility > m_MutationPercent)
                    {
                        continue;
                    }

                    int gene = chromosome.GetGeneAt(j);
                    int maxParameterIndice = chromosome.GetNumberOfParameter();
                    int randomParameter = Ressources.m_Random.Next(maxParameterIndice);
                    gene = (gene + randomParameter) % maxParameterIndice;
                    chromosome.SetGeneAt(j, gene);
                }
            }
        }

        public static Chromosome ReproductionByRank(Population aPopulation)
        {
            int populationCount = aPopulation.GetCount();
            Chromosome[] orderedChromosomeByRank = aPopulation.GetChromosomesOrderedByLowestPerformance();
            double[] rankScores = new double[populationCount];
            double sumRank = 0;
            for (int i = 0; i < populationCount; i++)
            {
                rankScores[i] = i + 1;
                sumRank += i;
            }

            return ReproductionByAdaptation(
                orderedChromosomeByRank,
                rankScores, 
                sumRank);
        }

        public static Chromosome ReproductionByRoulette(Population aPopulation)
        {
            return ReproductionByAdaptation(
                aPopulation.GetChromosomes(), 
                aPopulation.GetCurrentPopulationAdaptation(), 
                aPopulation.GetAdaptationTotal());
        }

        private static Chromosome ReproductionByAdaptation(Chromosome[] aChromosomes, double[] aAdaptations, double aAdaptationSum)
        {
            double randomPercent = Ressources.m_Random.NextDouble();
            double currentAdaptationSum = 0;
            double adaptationPercent = 0;
            Chromosome currentChromosome = null;

            for (int i = 0; i < aChromosomes.Length; i++)
            {
                currentAdaptationSum += aAdaptations[i];

                adaptationPercent = currentAdaptationSum / aAdaptationSum;
                if (adaptationPercent > randomPercent)
                {
                    currentChromosome = aChromosomes[i];
                    break;
                }
            }

            return currentChromosome;
        }

        public static Chromosome[] ReproductionByTournamenent(Population aPopulation, int aNbPair)
        {
            int minChromomes = aNbPair * PAIR_GAP;
            if (minChromomes > aPopulation.GetCount())
            {
                return null;
            }

            List<Chromosome> chromosomesWinner = new List<Chromosome>();
            List<Chromosome> chromosomesLooser = new List<Chromosome>();

            Chromosome chromosome1;
            Chromosome chromosome2;
            for (int i = 0; i < minChromomes; i += PAIR_GAP)
            {
                chromosome1 = GetRandomChromosome(aPopulation);
                chromosome2 = null;

                while (chromosome2 == null || chromosome2 == chromosome1)
                {
                    chromosome2 = GetRandomChromosome(aPopulation);
                }

                bool chromosome1Win = chromosome1.m_Adaptation > chromosome2.m_Adaptation;
                
                Chromosome winner = chromosome1Win ?  chromosome1 : chromosome2;
                Chromosome looser = chromosome1Win ?  chromosome2 : chromosome1;                
                chromosomesWinner.Add(winner);
                chromosomesLooser.Add(looser);
            }

            for (int i = 0; i < chromosomesWinner.Count; i++)
            {
                double randomSelection = Ressources.m_Random.NextDouble();
                if (randomSelection > m_SelectionForReproductionPercent)
                {
                    chromosomesWinner.RemoveAt(i);
                    chromosomesLooser.RemoveAt(i);
                }
                else
                {
                    aPopulation.RemoveChromosomes(chromosomesWinner.ToArray());
                    aPopulation.RemoveChromosomes(chromosomesLooser.ToArray());
            }
            }

            return chromosomesWinner.ToArray();
        }
    }
}
