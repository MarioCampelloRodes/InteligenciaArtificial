using UnityEngine;

public class AB_RunAway : StateMachineBehaviour
{
    private AnimalNPC npc;

    private Vector3 destination;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<AnimalNPC>();

        //Probabilidad de que se cague si ve un peligro
        if(Random.Range(1, 11) <= 2)
        {
            animator.SetBool("IsMecago", true);
            return;
        }

        destination = GetRunawayPosition(animator);
        //Huir en cuanto entra en este estado
        npc.agent.SetDestination(destination);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = destination - animator.transform.position;
        direction.y = 0;

        if ((direction).sqrMagnitude <= npc.agent.stoppingDistance + 0.25f)
        {
            if(npc.DetectDanger() == null)
            {
                animator.SetBool("IsIdle", true);
            }
            else
            {
                destination = GetRunawayPosition(animator);
                npc.agent.SetDestination(destination);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsRunningAway", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    Vector3 GetRunawayPosition(Animator anim)
    {
        //Calcula la dirección contraria a la que se encuentra el peligro
        Vector3 direction = anim.transform.position - npc.DetectDanger().position;

        //Calcular rotación aleatoria
        int randomRotation = Random.Range(0, Mathf.RoundToInt(npc.runAwayAngle * 0.5f));

        //Aplicar una rotación a la dirección de huida. SOLO funciona en este orden: Quaternion * Vector
        direction = Quaternion.Euler(0, randomRotation, 0) * direction.normalized;

        return anim.transform.position + direction.normalized * npc.runAwayDistante;
    }
}
