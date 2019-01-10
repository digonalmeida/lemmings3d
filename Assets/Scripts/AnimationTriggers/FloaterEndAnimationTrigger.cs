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

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1f) animator.GetComponent<LemmingAnimationController>().finishedAnimationAction.SafeInvoke();
    }
}
