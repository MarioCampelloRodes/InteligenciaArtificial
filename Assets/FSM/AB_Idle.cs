using UnityEngine;

public class AB_Idle : StateMachineBehaviour
{
    private AnimalNPC npc;
    private float timer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Como esto no es Monobehaviour, hay que acceder al script a través del animator.
        npc = animator.GetComponent<AnimalNPC>();

        timer = Random.Range(npc.idleTimeRange.x, npc.idleTimeRange.y);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            animator.SetBool("IsPatrolling", true);
        }
    }

    //Como los triggers de iunaiti en el animator son una mierda, se pueden usar booleanos para más fiabilidad
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.SetBool("IsPatrolling", false);
    //}
}
