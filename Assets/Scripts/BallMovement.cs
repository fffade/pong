using System;
using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class BallMovement : MonoBehaviour
{

    private ErrorUI _errorUI;
    
    private Rigidbody2D _rigidbody;

    private Transform _transform;

    private BallFire _ballFire;
    
    
    // How much y direction can be used when hitting with paddle
    [SerializeField] private float yVelocityClamp = 0.5f;

    [SerializeField] private float minInitialAngle,
                                    maxInitialAngle;

    [SerializeField] private float initialSpeed;

    public float CurrentSpeed { get; private set; }

    // Ball changes speed over time
    [SerializeField] private float acceleration = 0.1f;

    public Vector2 Direction { get; private set; }

    public float TargetRotation => Vector2.SignedAngle(Vector2.down, Direction);

    [SerializeField] private float rotationSpeed;

    // The last paddle this ball was hit by
    public Transform lastHitPaddle;
    
    // Distance moved since last stop
    private float _distanceTraveledTimer;
    public float DistanceTraveled { get; private set; }
    public Vector2 LastPosition { get; private set; }


    void Awake()
    {
        _errorUI = GameObject.FindGameObjectWithTag("ErrorUI").GetComponent<ErrorUI>();
        
        _rigidbody = GetComponent<Rigidbody2D>();

        _transform = transform;

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
        UpdateRotation();
    }

    void LateUpdate()
    {
        LastPosition = _rigidbody.position;
    }
    
    // Checks for known error
    private void CheckStuckError()
    {
        // KNOWN BUG: Ball sometimes gets stuck by CPU against wall
        // Temporary fix
        if (!Direction.Equals(Vector2.zero) && DistanceTraveled is (<= 0.01f and > 0f))
        {
            Debug.LogWarning("Ball is stuck with " + DistanceTraveled + " distance made, direction is " + Direction + ", speed is " + CurrentSpeed + ", rigidbody velocity is " + _rigidbody.velocity);

            // string logFile = $"{Application.persistentDataPath}\\Player.log";
            //
            // _errorUI.Show($"Please send your game logs stored at '{logFile}' as well as a screenshot of this error to the developer. Thank you!");
            
            _rigidbody.MovePosition(_rigidbody.position + (Vector2.zero - _rigidbody.position).normalized * 2f);
        }
    }
    
    // Update rotation of ball toward direction
    private void UpdateRotation()
    {
        Debug.Log($"{_rigidbody.rotation} -> {TargetRotation}");
        
        float newRotation = Mathf.Lerp(_rigidbody.rotation, TargetRotation,
            Time.fixedDeltaTime * rotationSpeed);

        _rigidbody.rotation = newRotation;
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
        
        // Bug fix: Launch is clamped between this angle TOWARD POSITIVE X
        float randomAngle = Random.Range(minInitialAngle, maxInitialAngle);

        int xDirection = Random.Range(0, 2) == 0 ? 1 : -1;
        
        Direction = new Vector2(
            xDirection * Mathf.Cos(Mathf.Deg2Rad * randomAngle),
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
