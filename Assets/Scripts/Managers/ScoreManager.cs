using UnityEngine;
using TMPro;
public class ScoreManager : Singleton<ScoreManager>
{
    public TextMeshProUGUI scoreText; // Reference to the UI text to display the score
    private float startTime;

    private void Start()
    {
        if (PlayerPrefs.HasKey("StartTime"))
        {
            float savedTime = PlayerPrefs.GetFloat("StartTime");
            startTime = Time.time - savedTime; // Resume from the saved time
        }
        else
        {
            startTime = Time.time; // Record the start time when the maze begins
            PlayerPrefs.SetFloat("StartTime", startTime); // Save the start time
        }
    }

    // Call this method when a checkpoint is completed
    public void OnCheckpointReached()
    {
        float elapsedTime = Time.time - startTime;
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        // Calculate the elapsed time since the maze started
        float elapsedTime = Time.time - startTime;

        // Display the elapsed time as the score
        DisplayScore(elapsedTime);
    }

    private void DisplayScore(float time)
    {
        // Display the time as the score on the UI text
        scoreText.text = "Score: " + Mathf.Round(time).ToString();
    } 
}
