using UnityEngine;

/* This powerup turns on the ball's paddle shrink effect */
public class PaddleShrinkPowerup : Powerup
{
    
    protected override void OnHit(Transform ball)
    {
        base.OnHit(ball);

        BallPaddleShrink ballPaddleShrink = ball.GetComponent<BallPaddleShrink>();
        
        // Turns on the paddle shrink effect, next paddle hit will shrink
        ballPaddleShrink.IsActive = true;

        Delete();
    }
}
