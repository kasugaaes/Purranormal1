using UnityEngine;

// This cannote be attached to the game object
// Because abstract classes are meant to be EXTENDED not INSTANTIATED
public abstract class FiniteStateMachine : MonoBehaviour
{
    // With virtual functions, you can define your method and give it things to do
    protected virtual void Initialize()
    {
        Debug.Log("This is an important initialization code.");
    }

    // With abstract function, you can only define them because the derived
    // class is obligated to implement it
    protected abstract void UpdateStateMachine();

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateStateMachine();
    }
}
