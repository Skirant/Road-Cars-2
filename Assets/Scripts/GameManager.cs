using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _startMenu;
    [SerializeField] GameObject _finishWindow;

    [SerializeField] TextMeshProUGUI _textLevel;

    [SerializeField] CoinManager _coinManager;

    private bool isButtonHeld;

    private void Start()
    {
        _textLevel.text = SceneManager.GetActiveScene().name;
    }

    [System.Obsolete]
    /*public void Play()
    {
        _startMenu.SetActive(false);
        FindObjectOfType<PlayerBehaviour>().Play();
    }*/

    public void ShowFinishWindow()
    {
        _finishWindow.SetActive(true);
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if(next < SceneManager.sceneCountInBuildSettings)
        {
            _coinManager.SaveToProgress();

            SceneManager.LoadScene(next);
        }        
    }
}
