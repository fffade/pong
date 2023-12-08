using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsEarnedEntry
{
    public string Description { get; private set; }
    public int Amount { get; private set; }

    public CoinsEarnedEntry(string description, int amount)
    {
        Description = description;
        Amount = amount;
    }
}
