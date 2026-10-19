using UnityEngine;
using UnityEngine.AI;   

public class AnimalNPC : MonoBehaviour
{
    //Rango de tiempo que dura el Idle (aleatorio)
    public Vector2 idleTimeRange = new Vector2(3, 5);

    public NavMeshAgent agent;

    public float moveDistance = 6f;

    public Vector2 poopTimeRange = new Vector2(6, 8);
    public ParticleSystem poopPS;

    //Rango para detectar objetos de los que tenga que huir
    public float detectRange = 4;
    public LayerMask dangerLayers;

    void Start()
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

    public Transform DetectDanger()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectRange, dangerLayers);

        if (detectedObjects.Length <= 0)
        {
            return null;
        }
        else
        {
            //Devuelve un objeto aleatorio de la lista de objetos detectados (debería ser del más cercano, pero así es más rápido
            return detectedObjects[Random.Range(0, detectedObjects.Length)].transform;
        }
    }

    private void OnDrawGizmos()
    {
        //Rango de movimiento
        Gizmos.color = Color.cornflowerBlue; //Color muy top
        Gizmos.DrawWireSphere(transform.position, moveDistance);

        //Detección de objetos de los que huir
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
