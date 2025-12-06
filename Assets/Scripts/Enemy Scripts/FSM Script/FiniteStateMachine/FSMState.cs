using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    public abstract void RunState(Transform agent, Transform player);
    public abstract void CheckTransition(Transform agent, Transform player);

    // This collection will contain the conditions and what state it will transition to when the condition is met
    protected Dictionary<TransitionID, StateID> _transitionMap = new Dictionary<TransitionID, StateID>();

    // property
    public abstract StateID StateId { get; }
    // A shortcut for a getter:
    /*
    public StateID StateId
    {
        get 
        { 
            return _stateId;
        }
    }*/
    protected StateID _stateId;

    public void AddTransition(TransitionID transitionID, StateID stateID)
    {
        // Validity checks
        if (transitionID == TransitionID.None || stateID == StateID.None)
        {
            return;
        }
        // Dictionaries by rule can only have 1 unique Key
        if (_transitionMap.ContainsKey(transitionID))
        {
            return;
        }
        _transitionMap.Add(transitionID, stateID);
    }

    public void RemoveTransition(TransitionID transitionID)
    {
        if (transitionID == TransitionID.None)
        {
            return;
        }
        if (_transitionMap.ContainsKey(transitionID))
        {
            _transitionMap.Remove(transitionID);
        }
    }

    // This returns the next state the FSM should be if it detects the transition
    // based on the transition map
    public StateID GetOutputState(TransitionID transitionID)
    {
        if (transitionID == TransitionID.None)
        {
            return StateID.None;
        }
        if (_transitionMap.ContainsKey(transitionID))
        {
            return _transitionMap[transitionID];
        }
        return StateID.None;
    }
}
