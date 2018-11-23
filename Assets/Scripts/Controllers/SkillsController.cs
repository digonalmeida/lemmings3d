﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelMap;

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

public class SkillsController : Singleton<SkillsController>
{
    [SerializeField]
    SkillsCounter skillsCounter;

    public Skill selectedSkill;

    public BlockerDirectionSelector blockerSelectorPrefab;
    public BlockerDirectionSelector blockerSelector { get; private set; }
    public bool isWaitingForBlockerConfirmation { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        MapController.OnLoadMap += OnLoadMap;
    }

    public void OnDestroy()
    {
        MapController.OnLoadMap -= OnLoadMap;
    }

    public void OnLoadMap(MapSettings settings)
    {
        skillsCounter = new SkillsCounter(settings.SkillsCounter);
        //LevelController.TriggerLemmingUsedSkill();
    }

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
        return skillsCounter[skill];
    }

    //Assign Skills
    public bool assignSkill(LemmingStateController lemming)
    {
        if (skillsCounter[selectedSkill] <= 0)
        {
            return false;
        }

        if (selectedSkill == Skill.Blocker)
        {
            blockerSelector.AttachToSomeone(lemming);
            isWaitingForBlockerConfirmation = true;
            return false;
        }

        if (!lemming.giveSkill(selectedSkill))
        {
            return false;
        }

        skillsCounter[selectedSkill]--;
        selectedSkill = Skill.None;
        GameEvents.Lemmings.LemmingUsedSkill.SafeInvoke();

        return true;
    }

    public void cancelSkill()
    {

        if (selectedSkill == Skill.Blocker)
        {
            blockerSelector.Detach();
        }
        selectedSkill = Skill.None;
        GameEvents.UI.DeselectedSkill.SafeInvoke();
    }

    public void explodeAll()
    {

        // explode every lemming
        foreach (var lemming in LevelController.Instance.lemmingsOnScene)
        {
            lemming.GetComponent<LemmingStateController>().giveSkill(Skill.Exploder);

        }

        // end game
        LevelController.Instance.EndGame();

    }

}
