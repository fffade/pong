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
    private ShopUI _shopUI;
    private CoinsUI _coinsUI;


    void Awake()
    {
        _helpMenuUI = GameObject.FindGameObjectWithTag("HelpMenuUI").GetComponent<HelpMenuUI>();
        _shopUI = GameObject.FindGameObjectWithTag("ShopUI").GetComponent<ShopUI>();
        _coinsUI = GameObject.FindGameObjectWithTag("CoinUI").GetComponent<CoinsUI>();
    }

    void Start()
    {
        _helpMenuUI.Hide();
        _shopUI.Hide();
        _coinsUI.Hide();
    }

    // Enters the game via a new scene
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    // Opens the shop menu
    public void OpenShop()
    {
        _shopUI.Show();
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
