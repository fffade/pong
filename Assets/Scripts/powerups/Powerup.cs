using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // The reference name for this powerup
    [SerializeField] public string name;
    
    // The sound to play upon powerup pickup
    [SerializeField] private AudioClip hitAudio;
    
    // The spawner used to spawn this powerup
    public PowerupSpawning spawner;

    
    
    /* Trigger event when collided with by ball */
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Ensure ball collision
        if (!collider.gameObject.CompareTag("Ball"))
            return;

        BallMovement ballMovement = collider.gameObject.GetComponent<BallMovement>();
        
        OnHit(collider.gameObject.transform, ballMovement.lastHitPaddle);
    }
    
    /* Occurs when this powerup is hit */
    // Method is used by subclasses
    protected virtual void OnHit(Transform ball, [CanBeNull] Transform paddle)
    {
        // Debug.Log($"Powerup '{name}' hit!");
        GlobalAudio.Instance.PlayAudio(hitAudio);
    }

    // Remove this powerup from the world
    protected void Delete()
    {
        spawner.DeletePowerup(gameObject);
    }
}
