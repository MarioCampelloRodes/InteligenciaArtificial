using UnityEngine;

//Abstract sirve para indicar que esta clase solo sirve para heredar de ella
[System.Serializable]
public abstract class State
{
    public string name = "Default State";
    public State stateToChange;

    //La máquina de estados con la que debe cambiar de uno a otro
    protected StateMachine stateMachine;

    public void Setup(StateMachine machine, State state)
    {
        stateMachine = machine;
        stateToChange = state;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
