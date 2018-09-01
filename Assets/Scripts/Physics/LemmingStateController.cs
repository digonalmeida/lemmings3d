using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill
{
    None,
    Basher,             //Horizontal Dig
    Blocker_TurnNorth,  //Change Direction (North)
    Blocker_TurnEast,   //Change Direction (East)
    Blocker_TurnSouth,   //Change Direction (South)
    Blocker_TurnWest,   //Change Direction (West)
    Builder,            //Stairs
    Climber,            //Climb Walls
    Digger,             //Vertical Dig
    Exploder,           //Self-Destruct
    Floater             //Umbrella
    //Miner             //Diagonal Dig
}

public class LemmingStateController : MonoBehaviour
{
    //Variables
    private bool[] skillsArray;

    //Start Method
    private void Start()
    {
        //Initialize Skills Array
        skillsArray = new bool[10];
        for(int i = 0; i < 10; i++)
        {
            skillsArray[i] = false;
        }
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
                break;
            case Skill.Blocker_TurnEast:
                skillsArray[2] = value;
                break;
            case Skill.Blocker_TurnSouth:
                skillsArray[3] = value;
                break;
            case Skill.Blocker_TurnWest:
                skillsArray[4] = value;
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
}
