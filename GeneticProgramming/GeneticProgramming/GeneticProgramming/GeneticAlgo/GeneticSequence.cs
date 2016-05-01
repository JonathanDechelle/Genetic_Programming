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
    }

    public class GeneticSequence
    {
        private StateMachine m_StateMachine;

        public GeneticSequence()
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
        }

        public void Update()
        {
            m_StateMachine.Update();
        }

        public void OnEnterGenerateFirstPopulation()
        {

        }

        public void OnExitGenerateFirstPopulation()
        {

        }

        public void OnEnterGenerateNewPopulation()
        {

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