using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonUI : MonoBehaviour
{
    private ShopUI _shopUI;

    void Awake()
    {
        _shopUI = GameObject.FindGameObjectWithTag("ShopUI").GetComponent<ShopUI>();
    }
    
    // When the shop button is pressed
    public void OnClick()
    {
        _shopUI.Show();
        Hide();
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
