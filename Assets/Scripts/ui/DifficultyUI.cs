using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyUI : MonoBehaviour
{
    /* Handles changing difficulty settings */

    private Difficulty _difficulty;
    
    // UI Elements
    [SerializeField] private TextMeshProUGUI difficultyLevelText;
    
    // Start is called before the first frame update
    void Awake()
    {
        _difficulty = GameObject.FindGameObjectWithTag("GameController").GetComponent<Difficulty>();
    }


    void Update()
    {
        difficultyLevelText.SetText(_difficulty.CurrentSettings.uniqueName);
    }

    public void OnDifficultyChange()
    {
        _difficulty.ChangeDifficultySetting();
    }

}
