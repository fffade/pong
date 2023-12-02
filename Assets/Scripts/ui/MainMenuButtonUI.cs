using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonUI : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /* Clicking main menu button returns user to main menu */
    public void OnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
