using UnityEngine;

//Abstract sirve para indicar que esta clase solo sirve para heredar de ella
public abstract class State
{
    public string name = "Default State";

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
