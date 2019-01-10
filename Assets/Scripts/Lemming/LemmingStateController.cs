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
    private bool isBlocker = false;

    //Synced Variables
    [SyncVar, SerializeField]
    private Player team = Player.None;

    //References
    LemmingMovementController movementController;

    //Queue Skills
    private Queue<Skill> queuedSkills;
    private Skill delayedEnqueuedSkill;

    //Control Variables
    private GameObject actionObject;
    public GameObject lemmingModel;
    public Transform foot;
    [SerializeField]
    private GameObject blockerIndicator = null;

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

    //Awake
    private void Awake()
    {
        movementController = this.GetComponent<LemmingMovementController>();
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
    public bool giveSkill(Skill skill, Vector3 originPosition)
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
            if (checkIsBlocker() && skill == Skill.Exploder) forceExplode = true;
            else
            {
                delayedEnqueuedSkill = skill;
                movementController.forceNewPosition(originPosition);
                movementController.OnArrived += giveEnqueuedSkill;
            }
        }

        return true;
    }

    public void giveEnqueuedSkill()
    {
        this.GetComponent<LemmingMovementController>().OnArrived -= giveEnqueuedSkill;
        queuedSkills.Enqueue(delayedEnqueuedSkill);
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
        if (team == Player.Player1 && LNetworkLobbyPlayer.Player1Instance != null)
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

    //Check for Exit
    public bool CheckExitPoint()
    {
        var block = LevelMap.MapController.Instance.GetBlockAtPosition(Vector3Int.RoundToInt(this.transform.position));
        if (block != null)
        {
            if (block.Type == LevelMap.MapBlock.BlockType.End)
            {
                return true;
            }
        }
        return false;
    }

    //Fixed Update
    private void FixedUpdate()
    {
        if (!isServer)
        {
            if (CheckExitPoint()) HighlightPointer.Instance.clearHighlight(this.GetComponent<HighlightableObject>());
        }
    }

    public void SetBlockerIndicatorActive(bool blockerActive)
    {
        if (blockerIndicator == null)
        {
            return;
        }
        blockerIndicator.SetActive(blockerActive);
    }

}
