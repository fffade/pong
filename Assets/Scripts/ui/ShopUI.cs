using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{

    private ShopButtonUI _shopButtonUI;
    
    private CoinsUI _coinsUI;

    void Awake()
    {
        _shopButtonUI = GameObject.FindGameObjectWithTag("ShopButtonUI").GetComponent<ShopButtonUI>();

        _coinsUI = GameObject.FindGameObjectWithTag("CoinUI").GetComponent<CoinsUI>();
    }
    
    public void Show()
    {
        _coinsUI.Show(); // Coins always show along with shop
        
        gameObject.SetActive(true);
    }

    public void OnClosed()
    {
        Hide();
        _shopButtonUI.Show();
    }

    public void Hide()
    {
        // Coins are hidden along with the shop WHEN on the MAIN MENU
        if (SceneManager.GetActiveScene().name.Equals("MainMenuScene"))
        {
            _coinsUI.Hide();
        }
        
        gameObject.SetActive(false);
    }
}
