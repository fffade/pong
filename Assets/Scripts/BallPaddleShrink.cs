using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallPaddleShrink : MonoBehaviour
{
    /* When the ball is 'paddle shrink activated', the next paddle it hits will shrink in size */

    public bool IsActive = false;
    
    // How much to shrink targeted paddles
    [SerializeField] private float shrinkMultiplier;

    // Triggered when the ball hits, checks for activation
    public void Check(Transform paddle)
    {
        if (!IsActive)
            return;

        Use(paddle);
    }

    private void Use(Transform paddle)
    {
        // Shrinks the hit paddle
        PaddleSize paddleSize = paddle.GetComponent<PaddleSize>();
        
        paddleSize.MultiplyModifier(shrinkMultiplier);
        
        // Single use before deactivating
        IsActive = false;
    }
}
