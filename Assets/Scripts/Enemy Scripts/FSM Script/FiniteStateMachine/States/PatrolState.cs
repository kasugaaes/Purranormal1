using UnityEngine;

public class PatrolState : FSMState
{
    public override StateID StateId => StateID.Patrol;

    private KodamaFSM _kodama;
    private Transform _currentTarget;
    private Transform[] _target;

    //Constructor

    public PatrolState(KodamaFSM kodama, Transform[] waypoints)
    {
        _target = waypoints;
        _kodama = kodama;
        RandomizeWaypointTarget();
    }

    public override void CheckTransition(Transform agent, Transform player)
    {
        // Check the rules of our transition
       
    }

    public override void RunState(Transform agent, Transform player)
    {
        

    }

    private void RandomizeWaypointTarget()
    {
        // Randomize a value from the array.
        // Random.Range when int, max is exclusive so we can directly use the length of array
        int randomIndex = Random.Range(0, _target.Length);

        // Ensure that the next randomized waypoint is unique
        while (_target[randomIndex] == _currentTarget)
        {
            randomIndex = Random.Range(0, _target.Length);
        }
        SetCurrentTarget(_target[randomIndex]);
    }

    private void SetCurrentTarget(Transform target)
    {
        _currentTarget = target;
    }
}
