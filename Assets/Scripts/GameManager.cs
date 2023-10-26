using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _finishWindow;

    [SerializeField] TextMeshProUGUI _textLevel;   

    [SerializeField] int _level;

    private bool isButtonHeld;

    private void Start()
    {
        _level = Progress.Instance.LevelNumber;
        _textLevel.text = "LEVEL " + _level.ToString();
    }    

    public void ShowFinishWindow()
    {
        _finishWindow.SetActive(true);
    }

    public void NextLevel()
    {
        _level += 1;        
        StartCoroutine(RestartLevelWithDelay(2.0f));
    }

    private IEnumerator RestartLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Progress.Instance.LevelNumber = _level;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetCurrentLevel()
    {
        return _level;
    }
}
