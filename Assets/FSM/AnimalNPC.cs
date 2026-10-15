using UnityEngine;
using UnityEngine.AI;   

public class AnimalNPC : MonoBehaviour
{
    //Rango de tiempo que dura el Idle (aleatorio)
    public Vector2 idleTimeRange = new Vector2(3, 5);

    public NavMeshAgent agent;

    public float moveDistance = 6f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Calcula una posición aleatoria dentro del Navmesh
    public Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-moveDistance, moveDistance), transform.position.y, Random.Range(-moveDistance, moveDistance));

        position += transform.position;

        //Con esta función se puede comprobar si el punto está dentro de la Navmesh
        //El valor de maxDistance siempre es el doble de la altura del agente
        if(NavMesh.SamplePosition(position, out NavMeshHit hit, agent.height * 2, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cornflowerBlue; //Color muy top
        Gizmos.DrawWireSphere(transform.position, moveDistance);
    }
}
