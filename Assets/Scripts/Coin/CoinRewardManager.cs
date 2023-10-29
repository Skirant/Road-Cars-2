using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinRewardManager : MonoBehaviour
{
    [SerializeField] private GameObject PileOfCoinParent;
    [SerializeField] private TextMeshProUGUI Counter;
    [SerializeField] private Vector3[] InitialPos;
    [SerializeField] private Quaternion[] InitialRotation;
    [SerializeField] private int CoinNo;

    [Header("Х & Y ImageCoin")]
    //кординаты монеты
    [SerializeField] private float xOffsetImageCoin; [SerializeField] private float yOffsetImageCoin;

    private bool update = true;

    private void Start()
    {
        InitialPos = new Vector3[CoinNo];
        InitialRotation = new Quaternion[CoinNo];

        PlayerPrefs.DeleteAll();

        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            InitialPos[i] = PileOfCoinParent.transform.GetChild(i).position;
            InitialRotation[i] = PileOfCoinParent.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            PileOfCoinParent.transform.GetChild(i).position = InitialPos[i];
            PileOfCoinParent.transform.GetChild(i).rotation = InitialRotation[i];
        }
    }

    public void RewardPileOfCoind(int noCoin)
    {
        if (!update) return;

        Reset();

        var delay = 0f;

        PileOfCoinParent.SetActive(true);

        for(int i = 0; i<PileOfCoinParent.transform.childCount; i++)
        {
            var child = PileOfCoinParent.transform.GetChild(i);

            child.DOScale(endValue: 1f, duration: 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            // Измените координаты конечной точки, чтобы монеты исчезали раньше
            RectTransform canvasRectTransform = PileOfCoinParent.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            Vector2 rightTopCorner = new Vector2(canvasRectTransform.rect.width / 2f, canvasRectTransform.rect.height / 2f);

            Vector2 finalPosition = rightTopCorner + new Vector2(xOffsetImageCoin, yOffsetImageCoin);
            child.GetComponent<RectTransform>().DOAnchorPos(endValue: finalPosition, duration: 0.8f).SetDelay(delay + 0.5f).SetEase(Ease.InBack);

            child.DORotate(Vector3.zero, duration: 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash)/*.OnComplete(CountCoinsByComplete)*/;

            // Уменьшите задержку перед исчезновением монет
            child.DOScale(endValue: 0f, duration: 0.3f).SetDelay(delay + 1.3f).SetEase(Ease.OutBack);

            delay += 0.1f;
        }

        update = false;
    }
}
