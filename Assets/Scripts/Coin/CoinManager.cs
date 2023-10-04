using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int NumberOfCoins;
    [SerializeField] TextMeshProUGUI _textCoin;

    private void Start()
    {
        NumberOfCoins = Progress.Instance.Coins;
        MoneyUpdate();
    }

    public void AddOne()
    {
        NumberOfCoins += 1;
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

    public void MoneyUpdate()
    {
        _textCoin.text = NumberOfCoins.ToString();
    }
}
