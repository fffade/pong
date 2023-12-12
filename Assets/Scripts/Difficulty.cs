using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    // All of available settings to choose from loaded through these files
    [SerializeField] private string[] difficultyFileNames;
    
    public DifficultySettings[] AvailableSettings { get; private set; }
    
    public DifficultySettings CurrentSettings { get; private set; }


    [SerializeField] private string fileName;
    private string _filePath;


    void Awake()
    {
        _filePath = Application.persistentDataPath + "/" + fileName;
        
        LoadDifficultySettings();
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
    
    /* Loads all difficulty settings from given files */
    private void LoadDifficultySettings()
    {
        AvailableSettings = new DifficultySettings[difficultyFileNames.Length];
        
        for (int i = 0; i < difficultyFileNames.Length; i++)
        {
            AvailableSettings[i] = DifficultySettings.LoadSettingsFromFile(Application.streamingAssetsPath + "/" + difficultyFileNames[i]);
        }
        
        Debug.Log($"Successfully loaded {AvailableSettings.Length} settings files");

        if (AvailableSettings.Length <= 0)
        {
            Debug.LogError("No available difficulty settings to choose from");
            throw new Exception();
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
        
        streamWriter.Write(JsonUtility.ToJson(CurrentSettings));

        streamWriter.Close(); // Do not forget to close file
        
        Debug.Log("Saved difficulty settings");
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

        CurrentSettings = JsonUtility.FromJson<DifficultySettings>(fileData);
            
        Debug.Log($"Existing difficulty settings loaded");
    }
    
    // Transitions to the next difficulty setting available
    public void ChangeDifficultySetting()
    {
        int currentSettingIndex = Array.IndexOf(AvailableSettings, AvailableSettings.First(settings => settings.uniqueName.Equals(CurrentSettings.uniqueName)));

        CurrentSettings = currentSettingIndex < AvailableSettings.Length - 1 ? AvailableSettings[currentSettingIndex + 1] : AvailableSettings[0];
    }
    
    /* Called when the application is closed properly, ensure auto-save */
    void OnApplicationQuit()
    {
        Debug.Log("Application exit detected, auto-saving difficulty settings");
        SaveData();
    }
    
    /* Data need to be saved in between scenes as well */
    void OnDestroy()
    {
        Debug.Log("Scene exit detected, auto-saving difficulty settings");
        SaveData();
    }
    
    /* Resets difficulty data, selecting the first file by default */
    public void Reset()
    {
        // Delete existing save data
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }

        // Create a new file
        StreamWriter file = File.CreateText(_filePath);
        
        file.Write(JsonUtility.ToJson(AvailableSettings[0]));

        file.Close();
        
        Debug.Log("Difficulty settings reset");
        
        ReadData();
    }
}
