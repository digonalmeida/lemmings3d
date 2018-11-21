using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterEndAnimationTrigger : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<LemmingAnimationController>().setUmbrellaAnimation(false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<LemmingAnimationController>().setActiveUmbrella(false);
    }
}
