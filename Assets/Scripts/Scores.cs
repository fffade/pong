using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Scores : MonoBehaviour
{
    /* Manages two-player scores */

    private ScoreUI _scoreUI;
    
    public int Score1,
                Score2;

    void Awake()
    {
        _scoreUI = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<ScoreUI>();
    }


    public void ResetScores()
    {
        Score1 = 0;
        Score2 = 0;
    }
    
    // Returns the side (1 or 2) that won based off a winning score
    // Returns -1 if neither has won yet
    public int GetWinningScore(int maxScore)
    {
        if (Score1 >= maxScore)
        {
            return 1;
        }

        if (Score2 >= maxScore)
        {
            return 2;
        }

        return -1;
    }

    void Update()
    {
        _scoreUI.Set(Score1, Score2);
    }
}
