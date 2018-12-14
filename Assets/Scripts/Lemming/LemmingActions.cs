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
        Debug.Log("Action Entered exit point");

        // turn off highlightable  object
        highlightableScript.canBeHighlighted = false;

        // trigger event
        GameEvents.Lemmings.LemmingReachedExit.SafeInvoke(lemmingAIScript);
    }

    //Goodbye Cruel World
    public void KillLemming()
    {
        GameEvents.Lemmings.LemmingDied.SafeInvoke(lemmingAIScript);
    }

    //Boom!
    public void KillLemmingByExplosion()
    {
        GameEvents.Lemmings.LemmingExploded.SafeInvoke(lemmingAIScript);
        KillLemming();
    }

    public void EliminateLemming()
    {
        Destroy(gameObject);
    }

}
