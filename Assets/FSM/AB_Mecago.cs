using UnityEngine;

public class AB_Mecago : StateMachineBehaviour
{
    private AnimalNPC npc;
    private float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Como esto no es Monobehaviour, hay que acceder al script a través del animator.
        npc = animator.GetComponent<AnimalNPC>();

        timer = Random.Range(npc.poopTimeRange.x, npc.poopTimeRange.y);

        npc.poopPS.gameObject.SetActive(true);
        npc.poopPS.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            animator.SetBool("IsIdle", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.poopPS.Pause();
        npc.poopPS.gameObject.SetActive(false);

        animator.SetBool("IsMecago", false);
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
