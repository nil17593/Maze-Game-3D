using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameOver { get; private set; }
    [SerializeField] private List<GameObject> checkPoints;
    [SerializeField] private Transform player;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CheckpointData checkpointData = CheckPointManager.Instance.LoadCheckpoint();
        if (checkpointData != null)
        {
            player.transform.position = checkpointData.playerPosition;
            //ScoreManager.Instance.
        }

    }

    public void RemoveCheckPoint(GameObject checkpoint)
    {
        if (checkPoints.Contains(checkpoint))
        {
            checkPoints.Remove(checkpoint);
        }
        Destroy(checkpoint);
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        IsGameOver = true;
        EventManager.Instance.TriggerGameOverEvent(); // Trigger the game over event
    }

    public void Reset()
    {
        IsGameOver = false;

    }
}
