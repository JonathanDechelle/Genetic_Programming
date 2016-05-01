using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class StateMachine
    {
        private List<State> m_StateList;
        private State m_CurrentState;
        private State m_NextState;

        public StateMachine()
        {
            m_StateList = new List<State>();
        }

        public void AddState(Enum aState, Status aStatus, Action aFunctionToCall)
        {
            State state = new State();
            state.m_State = aState;
            state.m_Status = aStatus;
            state.m_FunctionToCall = aFunctionToCall;

            m_StateList.Add(state);
        }

        public void SetState(Enum aState)
        {
            if (m_CurrentState == null)
            {
                m_CurrentState = new State();
                m_CurrentState.m_State = aState;
                m_CurrentState.m_Status = Status.OnEnter;
                m_CurrentState.m_FunctionToCall = GetOnEnterFunction(m_CurrentState);

                if (m_CurrentState.m_FunctionToCall != null)
                {
                    m_CurrentState.m_FunctionToCall();
                }

                m_NextState = GetStateCopy(m_CurrentState);
            }

            m_NextState.m_State = aState;
        }

        public int GetCurrentState()
        {
            return Convert.ToInt32(m_CurrentState.m_State);
        }
            
        private State GetStateCopy(State aOriginalState)
        {
            State newState = new State();
            newState.m_Status = aOriginalState.m_Status;
            newState.m_FunctionToCall = aOriginalState.m_FunctionToCall;
            return newState;
        }

        public void Update()
        {
            if (m_CurrentState.m_State != m_NextState.m_State)
            {
                SwitchNextState();
            }

            Action currentOnUpdateFunction = GetOnUpdateFunction(m_CurrentState);
            if (currentOnUpdateFunction != null)
            {
                currentOnUpdateFunction();
            }
        }

        private void SwitchNextState()
        {
            Action currentOnExitFunction = GetOnExitFunction(m_CurrentState);
            if (currentOnExitFunction != null)
            {
                currentOnExitFunction();
            }

            m_CurrentState.m_State = m_NextState.m_State;

            Action currentOnEnterFunction = GetOnEnterFunction(m_CurrentState);
            if (currentOnEnterFunction != null)
            {
                currentOnEnterFunction();
            }
        }

        private Dictionary<Action, Status> GetAllStatusFunction(State aState)
        {
            Dictionary<Action, Status> statusActionDicto = new Dictionary<Action, Status>();

            for (int i = 0; i < m_StateList.Count; i++)
            {
                int stateToCheck = Convert.ToInt32(aState.m_State);
                int stateInList = Convert.ToInt32(m_StateList[i].m_State);

                if (stateToCheck == stateInList)
                {
                    statusActionDicto.Add(m_StateList[i].m_FunctionToCall, m_StateList[i].m_Status);
                }
            }

            return statusActionDicto;
        }

        private Action GetOnEnterFunction(State aState)
        {
            Dictionary<Action, Status> statusActionDicto = GetAllStatusFunction(aState);

            foreach (KeyValuePair<Action, Status> entry in statusActionDicto)
            {
                if (entry.Value == Status.OnEnter)
                {
                    return entry.Key;
                }
            }

            return null;
        }

        private Action GetOnUpdateFunction(State aState)
        {
            Dictionary<Action, Status> statusActionDicto = GetAllStatusFunction(aState);

            foreach (KeyValuePair<Action, Status> entry in statusActionDicto)
            {
                if (entry.Value == Status.OnUpdate)
                {
                    return entry.Key;
                }
            }

            return null;
        }

        private Action GetOnExitFunction(State aState)
        {
            Dictionary<Action, Status> statusActionDicto = GetAllStatusFunction(aState);

            foreach (KeyValuePair<Action, Status> entry in statusActionDicto)
            {
                if (entry.Value == Status.OnExit)
                {
                    return entry.Key;
                }
            }

            return null;
        }
    }

    public class State
    {
        public Enum m_State;
        public Status m_Status;
        public Action m_FunctionToCall;

        public State()
        {
            /* Initialize */
        }
    }

    public enum Status
    {
        OnEnter,
        OnUpdate,
        OnExit,
    }
}
