using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// manager class handles all the Game data inclusing retrieving the data from json
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public bool IsGameOver { get; private set; }
        [Header("Settings")]
        [SerializeField] private List<GameObject> checkPoints;
        [SerializeField] private Transform player;
        [SerializeField] private Transform startPoint;

        protected override void Awake()
        {
            base.Awake();
            CheckpointData checkpointData = CheckPointManager.Instance.LoadCheckpoint();
            if (checkpointData != null)
            {
                player.transform.position = checkpointData.playerPosition;
            }
            else
            {
                player.transform.position = startPoint.position;
            }
        }
        private void Start()
        {
           
        }

        //we can use this method to remove the reached checkpoint from list
        public void RemoveCheckPoint(GameObject checkpoint)
        {
            if (checkPoints.Contains(checkpoint))
            {
                checkPoints.Remove(checkpoint);
            }
            Destroy(checkpoint);
        }

        #region Button click events

        //menu button click
        public void OnMenuButtonClicked()
        {
            SceneManager.LoadScene("MenuScene");
            Reset();
        }

        //restsrts the level
        public void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Reset();
        }

        #endregion


        //triggers when game is over
        public void GameOver()
        {
            IsGameOver = true;
            EventManager.Instance.TriggerGameOverEvent(); // Trigger the game over event
        }

        //reset game data
        public void Reset()
        {
            IsGameOver = false;
            UIManager.Instance.Reset();
            ScoreManager.Instance.Reset();
        }
    }
}