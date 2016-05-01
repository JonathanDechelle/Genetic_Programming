using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public enum ESequenceState
    {
        GENERATE_FIRST_POPULATION = 0,
        GENERATE_NEW_POPULATION = 1,
        GET_THE_ELITES = 2,
        REPRODUCTION = 3,
        EQUALIZE_POPULATION = 4,
        RANDOM_MUTATION = 5,
        REPLACE_OLD_GENERATION = 6,
        UPDATE_SITUATION = 7,
        EXPERIENCE_FINISH = 8,
    }

    public class GeneticSequence
    {
        private StateMachine m_StateMachine;
        
        private Situation m_Situation;
        protected SituationData m_SituationData;
        
        protected Population m_Population;
        protected Population m_NewPopulation;

        private Chromosome m_BestChromosome;
        private Chromosome[] m_ParentChromosomes;
        private int m_NbGenerations = 0;

        public GeneticSequence(SituationData aSituationData)
        {
            m_StateMachine = new StateMachine();
            m_StateMachine.AddState(ESequenceState.GENERATE_FIRST_POPULATION, Status.OnEnter, OnEnterGenerateFirstPopulation);
            m_StateMachine.AddState(ESequenceState.GENERATE_FIRST_POPULATION, Status.OnExit, OnExitGenerateFirstPopulation);
            m_StateMachine.AddState(ESequenceState.GENERATE_NEW_POPULATION, Status.OnEnter, OnEnterGenerateNewPopulation);
            m_StateMachine.AddState(ESequenceState.GET_THE_ELITES, Status.OnExit, OnEnterGetTheElites);
            m_StateMachine.AddState(ESequenceState.REPRODUCTION, Status.OnEnter, OnEnterReproduction);
            m_StateMachine.AddState(ESequenceState.REPRODUCTION, Status.OnExit, OnExitReproduction);
            m_StateMachine.AddState(ESequenceState.EQUALIZE_POPULATION, Status.OnEnter, OnEnterEqualizePopulation);
            m_StateMachine.AddState(ESequenceState.RANDOM_MUTATION, Status.OnEnter, OnEnterRandomMutation);
            m_StateMachine.AddState(ESequenceState.REPLACE_OLD_GENERATION, Status.OnEnter, OnEnterReplaceOldGeneration);
            m_StateMachine.AddState(ESequenceState.REPLACE_OLD_GENERATION, Status.OnExit, OnExitReplaceOldGeneration);
            m_StateMachine.AddState(ESequenceState.UPDATE_SITUATION, Status.OnEnter, OnEnterUpdateSituation);
            m_StateMachine.AddState(ESequenceState.UPDATE_SITUATION, Status.OnExit, OnExitUpdateSituation);
            m_StateMachine.AddState(ESequenceState.EXPERIENCE_FINISH, Status.OnEnter, OnEnterExperienceFinish);

            m_SituationData = aSituationData;
            m_StateMachine.SetState(ESequenceState.GENERATE_FIRST_POPULATION);
        }

        public void Update()
        {
            m_StateMachine.Update();
        }

        protected virtual void InitializeFirstPopulation()
        {
            m_Population = new Population(m_SituationData);
        }

        protected void OnEnterGenerateFirstPopulation()
        {
            InitializeFirstPopulation();
            m_Population.GenerateAdditionalPopulation(m_SituationData);

            m_Situation = new Situation(m_SituationData);
            GeneticOperator.SetOperatorPercents(m_SituationData);

            m_StateMachine.SetState(ESequenceState.GENERATE_NEW_POPULATION);
        }

        protected void OnExitGenerateFirstPopulation()
        {
            m_Population.ComputeAdaptation(); 
            UpdateCurrentAdaptation();
        }

        protected void OnEnterExperienceFinish()
        {
            m_Situation.AddABestChromosome(m_BestChromosome.Clone());
            Console.WriteLine("Nb Generation = " + m_NbGenerations);
        }

        protected virtual void InitializeNewtPopulation()
        {
            m_Population = new Population(m_SituationData);
        }

        protected void OnEnterGenerateNewPopulation()
        {
            if (m_Situation.HasSucceeded)
            {
                m_StateMachine.SetState(ESequenceState.EXPERIENCE_FINISH);
                return;
            }

            InitializeNewtPopulation();

            m_StateMachine.SetState(ESequenceState.GET_THE_ELITES);
        }

        protected void OnEnterGetTheElites()
        {
            m_ParentChromosomes = GeneticOperator.GetElites(m_Population);
            m_NewPopulation.AddChromosomes(m_ParentChromosomes);

            m_StateMachine.SetState(ESequenceState.REPRODUCTION);
        }

        protected void OnEnterReproduction()
        {
            Chromosome[] childrens = GeneticOperator.CrossOver1Point(m_ParentChromosomes);
            m_NewPopulation.AddChromosomes(childrens);

            m_StateMachine.SetState(ESequenceState.EQUALIZE_POPULATION);
        }

        protected void OnExitReproduction()
        {
            m_NewPopulation.ComputeAdaptation();
        }

        protected void OnEnterEqualizePopulation()
        {
            m_NewPopulation.GenerateAdditionalPopulation(m_SituationData);

            m_StateMachine.SetState(ESequenceState.RANDOM_MUTATION);
        }

        protected void OnEnterRandomMutation()
        {
            GeneticOperator.Mutate(m_NewPopulation);

            m_StateMachine.SetState(ESequenceState.REPLACE_OLD_GENERATION);
        }

        protected void OnEnterReplaceOldGeneration()
        {
            m_Population.SetChromosomes(m_NewPopulation.GetChromosomes());

            m_StateMachine.SetState(ESequenceState.UPDATE_SITUATION);
        }

        protected void OnExitReplaceOldGeneration()
        {
            m_Population.ComputeAdaptation();
        }

        protected void OnEnterUpdateSituation()
        {
            UpdateCurrentAdaptation();
            m_StateMachine.SetState(ESequenceState.GENERATE_NEW_POPULATION);
        }

        private void UpdateCurrentAdaptation()
        {
            m_BestChromosome = m_Population.GetBestChromosome();
            m_SituationData.m_CurrentMaxAdaptation = m_BestChromosome.m_Adaptation;
        }

        protected void OnExitUpdateSituation()
        {
            m_NbGenerations++;
            if (m_NbGenerations % m_SituationData.m_BestChromosomeByXGeneration == 0)
            {
                double bestAdaptation = m_Situation.GetBestAdaptation();
                if (bestAdaptation < m_BestChromosome.m_Adaptation)
                {
                    m_Situation.AddABestChromosome(m_BestChromosome.Clone());
                }
            }
        }
    } 
}