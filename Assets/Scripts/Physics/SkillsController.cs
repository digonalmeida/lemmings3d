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
    //Control Variables
    public int remainingBashers;
    public int remainingBlockers;
    public int remainingBuilders;
    public int remainingClimbers;
    public int remainingDiggers;
    public int remainingExploders;
    public int remainingFloaters;
    public Skill selectedSkill;

    //Singleton Instance Variable
    private static SkillsController instance;
    public static SkillsController Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //Start
    public void Start()
    {
        selectedSkill = Skill.None;
    }

    //Get Remaining Uses
    public int getRemainingUses(Skill skill)
    {
        switch (skill)
        {
            case Skill.Basher:
                return remainingBashers;
            case Skill.Blocker_TurnEast:
                return remainingBlockers;
            case Skill.Blocker_TurnNorth:
                return remainingBlockers;
            case Skill.Blocker_TurnSouth:
                return remainingBlockers;
            case Skill.Blocker_TurnWest:
                return remainingBlockers;
            case Skill.Builder:
                return remainingBuilders;
            case Skill.Climber:
                return remainingClimbers;
            case Skill.Digger:
                return remainingDiggers;
            case Skill.Exploder:
                return remainingExploders;
            case Skill.Floater:
                return remainingFloaters;
            default:
                return 0;
        }
    }

    //Assign Skills
    public bool assignSkill(LemmingStateController lemming)
    {
        switch(selectedSkill)
        {
            case Skill.Basher:
            {
                if (remainingBashers > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Basher, true);
                        remainingBashers--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Blocker_TurnNorth:
            {
                if (remainingBlockers > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Blocker_TurnNorth, true);
                        remainingBlockers--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Blocker_TurnEast:
            {
                if (remainingBlockers > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Blocker_TurnEast, true);
                        remainingBlockers--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Blocker_TurnSouth:
            {
                if (remainingBlockers > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Blocker_TurnSouth, true);
                        remainingBlockers--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Blocker_TurnWest:
            {
                if (remainingBlockers > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Blocker_TurnWest, true);
                        remainingBlockers--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Builder:
            {
                if (remainingBuilders > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Builder, true);
                        remainingBuilders--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Climber:
            {
                if (remainingClimbers > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Climber, true);
                        remainingClimbers--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Digger:
            {
                if (remainingDiggers > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Digger, true);
                        remainingDiggers--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Exploder:
            {
                if (remainingExploders > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Exploder, true);
                        remainingExploders--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            case Skill.Floater:
            {
                if (remainingFloaters > 0)
                {
                    if (!lemming.checkSkill(selectedSkill))
                    {
                        lemming.setSkill(Skill.Floater, true);
                        remainingFloaters--;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            default: return false;
        }

        //Finally...
        selectedSkill = Skill.None;
    }
}
