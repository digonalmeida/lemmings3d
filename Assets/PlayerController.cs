using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum Behaviours
    {
        Wander,
        Climb
    }

    Behaviours currentBehaviour = Behaviours.Climb;

	private void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var layerMask = LayerMask.GetMask("Lemming");
            var hitInfo = new RaycastHit();
            if(Physics.Raycast(ray, out hitInfo, 1000, layerMask))
            {
                var lemming = hitInfo.collider.GetComponent<LemmingController>();
                if(lemming != null)
                {
                    SetSelectedBehaviourToLemming(lemming);
                }
            }
        }
	}

    private void SetSelectedBehaviourToLemming(LemmingController lemming)
    {
        switch (currentBehaviour)
        {
            case Behaviours.Wander:
                lemming.CurrentBehaviour = lemming.WanderBehaviour;
                break;
            case Behaviours.Climb:
                lemming.CurrentBehaviour = lemming.ClimbBehaviour;
                break;
            default:
                break;
        }
    }
}
