using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int NumberOfCoins;
    public int CoinsAddInLevel;
    public int RealCoinInThisLevel;
    [SerializeField] TextMeshProUGUI _textCoin;
    [SerializeField] TextMeshProUGUI _bonusCoin;

    [SerializeField] int _priceCoin = 10;

    private void Start()
    {
        NumberOfCoins = Progress.Instance.Coins;
        RealCoinInThisLevel = Progress.Instance.Coins;
        MoneyUpdate();
    }

    public void UpdatePriceCoin(float multiplier)
    {
        _priceCoin = Mathf.RoundToInt(_priceCoin * multiplier);
    }

    public void AddOne()
    {
        RealCoinInThisLevel += _priceCoin;
        CoinsAddInLevel += _priceCoin;
        MoneyUpdate();
    }   

    public void SpendMoney(int value)
    {
        if(RealCoinInThisLevel >= value)
        {
            NumberOfCoins -= value;
            RealCoinInThisLevel -= value;
        }        
        MoneyUpdate();
    }

    public void UpdateBonusCoinText()
    {
        _bonusCoin.text = FormatWeight(Mathf.RoundToInt(CoinsAddInLevel * _multiplier));
    }

    public void MultiplyMoney(float multiplier)
    {
        CoinsAddInLevel = Mathf.RoundToInt(CoinsAddInLevel * multiplier);
        NumberOfCoins += CoinsAddInLevel;
        _textCoin.text = FormatWeight(NumberOfCoins);

        SaveToProgress();
    }

    public void NoThanks()
    {        
        NumberOfCoins += CoinsAddInLevel;
        _textCoin.text = FormatWeight(NumberOfCoins);

        SaveToProgress();
    }

    public void MoneyUpdate()
    {
        _textCoin.text = FormatWeight(RealCoinInThisLevel);
    }

    private string FormatWeight(int value)
    {
        float floatValue = value;
        string formattedValue;
        float absValue = Mathf.Abs(floatValue);

        if (absValue >= 1000000000)
        {
            floatValue /= 1000000;
            formattedValue = floatValue.ToString("F1") + "B";
        }
        else if (absValue >= 1000000)
        {
            floatValue /= 1000000;
            formattedValue = floatValue.ToString("F1") + "M";
        }
        else if (absValue >= 100000)
        {
            floatValue /= 100000;
            formattedValue = floatValue.ToString("F1") + "KK";
        }
        else if (absValue >= 1000)
        {
            floatValue /= 1000;
            formattedValue = floatValue.ToString("F1") + "K";
        }
        else
        {
            formattedValue = floatValue.ToString("F1");
        }

        return formattedValue;
    }

    public void SaveToProgress()
    {
        Progress.Instance.Coins = NumberOfCoins;
    }
}
