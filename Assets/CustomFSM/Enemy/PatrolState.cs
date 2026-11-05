using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PatrolState : State
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public Vector2 idleTimeInterval = new Vector2(1, 2);
    private float idleTimer;

    //Índice del punto de ruta actual
    private int waypointIndex;

    public override void OnEnter()
    {
        //Iniciar el recorrido desde el primer waypoint
        waypointIndex = 0;
        //Inicializar el temporizador de Idle
        idleTimer = Random.Range(idleTimeInterval.x, idleTimeInterval.y);
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    public override void OnUpdate()
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
    }
}
