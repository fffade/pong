using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPreviewUI : MonoBehaviour
{
    private Skins _skins;   
    
    // Skin preview determined by selected skin
    private SkinSelectionUI _selectionUI;
    
    [SerializeField] private TextMeshProUGUI nameText,
                                            amountText;

    [SerializeField] private Image paddleImage;

    [SerializeField] private Button buyEquipButton;


    void Awake()
    {
        _skins = GameObject.FindGameObjectWithTag("GameController").GetComponent<Skins>();
        
        _selectionUI = GameObject.FindGameObjectWithTag("SkinSelectionUI").GetComponent<SkinSelectionUI>();
    }

    // Update is called once per frame
    void Update()
    {
        SkinItemUI selectedItemUI = _selectionUI.SelectedItemUI;

        Set(
            selectedItemUI.Skin, 
            _skins.IsSkinOwned(selectedItemUI.Skin),
            _selectionUI.EquippedItemUI && _selectionUI.EquippedItemUI.Equals(selectedItemUI)
        );
    }
    
    /* Updates preview properties based on a given skin */
    private void Set(PaddleSkin previewSkin, bool isOwned, bool isEquipped)
    {
        if (previewSkin == null)
            return;

        buyEquipButton.interactable = (!isEquipped || !isOwned);
        
        buyEquipButton.GetComponentInChildren<TextMeshProUGUI>().text = isEquipped ? "Equipped" :
                (isOwned ? "Equip" : "Buy");
        
        nameText.SetText(previewSkin.displayName);
        amountText.SetText(!isOwned ? previewSkin.coinPrice.ToString() : "bought");
        paddleImage.sprite = previewSkin.paddleSprite;
    }
}
