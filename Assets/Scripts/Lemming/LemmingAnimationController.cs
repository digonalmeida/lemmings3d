﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LemmingAnimationController : MonoBehaviour
{
    //Variables
    private Animator lemmingAnimator;
    private NetworkAnimator lemmingNetworkAnimator;
    public GameObject pickaxeObject;
    public GameObject umbrellaObject;

    //Action
    public Action finishedAnimationAction;

    private void Awake()
    {
        lemmingAnimator = this.GetComponent<Animator>();
        lemmingNetworkAnimator = this.GetComponent<NetworkAnimator>();
    }

    //Play Animation
    public void PlayAnimation(string animationName)
    {
        lemmingAnimator.Play(animationName);
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
    public void setTrigger(string variable)
    {
        lemmingNetworkAnimator.SetTrigger(variable);
    }

    //Enable/Disable Pickaxe
    public void setActivePickaxe(bool active)
    {
        pickaxeObject.SetActive(active);
    }

    //Enable/Disable Umbrella
    public void setActiveUmbrella(bool active)
    {
        umbrellaObject.SetActive(active);
    }

    //Set Umbrella Animation
    public void setUmbrellaAnimation(bool value)
    {
        umbrellaObject.GetComponent<Animator>().SetBool("Falling", value);
    }

    //Set Pickaxe Animation (Dig Forward)
    public void setDigForwardPickaxeAnimation()
    {
        pickaxeObject.GetComponent<Animator>().SetTrigger("DigForward");
    }

    //Set Pickaxe Animation (Dig Down)
    public void setDigDownPickaxeAnimation()
    {
        pickaxeObject.GetComponent<Animator>().SetTrigger("DigDown");
    }

    //Check if Certain Animation is Still Playing
    public bool checkCurrentAnimation(string animationName)
    {
        return lemmingAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}
