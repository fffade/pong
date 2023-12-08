using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItemUI : MonoBehaviour
{
    public PaddleSkin Skin { get; private set; }

    [SerializeField] private Image selectedImage,
                                iconImage,
                                overlayImage;

    [SerializeField] private Sprite equippedOverlaySprite,
        notOwnedOverlaySprite;
    
    // Sets a new icon for this item
    public void Set(PaddleSkin skin, bool isSelected, bool isOwned, bool isEquipped)
    {
        Skin = skin;
        
        iconImage.sprite = skin.iconSprite;

        selectedImage.enabled = isSelected;

        if (isOwned)
        {
            overlayImage.sprite = equippedOverlaySprite;
            overlayImage.enabled = isEquipped;
        }
        else
        {
            overlayImage.sprite = notOwnedOverlaySprite;
            overlayImage.enabled = true;
        }
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    // No item in this panel
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // When this item is pressed
    public void OnSelected()
    {
        Debug.Log($"Skin item (skin {Skin.id}) received press");
        
        SendMessageUpwards(nameof(SkinSelectionUI.SelectItemUI), this);
    }
}
