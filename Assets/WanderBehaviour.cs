using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : LemmingBehaviour
{
    public override void Update(LemmingController lemming)
    {
        if (lemming.forwardSensor.Check(lemming.transform))
        {
            lemming.transform.forward = -lemming.transform.forward;
        }

        lemming.MovementVelocity = lemming.transform.forward * lemming.MovementSpeed;
    }
}
