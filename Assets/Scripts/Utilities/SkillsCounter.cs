using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillsCounter
{
    [SerializeField]
    public int bashers = 0;

    [SerializeField]
    public int blockers = 0;

    [SerializeField]
    public int builders = 0;

    [SerializeField]
    public int climbers = 0;

    [SerializeField]
    public int diggers = 0;

    [SerializeField]
    public int exploders = 0;

    [SerializeField]
    public int floaters = 0;

    public SkillsCounter()
    {

    }

    public SkillsCounter(SkillsCounter other)
    {
        bashers = other.bashers;
        blockers = other.blockers;
        builders = other.builders;
        climbers = other.climbers;
        diggers = other.diggers;
        exploders = other.exploders;
        floaters = other.floaters;
    }

    public int this[SkillType skillType]
    {
        get
        {
            return Get(skillType);
        }
        set
        {
            Set(skillType, value);
        }
    }

    public int this[Skill skill]
    {
        get
        {
            return Get(skill);
        }
        set
        {
            Set(skill, value);
        }
    }

    public void Set(SkillType skillType, int value)
    {
        switch (skillType)
        {
            case SkillType.None:
                break;
            case SkillType.Basher:
                bashers = value;
                break;
            case SkillType.Blocker:
                blockers = value;
                break;
            case SkillType.Builder:
                builders = value;
                break;
            case SkillType.Climber:
                climbers = value;
                break;
            case SkillType.Digger:
                diggers = value;
                break;
            case SkillType.Exploder:
                exploders = value;
                break;
            case SkillType.Floater:
                floaters = value;
                break;
            default:
                break;
        }
    }

    public int Get(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.None:
                return 0;
            case SkillType.Basher:
                return bashers;
            case SkillType.Blocker:
                return blockers;
            case SkillType.Builder:
                return builders;
            case SkillType.Climber:
                return climbers;
            case SkillType.Digger:
                return diggers;
            case SkillType.Exploder:
                return exploders;
            case SkillType.Floater:
                return floaters;
            default:
                return 0;
        }
    }

    public int Get(Skill skill)
    {
        return Get(GetType(skill));
    }

    public void Set(Skill skill, int value)
    {
        Set(GetType(skill), value);
    }

    public enum SkillType
    {
        None,
        Basher,
        Blocker,
        Builder,
        Climber,
        Digger,
        Exploder,
        Floater
    }

    public SkillType GetType(Skill skill)
    {
        switch (skill)
        {
            case Skill.Basher:
                return SkillType.Basher;
            case Skill.Builder:
                return SkillType.Builder;
            case Skill.Climber:
                return SkillType.Climber;
            case Skill.Digger:
                return SkillType.Digger;
            case Skill.Exploder:
                return SkillType.Exploder;
            case Skill.Floater:
                return SkillType.Floater;

            case Skill.Blocker_TurnNorth:
            case Skill.Blocker_TurnEast:
            case Skill.Blocker_TurnSouth:
            case Skill.Blocker_TurnWest:
            case Skill.Blocker:
                return SkillType.Blocker;

            case Skill.None:
            default:
                return SkillType.None;
        }
    }
}
