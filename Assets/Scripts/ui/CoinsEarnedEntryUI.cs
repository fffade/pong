using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsEarnedEntryUI : MonoBehaviour
{
    // Description & amount
    [SerializeField] private TextMeshProUGUI descriptionText,
                                            amountText;

    public void Set(CoinsEarnedEntry entry)
    {
        descriptionText.SetText(entry.Description);
        amountText.SetText("+" + entry.Amount);
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
