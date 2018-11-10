using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingActions : MonoBehaviour {

    public float timeToDetroyAfterEnterExitPoint = 2f;
    private HighlightableObject highlightableScript;

    private void Awake()
    {
        highlightableScript = GetComponent<HighlightableObject>();
    }

    public void EnterExitPoint()
    {
        // turn off highlightable  object
        highlightableScript.canBeHighlighted = false;

        // trigger event
        GameEvents.Lemmings.LemmingReachedExit.SafeInvoke();
    }


    //Goodbye Cruel World
    public void KillLemming()
    {
        Debug.Log(gameObject.name + " - I'm dead");
        GameEvents.Lemmings.LemmingDied.SafeInvoke();

        EliminateLemming();

        //TODO
    }

    public void EliminateLemming()
    {
        Destroy(gameObject);
    }

}
