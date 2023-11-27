using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private BallMovement _movement;


    void Awake()
    {
        _movement = GetComponent<BallMovement>();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        
        Debug.Log("Ball collided with: " + tag);
        
        // Reverse y velocity on colliding with border
        if (tag == "Border")
        {
            _movement.ReverseYDirection();
        }
        else if (tag == "Paddle")
        {
            _movement.ReverseXDirection();
            
            // Calculate the contact point from the center of the paddle to determine y velocity bounce off
            Vector2 contactPoint = collider.ClosestPoint(transform.position);

            Vector2 distanceFromCenter = (contactPoint - (Vector2)collider.bounds.center);

            _movement.AdjustYVelocity(Mathf.Abs(distanceFromCenter.y));
        }
    }
}
