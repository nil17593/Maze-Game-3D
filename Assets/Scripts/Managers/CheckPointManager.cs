using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckPointManager : Singleton<CheckPointManager>
{
    private string savePath = "checkpointData.json";

    public void SaveCheckpoint(CheckpointData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
        Debug.Log(jsonData);
    }

    public CheckpointData LoadCheckpoint()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<CheckpointData>(jsonData);
        }
        else
        {
            Debug.LogError("No saved checkpoint data found.");
            return null;
        }
    }
}

[System.Serializable]
public class CheckpointData
{
    public Vector3 playerPosition;
    public float elapsedTime;
}

