using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class CoinRewardManager : MonoBehaviour
{
    [SerializeField] private GameObject PileOfCoinParent;
    [SerializeField] private TextMeshProUGUI Counter;
    [SerializeField] private Vector3[] InitialPos;
    [SerializeField] private Quaternion[] InitialRotation;
    [SerializeField] private int CoinNo;

    private int coinCount;

    private void Start()
    {
        InitialPos = new Vector3[CoinNo];
        InitialRotation= new Quaternion[CoinNo];

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
        Reset();

        var delay = 0f;

        PileOfCoinParent.SetActive(true);

        for(int i = 0; i<PileOfCoinParent.transform.childCount; i++)
        {
            var child = PileOfCoinParent.transform.GetChild(i);

            child.DOScale(endValue: 1f, duration: 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            // Измените координаты конечной точки, чтобы монеты исчезали раньше
            child.GetComponent<RectTransform>().DOAnchorPos(endValue: new Vector2(x: 219f, y: 476f), duration: 0.8f).SetDelay(delay + 0.5f).SetEase(Ease.InBack);

            child.DORotate(Vector3.zero, duration: 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash)/*.OnComplete(CountCoinsByComplete)*/;

            // Уменьшите задержку перед исчезновением монет
            child.DOScale(endValue: 0f, duration: 0.3f).SetDelay(delay + 1.3f).SetEase(Ease.OutBack);

            delay += 0.1f;
        }

        //StartCoroutine(routine: CountCoins(coinNo: 10));
    }

    /*void CountCoinsByComplete()
    {
        coinCount += 1;
        Counter.text = coinCount.ToString();
    }*/

    /*IEnumerator CountCoins(int coinNo)
    {
        yield return new WaitForSecondsRealtime(time: 0.5f);

        var timer = 0f;

        for(int i = 0; i<coinNo; i++)
        {
            PlayerPrefs.SetInt("CountCoin", PlayerPrefs.GetInt(key: "CountCoin") + 1);

            Counter.text = PlayerPrefs.GetInt(key: "CountCoin").ToString();

            timer += 0.01f;

            yield return new WaitForSecondsRealtime(timer);
        }
    }*/
}
