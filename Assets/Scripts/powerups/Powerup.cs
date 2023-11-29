using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // The reference name for this powerup
    [SerializeField] public string name;
    
    // The spawner used to spawn this powerup
    public PowerupSpawning spawner;
    
    /* Trigger event when collided with by ball */
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Ensure ball collision
        if (!collider.gameObject.CompareTag("Ball"))
            return;
        
        OnHit(collider.gameObject.transform);
    }
    
    /* Occurs when this powerup is hit */
    // Method is used by subclasses
    protected virtual void OnHit(Transform ball)
    {
        Debug.Log($"Powerup '{name}' hit!");
    }

    // Remove this powerup from the world
    protected void Delete()
    {
        spawner.DeletePowerup(gameObject);
    }
}
