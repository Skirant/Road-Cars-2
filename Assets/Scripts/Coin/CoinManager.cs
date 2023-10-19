using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int NumberOfCoins;
    public int CoinsAddInLevel;
    public int RealCoinInThisLevel;
    [SerializeField] TextMeshProUGUI _textCoin;

    private void Start()
    {
        NumberOfCoins = Progress.Instance.Coins;
        RealCoinInThisLevel = Progress.Instance.Coins;
        MoneyUpdate();
    }

    public void AddOne()
    {
        RealCoinInThisLevel += 1;
        CoinsAddInLevel += 1;
        MoneyUpdate();
    }

    public void SaveToProgress()
    {
        Progress.Instance.Coins = NumberOfCoins;
    }

    public void SpendMoney(int value)
    {
        NumberOfCoins -= value;
        MoneyUpdate();
    }

    public void MultiplyMoney(float multiplier)
    {
        CoinsAddInLevel = Mathf.RoundToInt(CoinsAddInLevel * multiplier);
        NumberOfCoins += CoinsAddInLevel;
        _textCoin.text = NumberOfCoins.ToString();
    }

    public void MoneyUpdate()
    {
        _textCoin.text = RealCoinInThisLevel.ToString();
    }
}
