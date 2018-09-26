using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingStateController : MonoBehaviour
{
    //Permanent Skills
    private bool floater;
    private bool climber;

    //Queue Skills
    private Queue<Skill> queuedSkills;

    //Control Variables
    private GameObject actionObject;

    //Start Method
    private void Start()
    {
        //Initialize Variables
        floater = false;
        climber = false;
        queuedSkills = new Queue<Skill>();
        actionObject = this.transform.GetChild(0).gameObject;
    }

    //Set a certain skill for Lemming
    public void giveSkill(Skill skill)
    {
        //Check Permanent Skills
        if (skill == Skill.Climber) climber = true;
        else if (skill == Skill.Floater) floater = true;
        else queuedSkills.Enqueue(skill); //Other Skills
    }

    //Activate Lemming Action Trigger
    public void setLemmingActionTrigger(bool value)
    {
        this.transform.GetChild(0).gameObject.SetActive(value);
    }

    //Check Permanent Skills (Floater)
    public bool isFloater()
    {
        return floater;
    }

    //Check Permanent Skills (Climber)
    public bool isClimber()
    {
        return climber;
    }

    //Check current skill
    public Skill checkEnqueuedSkill()
    {
        if (queuedSkills.Count == 0) return Skill.None;
        else return queuedSkills.Peek();
    }

    //Dequeue Skill
    public void dequeueSkill()
    {
        queuedSkills.Dequeue();
    }
}
