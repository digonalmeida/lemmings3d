using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingActions : MonoBehaviour {

    public float timeToDetroyAfterEnterExitPoint = 2f;
    private HighlightableObject highlightableScript;
    private LemmingStateController lemmingAIScript;

    private void Awake()
    {
        highlightableScript = GetComponent<HighlightableObject>();
        lemmingAIScript = GetComponent<LemmingStateController>();
    }

    public void EnterExitPoint()
    {
        // turn off highlightable  object
        highlightableScript.canBeHighlighted = false;

        // trigger event
        GameEvents.Lemmings.LemmingReachedExit.SafeInvoke(lemmingAIScript);
    }


    //Goodbye Cruel World
    public void KillLemming()
    {
        GameEvents.Lemmings.LemmingDied.SafeInvoke(lemmingAIScript);
        EliminateLemming();
    }

    public void EliminateLemming()
    {
        Destroy(gameObject);
    }

}
