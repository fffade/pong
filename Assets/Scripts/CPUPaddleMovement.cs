using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUPaddleMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private EdgeCollider2D _borderCollider;

    private PaddleForcefield _forcefield;

    private Difficulty _difficulty;
    

    // The ball to reference when moving
    [SerializeField] private Transform ball;
    private BallMovement _ballMovement;


    public bool IsMovingUp { get; private set; }
    public bool IsMovingDown { get; private set; }


    public DifficultySettings Settings { private set; get; }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _borderCollider = GameObject.FindGameObjectWithTag("Border").GetComponent<EdgeCollider2D>();

        _forcefield = GetComponent<PaddleForcefield>();

        _ballMovement = ball.GetComponent<BallMovement>();

        _difficulty = GameObject.FindGameObjectWithTag("GameController").GetComponent<Difficulty>();
    }

    void Start()
    {
        // On every creation, load settings from global (main menu difficulty)
        Settings = _difficulty.CurrentSettings;
    }

    void FixedUpdate()
    {
        DetermineMovement();
        DetermineForcefieldTrigger();
        UpdateMovement();
    }

    private void DetermineMovement()
    {
        float ballDistance = Mathf.Abs(ball.position.y - _rigidbody.position.y);

        float targetY = ball.position.y;
        
        // Adjust for curve if ball is close enough
        if (Mathf.Abs(ball.position.x - _rigidbody.position.x) <= Settings.curveDistanceThreshold.x && ballDistance <= Settings.curveDistanceThreshold.y)
        {
            float curve = (targetY >= _rigidbody.position.y ? -1f : 1f) * Settings.curveAmount;

            targetY += curve;
            
            // Debug.Log($"Curving: {ballDistance}");
        }
        
        // Don't 'hug' the wall if the ball is moving fast, since its direction may change unexpectedly
        if (Mathf.Abs(_ballMovement.Direction.y) >= Settings.wallBufferYDirectionThreshold)
        {
            Vector2 yClamp = new Vector2(
                _borderCollider.bounds.max.y - Settings.wallBufferAmount,
                _borderCollider.bounds.min.y + Settings.wallBufferAmount
            );

            targetY = Mathf.Clamp(targetY, yClamp.y, yClamp.x);
            
            // Debug.Log("Wall buffer: " + targetY);
        }

        IsMovingUp = ballDistance > Settings.ballDistanceThreshold && targetY > _rigidbody.position.y;
        IsMovingDown = ballDistance > Settings.ballDistanceThreshold && targetY < _rigidbody.position.y;
        
        // If the ball is BEHIND this paddle, then stop moving altogether
        // This FIXES a bug where the ball will get stuck against the wall by the CPU
        if (ball.position.x >= _rigidbody.position.x)
        {
            IsMovingUp = false;
            IsMovingDown = false;
        }
    }

    private void DetermineForcefieldTrigger()
    {
        if (_forcefield.uses <= 0 || _forcefield.forcefield.IsActive)
            return;
        
        float ballDistanceX = Mathf.Abs(ball.position.x - _rigidbody.position.x);

        float ballSpeed = _ballMovement.CurrentSpeed;
        
        Debug.Log(ballSpeed);

        if (ballDistanceX <= Settings.forcefieldDistanceThreshold && ballSpeed > Settings.forcefieldSpeedThreshold)
        {
            _forcefield.forcefield.Activate();
            _forcefield.uses--;
        }
    }
    
    private void UpdateMovement()
    {
        if (IsMovingUp)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * (Settings.speed * Time.fixedDeltaTime));
        }

        if (IsMovingDown)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * (Settings.speed * Time.fixedDeltaTime));
        }
    }
}
