using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbBehaviour : LemmingBehaviour
{
    private bool climbing = false;
    public override void Update(LemmingController lemming)
    {
        if(climbing)
        {
            lemming.GravityVelocity = Vector3.up * lemming.MovementSpeed;
            if (!lemming.footForwardSensor.Check(lemming.transform))
            {
                climbing = false;
            }
            
        }
        else if (lemming.forwardSensor.Check(lemming.transform))
        {
            climbing = true;
        }
        else
        {
            lemming.WanderBehaviour.Update(lemming);
        }
    }
}
