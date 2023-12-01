using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    /* Handles the game loop */

    private Scores _scores;

    private GameEndUI _gameEndUI;
    private PauseMenuUI _pauseMenuUI;
    
    // Handles pausing
    public bool IsPaused { get; private set; } = false;
    private float _timeScaleBeforePause;
    
    // Spawner for powerups
    [SerializeField] private PowerupSpawning powerupSpawner;
    
    // How much to reach to win the game
    [SerializeField] private int winningScore;

    [SerializeField] private float paddleDefaultYPosition;
    
    // The two paddles
    [SerializeField] private Transform paddle1, paddle2;

    [SerializeField] private Transform ballSpawnTransform;

    // Ball components
    private BallMovement _ballMovement;
    private BallSize _ballSize;
    private BallPaddleShrink _ballPaddleShrink;
    
    // How long to wait before launching the ball after spawning
    [SerializeField] private float ballLaunchDelay = 2f;

    
    void Awake()
    {
        _scores = GetComponent<Scores>();

        _gameEndUI = GameObject.FindGameObjectWithTag("GameEndUI").GetComponent<GameEndUI>();
        _pauseMenuUI = GameObject.FindGameObjectWithTag("PauseMenuUI").GetComponent<PauseMenuUI>();

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        _ballMovement = ball.GetComponent<BallMovement>();
        _ballSize = ball.GetComponent<BallSize>();
        _ballPaddleShrink = ball.GetComponent<BallPaddleShrink>();
    }

    void Start()
    {
        ResetAndStart();
    }

    void Update()
    {
        CheckInput();

        if (IsPaused)
        {
            _pauseMenuUI.Show();
        }
        else
        {
            _pauseMenuUI.Hide();
        }
    }

    /* Retrieve important game input */
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!IsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
    
    // Reset the game by resetting positions, score, etc.
    public void Restart()
    {
        _scores.ResetScores();

        ResetBall();

        ResetPaddles();

        _gameEndUI.Hide();
    }
    
    // Starts the game
    public void StartGame()
    {
        _ballMovement.Invoke(nameof(_ballMovement.RandomDirection), ballLaunchDelay);
        
        powerupSpawner.isSpawning = true;
    }
    
    // Resets and starts the game at once
    public void ResetAndStart()
    {
        Restart();
        StartGame();
    }
    
    // Resets ball to center of screen with zero movement
    public void ResetBall()
    {
        _ballMovement.MovePosition(ballSpawnTransform.position);
        _ballMovement.StopMovement();
        
        _ballSize.ResetModifier();

        _ballPaddleShrink.IsActive = false;
    }
    
    // Resets paddles to default y
    public void ResetPaddles()
    {
        paddle1.position = new Vector3(paddle1.position.x, paddleDefaultYPosition, paddle1.position.y);
        paddle2.position = new Vector3(paddle2.position.x, paddleDefaultYPosition, paddle2.position.y);
        
        paddle1.GetComponent<PaddleSize>().ResetModifier();
        paddle2.GetComponent<PaddleSize>().ResetModifier();
    }
    
    // Ends the game, displaying the final UI with the winner
    public void End(int winner)
    {
        powerupSpawner.ClearPowerups();
        powerupSpawner.isSpawning = false;
        
        _gameEndUI.Show(winner);
    }
    
    // Triggered when a border is hit by the pong ball
    // side = 1 for player,
    // side = 2 for cpu
    public void OnBorderHit(int side)
    {
        // Reset ball 
        ResetBall();
        
        // Update scoreboard
        if (side == 2)
        {
            _scores.Score1++;
        }
        else
        {
            _scores.Score2++;
        }
        
        // Firstly, determine if someone won by hitting max points
        int winner = _scores.GetWinningScore(winningScore);

        if (winner == -1)
        {
            // Wait before launching ball
            _ballMovement.Invoke(nameof(_ballMovement.RandomDirection), ballLaunchDelay);
            
            paddle1.GetComponent<PaddleSize>().ResetModifier();
            paddle2.GetComponent<PaddleSize>().ResetModifier();

            return;
        }
        
        End(winner);
    }
    
    // Pause the game and game logic
    public void Pause()
    {
        if (IsPaused)
            return;

        IsPaused = true;

        _timeScaleBeforePause = Time.timeScale; // Track time scale for resuming
        Time.timeScale = 0.0f;
    }
    
    // Return game to normal speed
    public void Resume()
    {
        if (!IsPaused)
            return;

        IsPaused = false;

        Time.timeScale = _timeScaleBeforePause; // Return time to normal speed before pause
    }
}
