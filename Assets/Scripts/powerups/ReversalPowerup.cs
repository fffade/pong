using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This powerup reverses the direction of the ball  */
public class ReversalPowerup : Powerup
{
    
    protected override void OnHit(Transform ball, Transform paddle)
    {
        base.OnHit(ball, paddle);

        BallMovement ballMovement = ball.GetComponent<BallMovement>();
        
        ballMovement.ReverseXDirection();

        Delete();
    }
}
