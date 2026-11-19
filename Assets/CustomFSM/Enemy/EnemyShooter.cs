using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header ("STATES")]
    [SerializeField] private PatrolState patrol;
    [SerializeField] private CombatState combat;
    [SerializeField] private AlertState alert;
    [SerializeField] private FindCoverState findCover;

    private StateMachine stateMachine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Crear y guardar una máquina de estados
        stateMachine = new StateMachine();

        //Empezar en un estado
        stateMachine.ChangeState(patrol);

        //Configurar las transiciones entre estados
        //PATROL --> ALERT
        stateMachine.AddTransition(patrol, alert, TransitionType.PlayerDetected);
        //ALERT --> COMBAT
        stateMachine.AddTransition(alert, combat, TransitionType.PlayerDetected);
        //ALERT --> PATROL
        stateMachine.AddTransition(alert, patrol, TransitionType.PlayerLost);

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
        Gizmos.color = Color.dodgerBlue;
        Gizmos.DrawWireSphere(transform.position, alert.hearingRange);
    }
}
