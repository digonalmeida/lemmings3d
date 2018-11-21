using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterStartAnimationTrigger : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LemmingAnimationController lemmingAnimationController = animator.gameObject.GetComponent<LemmingAnimationController>();
        lemmingAnimationController.setActiveUmbrella(true);
        lemmingAnimationController.setUmbrellaAnimation(true);
    }
}
