using UnityEngine;
using YG;
using static Progress;

public class DropSaveGame : MonoBehaviour
{
    public GameObject GameObject;
    public void SaveDALITE()
    {
        GameObject.SetActive(false);

        YandexGame.ResetSaveProgress();
        DataHolder.ProgressInstance.MySave();
    }
}
