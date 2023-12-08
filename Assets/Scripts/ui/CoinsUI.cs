using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    // Coin data retrieved from game manager
    private Coins _coins;
    
    // The text to display coins
    [SerializeField] private TextMeshProUGUI coinsText;

    
    void Awake()
    {
        _coins = GameObject.FindGameObjectWithTag("GameController").GetComponent<Coins>();
    }
    

    // Update is called once per frame
    void Update()
    {
        coinsText.SetText(_coins.Data.playerCoins.ToString());
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
