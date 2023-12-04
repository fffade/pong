using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForcefieldCounterUI : MonoBehaviour
{
    // Which paddle forcefield this counter references
    [SerializeField] private PaddleForcefield paddleForcefield;
    
    // Text to display forcefield uses
    [SerializeField] private TextMeshProUGUI forcefieldUsesText;


    void Update()
    {
        forcefieldUsesText.SetText(paddleForcefield.uses.ToString());
    }
}
