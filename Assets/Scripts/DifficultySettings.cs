using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DifficultySettings
{
    public string uniqueName;

    public int coinBonusIndex;

    public float speed,
        ballDistanceThreshold,
        curveAmount,
        forcefieldDistanceThreshold,
        forcefieldSpeedThreshold,
        wallBufferYDirectionThreshold,
        wallBufferAmount;

    public Vector2 curveDistanceThreshold;


    public DifficultySettings(string uniqueName, int coinBonusIndex,
                            float speed, float ballDistanceThreshold, Vector2 curveDistanceThreshold, float curveAmount,
                            float forcefieldDistanceThreshold, float forcefieldSpeedThreshold,
                            float wallBufferYDirectionThreshold, float wallBufferAmount)
    {
        this.uniqueName = uniqueName;
        this.coinBonusIndex = coinBonusIndex;
        this.speed = speed;
        this.ballDistanceThreshold = ballDistanceThreshold;
        this.curveDistanceThreshold = curveDistanceThreshold;
        this.curveAmount = curveAmount;
        this.forcefieldDistanceThreshold = forcefieldDistanceThreshold;
        this.forcefieldSpeedThreshold = forcefieldSpeedThreshold;
        this.wallBufferYDirectionThreshold = wallBufferYDirectionThreshold;
        this.wallBufferAmount = wallBufferAmount;
    }
    
    /* Load settings from a file using JSON */
    public static DifficultySettings LoadSettingsFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError($"Failed to load Difficulty settings from path: {path} does not exist");
            throw new FileLoadException();
        }

        string fileData = File.ReadAllText(path);

        DifficultySettings settings = JsonUtility.FromJson<DifficultySettings>(fileData);

        return settings;
    }
}
