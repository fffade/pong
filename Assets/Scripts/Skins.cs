using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Skins : MonoBehaviour
{
    [SerializeField] private string fileName; // Where to store skins data
    private string _filePath;

    public SkinData Data { get; private set; }
    
    /* Skins that are 'pre-owned' by every player */
    [SerializeField] private List<int> defaultOwnedSkins;


    void Awake()
    {
        Data = new SkinData(new List<int>());
        
        _filePath = Application.persistentDataPath + "/" + fileName;
        
        // Read existing saved data
        // If accessible
        if (File.Exists(_filePath))
        {
            ReadData();
        }
        else
        {
            // Reset data
            Reset();
        }
    }
    
    /* Writes coin data to file */
    public void SaveData()
    {
        // Create new file if necessary
        if (!File.Exists(_filePath))
        {
            Reset();
        }

        StreamWriter streamWriter = new StreamWriter(_filePath, false);
        
        streamWriter.Write(JsonUtility.ToJson(Data));

        streamWriter.Close(); // Do not forget to close file
        
        Debug.Log("Saved skin data");
    }
    
    /* Reads data from file */
    public void ReadData()
    {
        if (!File.Exists(_filePath))
            return;
        
        string fileData = File.ReadAllText(_filePath);

        Data = JsonUtility.FromJson<SkinData>(fileData);

        Debug.Log($"Existing skin data read: " + Data.ownedSkinIds.Count + " owned skins");
    }
    
    /* Returns if a skin is 'owned' according to the data file */
    public bool IsSkinOwned(PaddleSkin skin)
    {
        return Data.ownedSkinIds.Contains(skin.id);
    }
    
    /* Called when the application is closed properly, ensure auto-save */
    void OnApplicationQuit()
    {
        Debug.Log("Application exit detected, auto-saving skin data");
        SaveData();
    }
    
    /* Data need to be saved in between scenes as well */
    void OnDestroy()
    {
        Debug.Log("Scene exit detected, auto-saving skin data");
        SaveData();
    }
    
    /* Resets all coin data, creating a completely new file */
    public void Reset()
    {
        // Delete existing save data
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }

        // Create an empty data file with zero coins
        StreamWriter file = File.CreateText(_filePath);
        
        file.Write(JsonUtility.ToJson(new SkinData(defaultOwnedSkins)));

        file.Close();
        
        Debug.Log("Skin data reset");

        ReadData();
    }
}
