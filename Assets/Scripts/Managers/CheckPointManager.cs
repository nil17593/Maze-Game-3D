using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckPointManager : Singleton<CheckPointManager>
{
    private string savePath = "checkpointData.json";


    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnGameOver += HandleGameOverEvent;
        EventManager.Instance.OnCheckpointReached += SaveCheckpoint;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameOver -= HandleGameOverEvent;
        EventManager.Instance.OnCheckpointReached -= SaveCheckpoint;
    }

    public void DeleteCheckpointData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Checkpoint data deleted.");
        }
        else
        {
            Debug.Log("No checkpoint data to delete.");
        }
    }

    private void HandleGameOverEvent()
    {
        DeleteCheckpointData(); // Call this method when game is over
    }

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
            Debug.Log(jsonData);
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

