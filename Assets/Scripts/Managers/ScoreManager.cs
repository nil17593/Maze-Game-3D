using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// Handles in game score/time taken by the player to travel the maze
    /// </summary>
    public class ScoreManager : Singleton<ScoreManager>
    {
        #region private variables
        public float startTime { get; private set; }
        public float lastSavedTime { get; private set; }
        private const string startTimeKey = "StartTime";
        private const string lastSavedTimeKey = "LastSavedTime";
        #endregion

        protected override void Awake()
        {
            base.Awake();
            EventManager.Instance.OnGameOver += HandleGameOverEvent; // Subscribe to the game over event
        }

        private void Start()
        {
            CheckpointData checkpointData = CheckPointManager.Instance.LoadCheckpoint();
            if (checkpointData != null)
            {
                lastSavedTime = checkpointData.elapsedTime;
                startTime = Time.time - lastSavedTime; // Adjust start time based on loaded elapsed time
            }
            else
            {
                startTime = Time.time; // Record the start time when the maze begins
                lastSavedTime = 0f;
            }
        }

        private void Update()
        {
            if (!GameManager.Instance.IsGameOver)
            {
                lastSavedTime = Time.time - startTime; // Calculate the live time
                UIManager.Instance.DisplayTime(lastSavedTime); // Display the live time
            }
        }

        //triggers when check point reached
        public void OnCheckpointReached()
        {
            SaveLastSavedTime(); // Save the last saved time when checkpoint is reached
        }

        //triggers on game over
        private void HandleGameOverEvent()
        {
            SaveLastSavedTime(); // Save the last saved time when game is over
            ResetPlayerPrefs(); // Reset/delete relevant PlayerPrefs
        }

        //saves the last reached checkpoint time
        private void SaveLastSavedTime()
        {
            CheckpointData data = new CheckpointData { elapsedTime = lastSavedTime };
            CheckPointManager.Instance.SaveCheckpoint(data); // Save the last saved time to JSON
        }

        //reset playerprefs when game is over
        private void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteKey(startTimeKey); // Delete specific PlayerPrefs by key
            PlayerPrefs.DeleteKey(lastSavedTimeKey); // Delete specific PlayerPrefs by key
            Debug.Log("PlayerPrefs related to ScoreManager reset."); // Log the action (optional)
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnGameOver -= HandleGameOverEvent; // Unsubscribe from the game over event
        }

        //returns the last saved elapsed time
        public float GetLastSavedTime()
        {
            return lastSavedTime;
        }

        public void Reset()
        {
            startTime = 0f;
            lastSavedTime = 0f;
            ResetPlayerPrefs();
        }
    }
}