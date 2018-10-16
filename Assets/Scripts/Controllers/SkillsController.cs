using System.Collections;
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

public class SkillsController : MonoBehaviour
{
    [SerializeField]
    SkillsCounter skillsCounter;
    
    public Skill selectedSkill;

    public BlockerDirectionSelector blockerSelectorPrefab;
    private BlockerDirectionSelector blockerSelector;
    public bool isWaitingForBlockerConfirmation { get; private set; }

    public void Awake()
    {
        MapController.OnLoadMap += OnLoadMap;
    }

    public void OnDestroy()
    {
        MapController.OnLoadMap -= OnLoadMap;
    }

    public void OnLoadMap(MapSettings settings)
    {
        skillsCounter = new SkillsCounter(settings.SkillsCounter);
        LevelController.TriggerLemmingUsedSkill();
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
        if(skillsCounter[selectedSkill] <= 0)
        {
            return false;
        }

        if (selectedSkill == Skill.Blocker)
        {
            blockerSelector.AttachToSomeone(lemming);
            isWaitingForBlockerConfirmation = true;
            return false;
        }

        if(!lemming.giveSkill(selectedSkill))
        {
            return false;
        }

        skillsCounter[selectedSkill]--;

        LevelController.TriggerLemmingUsedSkill();

        selectedSkill = Skill.None;

        return true;
    }

    public void CancelBlocker()
    {
        blockerSelector.Detach();
    }
}
