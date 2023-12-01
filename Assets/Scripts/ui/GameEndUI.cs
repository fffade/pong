using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndUI : MonoBehaviour
{
    // Winner display text
    [SerializeField] private TextMeshProUGUI winnerText;

    // Show with a specific winner
    // winner = 1 for player
    // winner = 2 for cpu
    public void Show(int winner)
    {
        string winnerString = (winner == 1) ? "You won!" : "You lost";
        
        winnerText.SetText(winnerString);
        
        gameObject.SetActive(true);
    }
    
    // Hide
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
