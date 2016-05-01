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
        private SituationData m_SituationData;
        private Population m_Population;
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
        }

        public void Update()
        {
            m_StateMachine.Update();
        }

        public void OnEnterGenerateFirstPopulation()
        {
            m_Population = new Population(m_SituationData);
            m_Population.GenerateAdditionalPopulation(m_SituationData);

            m_Situation = new Situation(m_SituationData);
            GeneticOperator.SetOperatorPercents(m_SituationData);
        }

        public void OnExitGenerateFirstPopulation()
        {
            m_Population.ComputeAdaptation();
            m_SituationData.m_CurrentMaxAdaptation = m_Population.GetMaxAdaptation();
        }

        public void OnEnterExperienceFinish()
        {

        }

        public void OnEnterGenerateNewPopulation()
        {
            if (m_Situation.HasSucceeded)
            {
                m_StateMachine.SetState(ESequenceState.EXPERIENCE_FINISH);
            }
        }

        public void OnEnterGetTheElites()
        {

        }

        public void OnEnterReproduction()
        {

        }

        public void OnExitReproduction()
        {

        }

        public void OnEnterEqualizePopulation()
        {

        }

        public void OnEnterRandomMutation()
        {

        }

        public void OnEnterReplaceOldGeneration()
        {

        }

        public void OnExitReplaceOldGeneration()
        {

        }

        public void OnEnterUpdateSituation()
        {

        }

        public void OnExitUpdateSituation()
        {

        }
    } 
}