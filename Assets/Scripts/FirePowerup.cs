using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This powerup increases the ball's speed by activating 'fire' */
public class FirePowerup : Powerup
{
    
    protected override void OnHit(Transform ball)
    {
        base.OnHit(ball);

        BallFire ballFire = ball.GetComponent<BallFire>();
        
        ballFire.Activate();

        Delete();
    }
}
