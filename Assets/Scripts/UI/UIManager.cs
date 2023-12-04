using TMPro;
using UnityEngine;

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

    public void ActivateLevelWinPanel()
    {
        //timeTakenText.text= ScoreManager.Instance.
    }
    public void ActivateLosePanel()
    {
        //timeTakenText.text= ScoreManager.Instance.
    }

    public void DisplayTime(float time)
    {
        levelTimeText.text = "Score: " + Mathf.Round(time).ToString();
    }
}
