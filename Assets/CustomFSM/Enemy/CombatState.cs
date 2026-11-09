using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class CombatState : State
{
    public NavMeshAgent agent;
    public Transform target;

    public float attackRange = 6;
    public float timeToAttack = 0.5f;
    public float rotationSpeed = 5;

    private float attackTimer;

    public override void OnEnter()
    {
        agent.stoppingDistance += attackRange;
        
        //Si es la primera vez que entra al estado, busca al jugador por tag y lo guarda
        if(target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    public override void OnUpdate()
    {
        agent.SetDestination(target.position);
        Vector3 targetDir = target.position - agent.transform.position;
        targetDir.y = 0;

        if(targetDir.sqrMagnitude <= attackRange * attackRange)
        {
            Shoot();
        }

        //Mirar hacia el objetivo
        LookAtPlayer(targetDir);
    }

    public override void OnExit()
    {
        agent.stoppingDistance -= attackRange;
    }

    void Shoot()
    {
        if(Time.time >= attackTimer)
        {
            attackTimer = Time.time + timeToAttack;
            Debug.Log("PIUM PIUM");
        }
    }

    void LookAtPlayer(Vector3 playerDir)
    {
        //Calcular la rotación y rotar para mirar hacia el jugador
        Quaternion rotation = Quaternion.LookRotation(playerDir);
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
