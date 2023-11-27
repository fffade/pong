using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // Player 1 and Player 2 score text
    [SerializeField] private TextMeshProUGUI player1ScoreText,
                                            player2ScoreText;

    // Change the displaying scores
    public void Set(int score1, int score2)
    {
        player1ScoreText.SetText(score1.ToString());
        player2ScoreText.SetText(score2.ToString());
    }
}
