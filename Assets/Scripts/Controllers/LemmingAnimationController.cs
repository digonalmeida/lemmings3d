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

    //Set Bool
    public void setBool(string variable, bool value)
    {
        lemmingAnimator.SetBool(variable, value);
    }

    //Set Int
    public void setInt(string variable, int value)
    {
        lemmingAnimator.SetInteger(variable, value);
    }

    //Set Trigger
    public void setTrigger(string variable, bool active = true)
    {
        if (active) lemmingAnimator.SetTrigger(variable);
        else lemmingAnimator.ResetTrigger(variable);
    }
}
