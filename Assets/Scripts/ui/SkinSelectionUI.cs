using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SkinSelectionUI : MonoBehaviour
{
    private Skins _skinsManager;
    private Coins _coins;
    
    // Selection panel retrieves its item as children
    private SkinItemUI[] _itemUIs;
    
    // Available skins are loaded from a transform
    [SerializeField] private Transform skinLoader;
    private PaddleSkin[] _skins;
    
    // The currently selected item
    public SkinItemUI SelectedItemUI { get; private set; }
    
    // The currently equipped item
    [CanBeNull] public SkinItemUI EquippedItemUI { get; private set; }


    void Awake()
    {
        _skinsManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Skins>();

        _coins = GameObject.FindGameObjectWithTag("GameController").GetComponent<Coins>();
        
        _itemUIs = GetComponentsInChildren<SkinItemUI>();

        SelectedItemUI = _itemUIs[0]; // Default selection
    }

    void Start()
    {
        LoadSkins();
        ReloadSkinItemUIs();

        // Preload
        Update();
    }

    // Reloads skin options from the serialized transform
    private void LoadSkins()
    {
        _skins = skinLoader.GetComponentsInChildren<PaddleSkin>();
        
        Debug.Log($"Successfully loaded {_skins.Length} skins");
    }
    
    // Loads skins into the UI
    private void ReloadSkinItemUIs()
    {
        for (int i = 0; i < _itemUIs.Length; i++)
        {
            SkinItemUI skinItemUI = _itemUIs[i];
            
            PaddleSkin correspondingSkin = i >= _skins.Length ? null : _skins[i];

            // Not always an available skin for every slot
            if (!correspondingSkin)
            {
                skinItemUI.Hide();
                continue;
            }

            skinItemUI.Set(correspondingSkin,
                SelectedItemUI.Equals(skinItemUI),
                _skinsManager.IsSkinOwned(correspondingSkin), 
                EquippedItemUI && EquippedItemUI.Equals(skinItemUI)
            );
            skinItemUI.Show();
        }
    }

    void Update()
    {
        SkinItemUI[] equippedItems =
            _itemUIs.Where(itemUI => itemUI.Skin && itemUI.Skin.id == _skinsManager.Data.equippedSkinId).ToArray();

        EquippedItemUI = equippedItems.Any() ? equippedItems.First() : null;
        
        ReloadSkinItemUIs();
    }
    
    /* Selects a new item/skin on the panel */
    public void SelectItemUI(SkinItemUI itemUI)
    {
        SelectedItemUI = itemUI;
    }
    
    /* Buys the currently selected item if possible */
    public void BuySelectedItemUI()
    {
        // For owned skins, equip them
        if (_skinsManager.IsSkinOwned(SelectedItemUI.Skin))
        {
            _skinsManager.Data.equippedSkinId = SelectedItemUI.Skin.id;
            
            Debug.Log($"Equipping skin {SelectedItemUI.Skin.id}");
            return;
        }

        if (!_coins.CanSpend(SelectedItemUI.Skin.coinPrice))
            return;

        int skinId = SelectedItemUI.Skin.id;
        
        _coins.Spend(SelectedItemUI.Skin.coinPrice);
        
        // Ensure skin is added to player's roster
        _skinsManager.Data.ownedSkinIds.Add(skinId);

        Debug.Log($"Player purchased skin {skinId} for {SelectedItemUI.Skin.coinPrice} coins");
    }
}
