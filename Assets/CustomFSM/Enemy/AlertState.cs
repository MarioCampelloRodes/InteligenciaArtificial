using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AlertState : State
{
    //Rango del overlap de detección
    public float hearingRange = 6;

    public float visionAngle = 90;

    public LayerMask detectionLayers;
    public NavMeshAgent agent;

    private Transform player;

    public override void OnEnter()
    {
        //Va a la última posición en la que vio al jugador (la posición en la que esté en ese momento)
        player = GameObject.FindWithTag("Player").transform;
        agent.SetDestination(player.position);
    }

    public override TransitionType OnUpdate()
    {
        //Calcula la dirección hacia el objeto detectado (player)
        Vector3 dirToTarget = player.position - agent.transform.position;
        dirToTarget.y = 0;

        //Calcular el producto escalar entre la dirección en la que mira y donde está el objetivo
        float dotProduct = Vector3.Dot(agent.transform.forward, dirToTarget.normalized);
        Debug.Log(dotProduct);


        //Comprobar si el jugador está en su rango de visión
        if (dotProduct > Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad))
        {
            return TransitionType.PlayerDetected;
        }
        
        //Si llega a la posición donde vio al objetivo pero no lo detecta
        if(agent.remainingDistance < agent.stoppingDistance + 0.1f)
        {
            return TransitionType.PlayerLost;
        }

        return TransitionType.None;
    }
    public override void OnExit()
    {

    }
}
