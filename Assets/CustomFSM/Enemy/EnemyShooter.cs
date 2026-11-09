using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header ("STATES")]
    [SerializeField] private PatrolState patrol;
    [SerializeField] private CombatState combat;

    private StateMachine stateMachine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Crear y guardar una máquina de estados
        stateMachine = new StateMachine();

        //Empezar en un estado
        stateMachine.ChangeState(patrol);

        //Asignar el estado a cambiar cuando se cumplan ciertas condiciones
        patrol.Setup(stateMachine, combat);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.mediumVioletRed;
        Gizmos.DrawWireSphere(transform.position, combat.attackRange);
        Gizmos.color = Color.navajoWhite;
        Gizmos.DrawWireSphere(transform.position, patrol.detectionRange);
    }
}
