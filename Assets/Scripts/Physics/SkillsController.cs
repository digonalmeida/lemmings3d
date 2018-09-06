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

public class SkillsController : MonoBehaviour
{
    public int remainingBashers;
    public int remainingBlockers;
    public int remainingBuilders;
    public int remainingClimbers;
    public int remainingDiggers;
    public int remainingExploders;
    public int remainingFloaters;

    //Assign Skills
    public void assignSkill(LemmingStateController lemming, Skill skill)
    {
        switch(skill)
        {
            case Skill.Basher:
            {
                if (remainingBashers > 0)
                {
                    lemming.setSkill(Skill.Basher, true);
                    remainingBashers--;
                }
                break;
            }    
            case Skill.Blocker_TurnNorth:
            {
                if (remainingBlockers > 0)
                {
                    lemming.setSkill(Skill.Blocker_TurnNorth, true);
                    remainingBlockers--;
                }
                break;
            }
            case Skill.Blocker_TurnEast:
            {
                if (remainingBlockers > 0)
                {
                    lemming.setSkill(Skill.Blocker_TurnEast, true);
                    remainingBlockers--;
                }
                break;
            }
            case Skill.Blocker_TurnSouth:
            {
                if (remainingBlockers > 0)
                {
                    lemming.setSkill(Skill.Blocker_TurnSouth, true);
                    remainingBlockers--;
                }
                break;
            }
            case Skill.Blocker_TurnWest:
            {
                if (remainingBlockers > 0)
                {
                    lemming.setSkill(Skill.Blocker_TurnWest, true);
                    remainingBlockers--;
                }
                break;
            }
            case Skill.Builder:
            {
                if (remainingBuilders > 0)
                {
                    lemming.setSkill(Skill.Builder, true);
                        remainingBuilders--;
                }
                break;
            }
            case Skill.Climber:
            {
                if (remainingClimbers > 0)
                {
                    lemming.setSkill(Skill.Climber, true);
                    remainingClimbers--;
                }
                break;
            }
            case Skill.Digger:
            {
                if (remainingDiggers > 0)
                {
                    lemming.setSkill(Skill.Digger, true);
                    remainingDiggers--;
                }
                break;
            }
            case Skill.Exploder:
            {
                if (remainingExploders > 0)
                {
                    lemming.setSkill(Skill.Exploder, true);
                    remainingExploders--;
                }
                break;
            }
            case Skill.Floater:
            {
                if (remainingFloaters > 0)
                {
                    lemming.setSkill(Skill.Floater, true);
                    remainingFloaters--;
                }
                break;
            }
        }
    }
}
