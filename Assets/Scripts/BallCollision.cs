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
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        
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
            _movement.lastHitPaddle = collision.collider.transform;

            // Calculate the contact point from the center of the paddle to determine y velocity bounce off
            Vector2 contactPoint = collision.collider.ClosestPoint(transform.position);

            Vector2 distanceFromCenter = (contactPoint - (Vector2)collision.collider.bounds.center);

            _movement.AdjustYVelocity(Mathf.Abs(distanceFromCenter.y));
            
            // Determine y velocity reverse
            float distanceFromCenterX = Mathf.Abs(distanceFromCenter.x);
            float extentDiff = Mathf.Abs(distanceFromCenterX - collision.collider.bounds.extents.x);
            
            // Check side of paddle hit
            if (extentDiff > 0.05f)
            {
                _movement.ReverseYDirection();
            }
            else
            {
                _movement.ReverseXDirection();
            }
            
            // Check if paddle shrink is on from a picked up powerup
            _paddleShrink.Check(collision.collider.gameObject.transform);
        }
    }
}
