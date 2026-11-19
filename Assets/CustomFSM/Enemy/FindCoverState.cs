using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class FindCoverState : State
{
    public float searchRange = 20;

    public LayerMask coverLayers;

    public float speedModifier = 3;

    public NavMeshAgent agent;

    //El producto escalar mínimo que considerará una cobertura válida
    public float minimumDotProduct = -0.5f;

    //Cuando busca cobertura en el lado contrario del objeto, cuánto desplaza la posición original en esa dirección
    public float normalDisplacement = 2f;

    private Transform playerPos;

    public override void OnEnter()
    {
        //Buscar al player cuando entra al estado
        if(playerPos == null)
        {
            playerPos = GameObject.FindWithTag("Player").transform;
        }

        FindCover();
    }

    public override TransitionType OnUpdate()
    {
        //Si llega a la posición donde tiene que esconderse
        if (agent.remainingDistance < agent.stoppingDistance + 0.1f)
        {
            //Devuelve una transición de tipo ESCONDIDO
            return TransitionType.BehindCover;
        }

        return TransitionType.None;
    }

    void FindCover()
    {
        //Busca todas las posibles coberturas en su rango
        Collider[] covers = Physics.OverlapSphere(agent.transform.position, searchRange, coverLayers);

        if(covers.Length < 0) return;

        //Buscar la posición dentro del NavMesh más cercana a la posición de la cobertura
        if (NavMesh.SamplePosition(covers[0].transform.position, out NavMeshHit hit, agent.height * 2, agent.areaMask))
        {
            //Guardar la posición de la cobertura ya dentro del NavMesh
            Vector3 coverPos = hit.position;

            //Calcular la dirección de la cobertura hacia el jugador
            Vector3 dirToTarget = (playerPos.position - coverPos).normalized;

            //Buscar la arista de la NavMesh más cercana a la posición de la cobertura
            //La arista guarda la normal de la superficie, que es lo que nos interesa
            if (NavMesh.FindClosestEdge(coverPos, out hit, agent.areaMask))
            {
                if(IsValidCover(dirToTarget, hit.normal))
                {
                    agent.SetDestination(hit.position);
                }
                //Si no es una cobertura válida, hay que buscar una posición en el borde contrario
                else
                {
                    FindOppositeEdge(coverPos, hit.normal);
                }
            }
        }      
    }
    void FindOppositeEdge(Vector3 coverPos, Vector3 normalDir)
    {
        //Desplazar la posición de la cobertura hacia el lado contrario para encontrar ese borde
        coverPos -= normalDir * normalDisplacement;

        if (NavMesh.SamplePosition(coverPos, out NavMeshHit hit, agent.height * 2, agent.areaMask))
        {
            //Guardar la posición de la cobertura ya dentro del NavMesh
            coverPos = hit.position;

            //Calcular la dirección de la cobertura hacia el jugador
            Vector3 dirToTarget = (playerPos.position - coverPos).normalized;

            //Buscar la arista de la NavMesh más cercana a la posición de la cobertura
            //La arista guarda la normal de la superficie, que es lo que nos interesa
            if (NavMesh.FindClosestEdge(coverPos, out hit, agent.areaMask))
            {
                if (IsValidCover(dirToTarget, hit.normal))
                {
                    agent.SetDestination(hit.position);
                }
            }
        }
    }

    private bool IsValidCover(Vector3 dirToTarget, Vector3 coverNormal)
    {
        //Calcular el producto escalar entre la dirección de la cobertura al jugador y la normal de la cobertura
        float dotProduct = Vector3.Dot(dirToTarget, coverNormal);

        //Si ambas direcciones son contrarias, la cobertura es válida
        return dotProduct < minimumDotProduct;
    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.navajoWhite;

        Gizmos.DrawWireSphere(agent.transform.position, searchRange);
    }
}
