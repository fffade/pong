using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsEarnedUI : MonoBehaviour
{
    private CoinDistributor _coinDistributor;
    
    // Each entry UI element
    private CoinsEarnedEntryUI[] _entryUIs;

    void Awake()
    {
        // Coin distributor decides at the end of the match how many coins the player earned
        _coinDistributor = GameObject.FindGameObjectWithTag("GameController").GetComponent<CoinDistributor>();
        
        // Retrieve entry elements as children of this panel
        _entryUIs = GetComponentsInChildren<CoinsEarnedEntryUI>();
    }

    void Start()
    {
        foreach (CoinsEarnedEntryUI entryUI in _entryUIs)
        {
            entryUI.Hide();
        }
    }

    void Update()
    {
        // Always display the current coin entries
        for (int i = 0; i < _entryUIs.Length; i++)
        {
            CoinsEarnedEntry correspondingEntry = i < _coinDistributor.Entries.Count ? _coinDistributor.Entries[i] : null;

            // Not every indexed entry will exist
            if (correspondingEntry == null)
            {
                _entryUIs[i].Hide();
                continue;
            }
            
            _entryUIs[i].Set(correspondingEntry);
            _entryUIs[i].Show();
        }
    }
}
