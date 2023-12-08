using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSkinApplier : MonoBehaviour
{
    /* Applies the equipped skin to this paddle */
    private SkinSelectionUI _skinSelectionUI;

    [SerializeField] private SpriteRenderer paddleRenderer;

    void Awake()
    {
        _skinSelectionUI = GameObject.FindGameObjectWithTag("SkinSelectionUI").GetComponent<SkinSelectionUI>();
    }

    void Update()
    {
        if(_skinSelectionUI.EquippedItemUI)
            paddleRenderer.sprite = _skinSelectionUI.EquippedItemUI.Skin.paddleSprite;
    }
}
