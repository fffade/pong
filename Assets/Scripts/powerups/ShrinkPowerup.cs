using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This powerup decreases the size of the ball by a small amount */
public class ShrinkPowerup : Powerup
{
    // How much to multiply the ball's radius by
    [SerializeField] private float shrinkMultiplier;
    
    protected override void OnHit(Transform ball)
    {
        base.OnHit(ball);

        BallSize ballSize = ball.GetComponent<BallSize>();
        
        ballSize.MultiplyModifier(shrinkMultiplier);

        Delete();
    }
}
