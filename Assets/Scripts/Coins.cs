using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private string fileName; // Where to store coin data
    private string _filePath;

    public CoinData Data { get; private set; }


    void Awake()
    {
        _filePath = Application.persistentDataPath + "/" + fileName;
    }

    void Start()
    {
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
    
    /* Checks if the player has enough coins */
    public bool CanSpend(int amount)
    {
        return Data.playerCoins >= amount;
    }
    
    /* Removes an amount of coins */
    public void Spend(int amount)
    {
        Data.playerCoins = Math.Max(Data.playerCoins - amount, 0);
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
        
        Debug.Log("Saved coin data");
    }
    
    /* Reads existing coin data from file */
    public void ReadData()
    {
        if (!File.Exists(_filePath))
            return;
        
        string fileData = File.ReadAllText(_filePath);

        if (fileData.Length <= 0)
        {
            Reset();
            return;
        }

        Data = JsonUtility.FromJson<CoinData>(fileData);
            
        Debug.Log($"Existing coin data read: {Data.playerCoins} coins");
    }
    
    /* Called when the application is closed properly, ensure auto-save */
    void OnApplicationQuit()
    {
        Debug.Log("Application exit detected, auto-saving coin data");
        SaveData();
    }
    
    /* Data need to be saved in between scenes as well */
    void OnDestroy()
    {
        Debug.Log("Scene exit detected, auto-saving coin data");
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
        
        file.Write(JsonUtility.ToJson(new CoinData()));

        file.Close();
        
        Debug.Log("Coin data reset");
        
        ReadData();
    }
}
