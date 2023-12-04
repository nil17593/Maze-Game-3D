using TMPro;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// UI manager class manages all the UI in the game
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject levelRestart;
        [SerializeField] private TextMeshProUGUI levelTimeText;

        [Header("Level Win Panel Settings")]
        [SerializeField] private GameObject levelWinPanel;
        [SerializeField] private TextMeshProUGUI timeTakenText;

        [Header("Level Lose Panel Settings")]
        [SerializeField] private GameObject levelLosePanel;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            EventManager.Instance.OnGameOver += OnGameOver;
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnGameOver -= OnGameOver;
        }

        public void ActivateLevelWinPanel()
        {
            timeTakenText.text = ScoreManager.Instance.GetLastSavedTime().ToString();
            levelLosePanel.SetActive(true);
        }
        public void ActivateLosePanel()
        {
            levelLosePanel.SetActive(true);
        }

        public void Reset()
        {
            if (levelLosePanel.activeSelf)
            {
                levelLosePanel.SetActive(false);
            }
            if (levelWinPanel.activeSelf)
            {
                levelWinPanel.SetActive(false);
            }
        }

        private void OnGameOver()
        {
            ActivateLosePanel();
        }

        public void DisplayTime(float time)
        {
            levelTimeText.text = "Score: " + Mathf.Round(time).ToString();
        }
    }
}