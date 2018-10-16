using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill
{
    None,
    Basher,             //Horizontal Dig
    Blocker,            //Change Direction
    Blocker_TurnNorth,  //Change Direction (North)
    Blocker_TurnEast,   //Change Direction (East)
    Blocker_TurnSouth,  //Change Direction (South)
    Blocker_TurnWest,   //Change Direction (West)
    Builder,            //Stairs
    Climber,            //Climb Walls
    Digger,             //Vertical Dig
    Exploder,           //Self-Destruct
    Floater             //Umbrella
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

    public BlockerDirectionSelector blockerSelectorPrefab;
    private BlockerDirectionSelector blockerSelector;
    public bool isWaitingForBlockerConfirmation { get; private set; }

    //Start
    public void Start()
    {
        selectedSkill = Skill.None;
        blockerSelector = Instantiate(blockerSelectorPrefab);
        isWaitingForBlockerConfirmation = false;
    }

    public void changeSkill(Skill newSkill)
    {
        selectedSkill = newSkill;
        if (isWaitingForBlockerConfirmation)
        {
            blockerSelector.Detach();
            isWaitingForBlockerConfirmation = false;
        }
    }

    //Get Remaining Uses
    public int getRemainingUses(Skill skill)
    {
        switch (skill)
        {
            case Skill.Basher:
                return remainingBashers;
            case Skill.Blocker:
                return remainingBlockers;
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
        bool usedSkill = false;

        switch (selectedSkill)
        {
            case Skill.Basher:
                {
                    if (remainingBashers > 0)
                    {
                        lemming.giveSkill(Skill.Basher);
                        remainingBashers--;
                        usedSkill = true;
                        break;
                    }
                    else return false;
                }
            case Skill.Blocker:
                {
                    if (remainingBlockers > 0)
                    {
                        blockerSelector.AttachToSomeone(lemming);
                        isWaitingForBlockerConfirmation = true;
                        break;
                    }
                    else return false;
                }
            case Skill.Blocker_TurnNorth:
                {
                    if (remainingBlockers > 0)
                    {
                        lemming.giveSkill(Skill.Blocker_TurnNorth);
                        remainingBlockers--;
                        usedSkill = true;
                        isWaitingForBlockerConfirmation = false;
                        break;
                    }
                    else return false;
                }
            case Skill.Blocker_TurnEast:
                {
                    if (remainingBlockers > 0)
                    {
                        lemming.giveSkill(Skill.Blocker_TurnEast);
                        remainingBlockers--;
                        usedSkill = true;
                        isWaitingForBlockerConfirmation = false;
                        break;
                    }
                    else return false;
                }
            case Skill.Blocker_TurnSouth:
                {
                    if (remainingBlockers > 0)
                    {
                        lemming.giveSkill(Skill.Blocker_TurnSouth);
                        remainingBlockers--;
                        usedSkill = true;
                        isWaitingForBlockerConfirmation = false;
                        break;
                    }
                    else return false;
                }
            case Skill.Blocker_TurnWest:
                {
                    if (remainingBlockers > 0)
                    {
                        lemming.giveSkill(Skill.Blocker_TurnWest);
                        remainingBlockers--;
                        usedSkill = true;
                        isWaitingForBlockerConfirmation = false;
                        break;
                    }
                    else return false;
                }
            case Skill.Builder:
                {
                    if (remainingBuilders > 0)
                    {
                        lemming.giveSkill(Skill.Builder);
                        remainingBuilders--;
                        usedSkill = true;
                        break;
                    }
                    else return false;
                }
            case Skill.Climber:
                {
                    if (remainingClimbers > 0)
                    {
                        if (!lemming.isClimber())
                        {
                            lemming.giveSkill(Skill.Climber);
                            remainingClimbers--;
                            usedSkill = true;
                            break;
                        }
                        else return false;
                    }
                    else return false;
                }
            case Skill.Digger:
                {
                    if (remainingDiggers > 0)
                    {
                        lemming.giveSkill(Skill.Digger);
                        remainingDiggers--;
                        usedSkill = true;
                        break;
                    }
                    else return false;
                }
            case Skill.Exploder:
                {
                    if (remainingExploders > 0)
                    {
                        lemming.giveSkill(Skill.Exploder);
                        remainingExploders--;
                        usedSkill = true;
                        break;
                    }
                    else return false;
                }
            case Skill.Floater:
                {
                    if (remainingFloaters > 0)
                    {
                        if (!lemming.isFloater())
                        {
                            lemming.giveSkill(Skill.Floater);
                            remainingFloaters--;
                            usedSkill = true;
                            break;
                        }
                        else return false;
                    }
                    else return false;
                }
        }

        if (usedSkill)
        {
            LevelController.TriggerLemmingUsedSkill();
            return true;
        }

        //Finally...
        selectedSkill = Skill.None;

        return true;
    }

    public void CancelBlocker()
    {
        blockerSelector.Detach();
    }
}
