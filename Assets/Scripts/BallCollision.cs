using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private BallMovement _movement;

    private BallPaddleShrink _paddleShrink;

    // Sound the ball plays when hitting something
    [SerializeField] private AudioClip hitAudio;

    void Awake()
    {
        _movement = GetComponent<BallMovement>();

        _paddleShrink = GetComponent<BallPaddleShrink>();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        
        Debug.Log("Ball collided with: " + tag);
        
        GlobalAudio.Instance.PlayAudio(hitAudio);
        
        // Reverse y velocity on colliding with border
        if (tag == "SideBorder")
        {
            _movement.ReverseXDirection();
        }
        else if (tag == "Border")
        {
            _movement.ReverseYDirection();
        }
        else if (tag == "Paddle")
        {
            // Track hit paddle
            _movement.lastHitPaddle = collider.transform;
            
            _movement.ReverseXDirection();
            
            // Calculate the contact point from the center of the paddle to determine y velocity bounce off
            Vector2 contactPoint = collider.ClosestPoint(transform.position);

            Vector2 distanceFromCenter = (contactPoint - (Vector2)collider.bounds.center);

            _movement.AdjustYVelocity(Mathf.Abs(distanceFromCenter.y));
            
            // Check if paddle shrink is on from a picked up powerup
            _paddleShrink.Check(collider.gameObject.transform);
        }
    }
}
