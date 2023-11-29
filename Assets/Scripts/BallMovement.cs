using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private BallFire _ballFire;
    
    
    // How much y direction can be used when hitting with paddle
    [SerializeField] private float yVelocityClamp = 0.5f;
    
    [SerializeField] private float initialSpeed;

    public float CurrentSpeed { get; private set; } 

    // Ball changes speed over time
    [SerializeField] private float acceleration = 0.1f;

    public Vector2 Direction { get; private set; }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _ballFire = GetComponent<BallFire>();
    }

    void Start()
    {
        CurrentSpeed = initialSpeed;
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        CurrentSpeed += acceleration;
        
        Vector2 movement = Direction * (CurrentSpeed * _ballFire.BallSpeedModifier);

        _rigidbody.velocity = movement * Time.fixedDeltaTime;
    }

    // Sets this ball's direction to a random one using an angle from 0-360 randomly generated
    public void RandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);

        Direction = new Vector2(
            Mathf.Cos(Mathf.Deg2Rad * randomAngle),
            Mathf.Sin(Mathf.Deg2Rad * randomAngle)
        );
    }
    
    // Reverses y direction to go up instead of down or vice versa
    public void ReverseYDirection()
    {
        Direction = new Vector2(Direction.x, -Direction.y);
    }
    
    // Reverses x direction to go left instead of right or vice versa
    public void ReverseXDirection()
    {
        Direction = new Vector2(-Direction.x, Direction.y);
    }
    
    // Adjusts the y velocity according to a clamp from 0 to 1 representing the distance from the center of the paddle
    public void AdjustYVelocity(float distance)
    {
        float yVelocity = Mathf.Clamp(distance, 0f, yVelocityClamp) * (Direction.y < 0f ? -1f : 1f);

        float xVelocity = (1f - Mathf.Abs(yVelocity)) * (Direction.x < 0f ? -1f : 1f); // X needs to remain consistent based on y

        Direction = new Vector2(xVelocity, yVelocity);
    }
    
    // Sets movement to zero
    public void StopMovement()
    {
        Direction = Vector2.zero;
        CurrentSpeed = initialSpeed;

        _ballFire.ResetFire();
    }
    
    // Moves ball to a position
    public void MovePosition(Vector2 position)
    {
        _rigidbody.position = position;
    }
}
