using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* This script must be applied to the Game Controller that also has a 'Coins' component */
[RequireComponent(typeof(Coins))]
[RequireComponent(typeof(Scores))]
public class CoinDistributor : MonoBehaviour
{
    /* Handles earned coins after a match by generating "coins earned entries" */

    private Scores _scores;
    private Coins _coins;
    
    // Amounts that can be earned
    [SerializeField] private int winnerAmount,
                                loserAmount,
                                flawlessWinAmount;
    
    // The last generated coin entries
    public List<CoinsEarnedEntry> Entries { get; private set; }


    void Awake()
    {
        _scores = GetComponent<Scores>();
        _coins = GetComponent<Coins>();
    }
    
    /* Uses the winner / scores to generate coins earned entries */
    public void GenerateDistribution()
    {
        Entries = new List<CoinsEarnedEntry>();
        
        // Winner gains more points than loser (according to code)
        bool wonMatch = _scores.Score1 > _scores.Score2;
        
        CoinsEarnedEntry winningEntry = new CoinsEarnedEntry(
            wonMatch ? "Win" : "Loss",
            wonMatch ? winnerAmount : loserAmount
        );
        
        Entries.Add(winningEntry);
        
        // Enemy earning NO POINTS is a flawless win
        if (_scores.Score2 <= 0)
        {
            Entries.Add(new CoinsEarnedEntry(
                "Flawless",
                flawlessWinAmount
            ));
        }
        
        // Apply total earned coins to the user coins
        _coins.Data.playerCoins += GetDistributionTotal();
    }
    
    /* Returns the total coins earned based on current entries */
    public int GetDistributionTotal()
    {
        return Entries.Sum((entry) => entry.Amount);
    }
}
