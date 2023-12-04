using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    /* Handles the main menu loop */

    private HelpMenuUI _helpMenuUI;


    void Awake()
    {
        _helpMenuUI = GameObject.FindGameObjectWithTag("HelpMenuUI").GetComponent<HelpMenuUI>();
    }

    void Start()
    {
        _helpMenuUI.Hide();
    }

    // Enters the game via a new scene
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
