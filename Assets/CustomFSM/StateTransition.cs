using UnityEngine;

public enum TransitionType
{
    None,
    PlayerDetected,
    PlayerLost,
    BehindCover
}

public class StateTransition
{
    public State From;
    public TransitionType Type;
    public State To;

    public StateTransition(State from, TransitionType type, State to)
    {
        From = from;
        Type = type;
        To = to;
    }
}
