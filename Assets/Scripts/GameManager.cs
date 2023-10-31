using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Progress;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _finishWindow;
    [SerializeField] TextMeshProUGUI _textLevel;

    public int _level;
    public CoinManager coinManager;

    public static event Action OnLevelRestarted;

    private void OnEnable()
    {
        Progress.OnLoadComplete += LoadFromProgress;
    }

    private void OnDisable()
    {
        Progress.OnLoadComplete -= LoadFromProgress;
    }

    private void Start()
    {
        LoadFromProgress();
    }

    public void ShowFinishWindow()
    {
        _finishWindow.SetActive(true);
    }

    public void NextLevel()
    {
        _level = _level + 1;
        Progress.Instance.PlayerInfo.LevelNumber = _level;
        coinManager.ButtonOff();
        coinManager.NoThanks();

        DataHolder.ProgressInstance.MySave();

        StartCoroutine(RestartLevelWithDelay(2.0f));
    }

    private IEnumerator RestartLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadFromProgress()
    {
        _level = Progress.Instance.PlayerInfo.LevelNumber;
        _textLevel.text = "LEVEL " + _level.ToString();
    }
}
