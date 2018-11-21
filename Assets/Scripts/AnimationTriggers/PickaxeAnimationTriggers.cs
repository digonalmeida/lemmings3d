using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeAnimationTriggers : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LemmingAnimationController lemmingAnimationController = animator.gameObject.GetComponent<LemmingAnimationController>();
        lemmingAnimationController.setActivePickaxe(true);

        if(stateInfo.IsName("DigForward")) lemmingAnimationController.setDigForwardPickaxeAnimation();
        else if (stateInfo.IsName("DigDown")) lemmingAnimationController.setDigDownPickaxeAnimation();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<LemmingAnimationController>().setActivePickaxe(false);
    }
}
