using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

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
    
    // The last paddle this ball was hit by
    public Transform lastHitPaddle;
    
    // Distance moved since last stop
    private float _distanceTraveledTimer;
    public float DistanceTraveled { get; private set; }
    public Vector2 LastPosition { get; private set; }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _ballFire = GetComponent<BallFire>();
    }

    void Start()
    {
        CurrentSpeed = initialSpeed;
    }

    void Update()
    {
        // Track distance traveled
        DistanceTraveled += (LastPosition - _rigidbody.position).magnitude;

        _distanceTraveledTimer += Time.deltaTime;

        if (_distanceTraveledTimer >= 3f)
        {
            _distanceTraveledTimer = 0f;
            DistanceTraveled = 0f;
        }
        else
        {
            CheckStuckError();
        }

    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void LateUpdate()
    {
        LastPosition = _rigidbody.position;
    }
    
    // Checks for known error
    private void CheckStuckError()
    {
        // KNOWN BUG: Ball sometimes gets stuck by CPU against wall
        if (!Direction.Equals(Vector2.zero) && DistanceTraveled is (<= 0.01f and > 0f))
        {
            Debug.LogWarning("Ball is stuck with " + DistanceTraveled + " distance made, direction is " + Direction + ", speed is " + CurrentSpeed + ", rigidbody velocity is " + _rigidbody.velocity);

            string logFile = "%USERPROFILE%\\AppData\\LocalLow\\fffadedev\\Pong\\Player.log";
            
            GameObject.FindGameObjectWithTag("ErrorUI").GetComponent<ErrorUI>().Show($"Please send your game logs stored at '{logFile}' as well as a screenshot of this error to the developer. Thank you!");
        }
    }
    
    // Update ball velocity based on current speed
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

        Direction = new Vector2(xVelocity, yVelocity).normalized;
    }
    
    // Sets movement to zero
    public void StopMovement()
    {
        Direction = Vector2.zero;
        CurrentSpeed = initialSpeed;

        _ballFire.ResetFire();

        DistanceTraveled = 0f;
    }
    
    // Moves ball to a position
    public void MovePosition(Vector2 position)
    {
        _rigidbody.position = position;
    }
}
