using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Skills
{
    DrecreaseRate,
    IncreaseRate,
    Turn,
    Parachute
}

public class UIDockInfo_Skill : UIDockInfo
{

    public Skills skill;
    public bool skillEnabled;
    public int skillCount;
    public Text skillCountText;
    public Button skillButton;

    public override void UpdateInfo()
    {

        skillCountText.text = skillCount.ToString();

        skillButton.interactable = !(!skillEnabled || skillCount <= 0);
            
        

    }

}
