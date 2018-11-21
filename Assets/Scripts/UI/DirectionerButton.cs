using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DirectionerButton : Button
{

    BaseEventData m_evendData;

    public bool isHighlighted { get { return IsHighlighted(m_evendData); } }
}
