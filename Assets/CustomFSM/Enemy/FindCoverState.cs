using UnityEngine;
using UnityEngine.AI;

public class FindCoverState : State
{
    public float searchRange = 20;

    public LayerMask coverLayers;

    public float speedModifier = 3;

    public NavMeshAgent agent;

    public override void OnEnter()
    {
        
    }
}
