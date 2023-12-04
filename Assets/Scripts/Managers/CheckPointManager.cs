using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// class that manages checkpoint and its data and save in JSON so that we can load it 
    /// </summary>
    public class CheckPointManager : Singleton<CheckPointManager>
    {
        private string savePath = "checkpointData.json";

        private void Start()
        {
            EventManager.Instance.OnGameOver += HandleGameOverEvent;
            EventManager.Instance.OnCheckpointReached += SaveCheckpoint;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnGameOver -= HandleGameOverEvent;
            EventManager.Instance.OnCheckpointReached -= SaveCheckpoint;
        }

        //deletes all checkpint data when Game is over
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

        //triggers on game over
        private void HandleGameOverEvent()
        {
            DeleteCheckpointData(); // Call this method when game is over
        }

        //save checkpoint data
        public void SaveCheckpoint(CheckpointData data)
        {
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(savePath, jsonData);
            Debug.Log(jsonData);
        }

        //loads check point data
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
        public int keyCount;//keys count which are found during play
        public List<GameObject> checkPoints;//we can add checkpoints in the json so that we can remove them once we reached at specific checkpoint
    }

}