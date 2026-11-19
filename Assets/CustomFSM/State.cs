using UnityEngine;

//Abstract sirve para indicar que esta clase sólo sirve para heredar de ella
[System.Serializable]
public abstract class State
{
    public string name = "State";

    public virtual void OnEnter() { }

    public abstract TransitionType OnUpdate();

    public virtual void OnExit() { }

    public virtual void DrawGizmos() { }
}
