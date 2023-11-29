using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This powerup reverses the direction of the ball  */
public class ReversalPowerup : Powerup
{
    
    protected override void OnHit(Transform ball)
    {
        base.OnHit(ball);

        BallMovement ballMovement = ball.GetComponent<BallMovement>();
        
        ballMovement.ReverseXDirection();

        Delete();
    }
}
