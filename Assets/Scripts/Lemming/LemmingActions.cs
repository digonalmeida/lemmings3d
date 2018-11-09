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

        // trigger animation


        // trigger effects


        // update level lemming counter
        //TODO

        // destroy
        (this).WaitAndAct(timeToDetroyAfterEnterExitPoint, () => EliminateLemming());
    }

    private void EliminateLemming()
    {
        CountExit();

        Destroy(gameObject);
    }

    private void CountExit()
    {
        GameEvents.Lemmings.LemmingReachedExit.SafeInvoke();
    }
}
