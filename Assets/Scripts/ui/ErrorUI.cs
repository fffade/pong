using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorUI : MonoBehaviour
{
    /* Displays an error message to the user and provides them with a reset button */

    [SerializeField] private TextMeshProUGUI errorText;


    public void Show(string errorMessage)
    {
        errorText.SetText(errorMessage);
        
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
