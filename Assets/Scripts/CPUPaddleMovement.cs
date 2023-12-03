using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUPaddleMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    
    // How far the ball needs to be (y coords) before adjusting position
    [SerializeField] private float ballDistanceThreshold;

    // The ball to reference when moving
    [SerializeField] private Transform ball;

    // How fast the paddle can move
    [SerializeField] private float speed;


    public bool IsMovingUp { get; private set; }
    public bool IsMovingDown { get; private set; }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        DetermineMovement();
        UpdateMovement();
    }

    private void DetermineMovement()
    {
        float ballDistance = Mathf.Abs(ball.position.y - _rigidbody.position.y);
        
        IsMovingUp = ballDistance > ballDistanceThreshold && ball.position.y > _rigidbody.position.y;
        IsMovingDown = ballDistance > ballDistanceThreshold && ball.position.y < _rigidbody.position.y;
        
        // If the ball is BEHIND this paddle, then stop moving altogether
        // This FIXES a bug where the ball will get stuck against the wall by the CPU
        if (ball.position.x >= _rigidbody.position.x)
        {
            IsMovingUp = false;
            IsMovingDown = false;
        }
    }
    
    private void UpdateMovement()
    {
        if (IsMovingUp)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * (speed * Time.fixedDeltaTime));
        }

        if (IsMovingDown)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * (speed * Time.fixedDeltaTime));
        }
    }
}
