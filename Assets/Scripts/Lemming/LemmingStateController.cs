using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LemmingStateController : NetworkBehaviour
{
    //Permanent Skills
    private bool floater;
    private bool climber;
    private bool forceExplode;

    //Synced Variables
    [SyncVar, SerializeField]
    private Player team = Player.None;
   
    //Queue Skills
    private Queue<Skill> queuedSkills;

    //Control Variables
    private GameObject actionObject;
    public GameObject lemmingModel;

    public Direction BlockingDirection
    {
        get
        {
            if (checkSkill(Skill.Blocker_TurnNorth))
            {
                return Direction.North;
            }
            else if (checkSkill(Skill.Blocker_TurnEast))
            {
                return Direction.East;
            }
            else if (checkSkill(Skill.Blocker_TurnSouth))
            {
                return Direction.South;
            }
            else if (checkSkill(Skill.Blocker_TurnWest))
            {
                return Direction.West;
            }
            else
            {
                return Direction.North;
            }
        }
    }

    public Player Team
    {
        get
        {
            return team;
        }
        set
        {
            team = value;
        }
    }

    //Start Method
    private void Start()
    {
        //Initialize Variables
        floater = false;
        climber = false;
        forceExplode = false;
        queuedSkills = new Queue<Skill>();
        actionObject = this.transform.GetChild(0).gameObject;
    }

    //Set a certain skill for Lemming
    public bool giveSkill(Skill skill)
    {
        if (skill == Skill.Climber)
        {
            if (climber)
            {
                return false;
            }

            climber = true;
        }
        else if (skill == Skill.Floater)
        {
            if (floater)
            {
                return false;
            }

            floater = true;
        }
        else
        {
            queuedSkills.Enqueue(skill);
        }

        return true;
    }

    public bool checkIsBlocker()
    {
        return checkSkill(Skill.Blocker_TurnNorth) ||
            checkSkill(Skill.Blocker_TurnEast) ||
            checkSkill(Skill.Blocker_TurnSouth) ||
            checkSkill(Skill.Blocker_TurnWest);
    }


    //Activate Lemming Action Trigger
    public void setLemmingActionTrigger(bool value)
    {
        actionObject.SetActive(value);
    }

    [ClientRpc]
    public void RpcInformTeam()
    {
        setTeam(team);
    }

    //Set Team
    private void setTeam(Player team)
    {
        this.team = team;
        if(team == Player.Player1 && LNetworkLobbyPlayer.Player1Instance != null)
        {
            lemmingModel.GetComponentInChildren<SkinnedMeshRenderer>().materials[1].color = LNetworkLobbyPlayer.Player1Instance.playerClothColor;
            lemmingModel.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].color = LNetworkLobbyPlayer.Player1Instance.playerHairColor;
        }
        else if (team == Player.Player2 && LNetworkLobbyPlayer.Player2Instance != null)
        {
            lemmingModel.GetComponentInChildren<SkinnedMeshRenderer>().materials[1].color = LNetworkLobbyPlayer.Player2Instance.playerClothColor;
            lemmingModel.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].color = LNetworkLobbyPlayer.Player2Instance.playerHairColor;
        }
    }

    public void setForceExplode(bool _forceExplode)
    {
        forceExplode = _forceExplode;
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
    
    public bool checkForceExplode()
    {
        return forceExplode;
    }

    public bool checkSkill(Skill skill)
    {
        return checkEnqueuedSkill() == skill;
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
