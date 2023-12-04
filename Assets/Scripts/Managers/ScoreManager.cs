using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private float startTime;
    private float lastSavedTime;
    private const string startTimeKey = "StartTime";
    private const string lastSavedTimeKey = "LastSavedTime";

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

    public void OnCheckpointReached()
    {
        SaveLastSavedTime(); // Save the last saved time when checkpoint is reached
    }

    private void HandleGameOverEvent()
    {
        SaveLastSavedTime(); // Save the last saved time when game is over
        ResetPlayerPrefs(); // Reset/delete relevant PlayerPrefs
    }

    private void SaveLastSavedTime()
    {
        CheckpointData data = new CheckpointData { elapsedTime = lastSavedTime };
        CheckPointManager.Instance.SaveCheckpoint(data); // Save the last saved time to JSON
    }

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

    public float GetLastSavedTime()
    {
        return lastSavedTime;
    }
}
