using System.Collections.Generic;
using UnityEngine;

public class AdvancedFiniteStateMachine : FiniteStateMachine
{
    public FSMState CurrentState => _currentState;
    private FSMState _currentState;
    private StateID _currentStateID;

    protected List<FSMState> _states = new();

    /// <summary>
    /// Adds a state to the state machine
    /// </summary>
    /// <param name="state"></param>
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            return;
        }

        //Check if the state to be added is the first state
        //and make it the default state
        if (_states.Count == 0)
        {
            _states.Add(state);
            _currentState = state;
            _currentStateID = state.StateId;
            Debug.Log($"Default state is {_currentState}");
        }
        if (_states.Contains(state))
        {
            return;
        }
        _states.Add(state);
    }

    /// <summary>
    /// Removes state from the state machine
    /// </summary>
    /// <param name="state"></param>
    public void RemoveState(FSMState state)
    {
        if (state == null)
        {
            return;
        }
        if (_states.Contains(state))
        {
            _states.Remove(state);
        }
    }

    /// <summary>
    /// Attempt to change the state of the FSM based on the given transition
    /// </summary>
    /// <param name="transitionID"></param>
    public void PerformTransition(TransitionID transitionID)
    {
        if (transitionID == TransitionID.None)
        {
            return;
        }
        // Check if the current state of the FSM has the transition from the transitionMap
        StateID stateId = _currentState.GetOutputState(transitionID);
        if (stateId == StateID.None)
        {
            return;
        }
        // Set the current state
        _currentStateID = stateId;
        // Look for the id in our list
        foreach (FSMState state in _states)
        {
            if (state.StateId == stateId)
            {
                _currentState = state;
                break;
            }
        }
    }

    protected override void UpdateStateMachine()
    {
        //_currentState.StateId = StateID.Patrol;
    }
}
