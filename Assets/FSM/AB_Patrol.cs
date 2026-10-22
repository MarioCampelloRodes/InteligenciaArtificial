using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AB_Patrol : StateMachineBehaviour
{
    private AnimalNPC npc;

    private Vector3 destination;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<AnimalNPC>();

        destination = npc.GetRandomPosition();
        npc.agent.SetDestination(destination);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = destination - animator.transform.position;
        direction.y = 0;
        if ((direction).sqrMagnitude <= 0.01f)
        {
            ChooseAction(animator);
        }

        //Si detecta algún objeto peligroso en algún momento, pasa al estado de huir
        if (npc.DetectDanger() != null)
        {
            animator.SetBool("IsRunningAway", true);
        }
    }

    void ChooseAction(Animator animator)
    {
        int choice = Random.Range(1, 11);

        if (choice <= 7)
        {
            //Pasar al estado mecago
            animator.SetBool("IsMecago", true);
        }
        else
        {
            //Pasar al estado Idle
            animator.SetBool("IsIdle", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Cuando salga del estado, por la razón que sea, cambia el parámetro bool del estado a false (para que no se quede pillado por estar todo en true)
        animator.SetBool("IsPatrolling", false);
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
}
