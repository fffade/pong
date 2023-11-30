using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This powerup gives the shooter paddle an extra forcefield */
public class ForcefieldPowerup : Powerup
{
    // How many uses to give the player
    [SerializeField] private int forcefieldUseIncrement;
    
    
    protected override void OnHit(Transform ball, Transform paddle)
    {
        base.OnHit(ball, paddle);
        
        // Following code only works if a last hit paddle exists
        // e.g. not when the ball hits a powerup right after round start
        if (paddle == null)
            return;

        PaddleForcefield paddleForcefield = paddle.GetComponent<PaddleForcefield>();

        paddleForcefield.uses += forcefieldUseIncrement;

        Delete();
    }
}
