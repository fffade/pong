using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Activating ball fire increases the ball's speed with a burst and then gradually over time */
public class BallFire : MonoBehaviour
{
    [SerializeField]private Animator animator;
    
    // If the ball is currently on fire
    public bool IsActive { get; private set; } = false;
    
    // The current speed modifier as of right now
    public float BallSpeedModifier { get; private set; } = 1f;
    
    // How much speed the ball gains when first achieving fire
    [SerializeField] private float burstSpeed;
    
    // How much speed over time the ball gains while on fire
    [SerializeField] private float gradualSpeedIncrease;
    
    // How often speed is gained while on fire
    private float _gradualSpeedTimer = 0f;
    [SerializeField] private float gradualSpeedRate;
    
    // The extra sprite to turn on when on fire
    [SerializeField] private SpriteRenderer fireSpriteRenderer;



    void Update()
    {
        if (IsActive)
        {
            // Update gradual speed while on fire
            // = speed over time
            _gradualSpeedTimer += Time.deltaTime;

            if (_gradualSpeedTimer >= gradualSpeedRate)
            {
                BallSpeedModifier += gradualSpeedIncrease;

                _gradualSpeedTimer = 0f;
            }
        }

        fireSpriteRenderer.enabled = IsActive;
        
        // Ball being on fire uses an animation + new sprite
        animator.SetBool("IsOnFire", IsActive);
    }
    
    // Activates fire when its off
    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        BallSpeedModifier += burstSpeed;

        _gradualSpeedTimer = 0f;
    }
    
    // Resets fire completely, no more speed
    public void ResetFire()
    {
        BallSpeedModifier = 1f;

        IsActive = false;
    }
}
