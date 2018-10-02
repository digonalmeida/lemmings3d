using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingStateController : MonoBehaviour
{
    //Variables
    [SerializeField]
    private bool[] skillsArray;
    private GameObject actionObject;

    //Start Method
    private void Start()
    {
        //Initialize Skills Array
        skillsArray = new bool[10];
        for(int i = 0; i < 10; i++)
        {
            skillsArray[i] = false;
        }

        //Initialize Other Variables
        actionObject = this.transform.GetChild(0).gameObject;
    }

    //Check Lemming for a certain skill
    public bool checkSkill(Skill skill)
    {
        switch(skill)
        {
            case Skill.Basher:
                return skillsArray[0];
            case Skill.Blocker_TurnNorth:
                return skillsArray[1];
            case Skill.Blocker_TurnEast:
                return skillsArray[2];
            case Skill.Blocker_TurnSouth:
                return skillsArray[3];
            case Skill.Blocker_TurnWest:
                return skillsArray[4];
            case Skill.Builder:
                return skillsArray[5];
            case Skill.Climber:
                return skillsArray[6];
            case Skill.Digger:
                return skillsArray[7];
            case Skill.Exploder:
                return skillsArray[8];
            case Skill.Floater:
                return skillsArray[9];
            default: return false;
        }
    }

    //Set a certain skill for Lemming
    public void setSkill(Skill skill, bool value)
    {
        switch (skill)
        {
            case Skill.Basher:
                skillsArray[0] = value;
                break;
            case Skill.Blocker_TurnNorth:
                skillsArray[1] = value;
                this.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case Skill.Blocker_TurnEast:
                skillsArray[2] = value;
                this.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case Skill.Blocker_TurnSouth:
                skillsArray[3] = value;
                this.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case Skill.Blocker_TurnWest:
                skillsArray[4] = value;
                this.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case Skill.Builder:
                skillsArray[5] = value;
                break;
            case Skill.Climber:
                skillsArray[6] = value;
                break;
            case Skill.Digger:
                skillsArray[7] = value;
                break;
            case Skill.Exploder:
                skillsArray[8] = value;
                break;
            case Skill.Floater:
                skillsArray[9] = value;
                break;
        }
    }

    //Check Movement Blockers
    public bool checkMovementBlockingSkills()
    {
        return skillsArray[1] || skillsArray[2] || skillsArray[3] || skillsArray[4] || skillsArray[8];
    }

    public bool checkIsBlocker()
    {
        return skillsArray[(int)Skill.Blocker_TurnNorth] ||
            skillsArray[(int)Skill.Blocker_TurnEast] ||
            skillsArray[(int)Skill.Blocker_TurnSouth] ||
            skillsArray[(int)Skill.Blocker_TurnWest];
    }

    public Direction BlockingDirection
    {
        get
        {
            if(skillsArray[(int)Skill.Blocker_TurnNorth])
            {
                return Direction.North;
            }
            else if (skillsArray[(int)Skill.Blocker_TurnEast])
            {
                return Direction.East;
            }
            else if (skillsArray[(int)Skill.Blocker_TurnSouth])
            {
                return Direction.South;
            }
            else if (skillsArray[(int)Skill.Blocker_TurnWest])
            {
                return Direction.West;
            }
            else
            {
                return Direction.North;
            }
        }
    }
}
