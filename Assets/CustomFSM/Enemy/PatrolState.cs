using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PatrolState : State
{
    [Header("MOVEMENT")]
    public NavMeshAgent agent;
    public Vector2 idleTimeInterval = new Vector2(1, 2);
    public Transform[] waypoints;
    private float idleTimer;

    //Índice del punto de ruta actual
    private int waypointIndex;

    [Header("DETECTION")]

    public float detectionRange = 8;

    public LayerMask detectionLayers;

    public override void OnEnter()
    {
        //Iniciar el recorrido desde el primer waypoint
        waypointIndex = 0;
        //Inicializar el temporizador de Idle
        idleTimer = Random.Range(idleTimeInterval.x, idleTimeInterval.y);
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    public override TransitionType OnUpdate()
    {
        Vector3 direction = waypoints[waypointIndex].position - agent.transform.position;
        direction.y = 0;
        if(direction.sqrMagnitude <= agent.stoppingDistance + 0.05f)
        {
            idleTimer -= Time.deltaTime;
            if(idleTimer <= 0)
            {
                idleTimer = Random.Range(idleTimeInterval.x, idleTimeInterval.y);
                waypointIndex++;
                //Si ha llegado al último punto, vuelve a empezar desde el primero
                if (waypointIndex >= waypoints.Length)
                {
                    waypointIndex = 0;
                }
                agent.SetDestination(waypoints[waypointIndex].position);
            }
        }

        //Detecta a su alrededor con un Overlap. Si detecta al jugador, cambia de estado
        Collider[] targets = Physics.OverlapSphere(agent.transform.position, detectionRange, detectionLayers);

        if (targets.Length > 0)
        {
            return TransitionType.PlayerDetected;
        }

        return TransitionType.None;
    }
}
