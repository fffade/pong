using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class VersionNumberUI : MonoBehaviour
{
    /* Displays the current program version and build number as text */

    [SerializeField] private TextMeshProUGUI versionNumberText;

    [SerializeField] private BuildNumber buildNumber;

    [SerializeField] private string stage;


    void Update()
    {
        versionNumberText.SetText($"{stage} Version {Application.version} build {buildNumber.build}");
    }
}
