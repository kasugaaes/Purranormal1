// used in SimpleFSM
public enum State
{
    Patrol,
    Chase,
    Attack,
    Dead
}


// used in AdvancedFSM

public enum StateID
{
    None,
    Patrol,
    Revenge,
    Flee,
    Dead
}

public enum TransitionID
{
    None,
    GetHitByPlayer,
    LostPlayer,
    ReachPlayer,
    Hurt,
    Death
}