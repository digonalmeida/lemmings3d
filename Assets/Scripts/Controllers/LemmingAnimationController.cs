using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingAnimationController : MonoBehaviour
{
    //Variables
    public Animator lemmingAnimator;

    //Play Animation
    public void PlayAnimation(string animationName)
    {
        lemmingAnimator.Play(animationName);
    }

    //Check End of Animation
    public bool isEndOfAnimation(string animationName)
    {
        if (lemmingAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            return lemmingAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
        }
        else return false;
    }
}
