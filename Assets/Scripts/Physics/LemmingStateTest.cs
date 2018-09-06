using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LemmingStateController))]
public class LemmingStateTest : MonoBehaviour
{
    public Skill[] skillsToSet;

	// Use this for initialization
	void OnEnable()
    {
        for(int i = 0; i < skillsToSet.Length; i++)
        {
            this.GetComponent<LemmingStateController>().setSkill(skillsToSet[i], true);
        }
    }
}
