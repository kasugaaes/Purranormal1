using UnityEngine;

public class SimpleFiniteStateMachine : FiniteStateMachine
{
    // We have the option to implement Initialize
    protected override void Initialize()
    {
        // We also have the option to perform the original behaviour of the parent class
        // base.Initialize();
        Debug.Log("This is the initialize function of the Simple FSM");
    }

    // We are obligated to implement the abstract method
    protected override void UpdateStateMachine()
    {

    }
}
