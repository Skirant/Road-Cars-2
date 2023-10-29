using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int NumberOfCoins;
    public int CoinsAddInLevel;
    public int RealCoinInThisLevel;
    [SerializeField] TextMeshProUGUI _textCoin;
    [SerializeField] TextMeshProUGUI _bonusCoin;

    [SerializeField] int _priceCoin = 10;

    public Button noThanksButton;

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

        noThanksButton.interactable = false;
    }

    public void MoneyUpdate()
    {
        _textCoin.text = FormatWeight(RealCoinInThisLevel);
    }

    public void UpdateDisplayedCoins(float multiplier)
    {
        int displayCoins = Mathf.RoundToInt(CoinsAddInLevel * multiplier);
        _bonusCoin.text = FormatWeight(displayCoins);
    }

    private string FormatWeight(int value)
    {
        float floatValue = value;
        string formattedValue;
        float absValue = Mathf.Abs(floatValue);

        if (absValue >= 1000000000)
        {
            floatValue /= 1000000;
            formattedValue = floatValue.ToString("F0") + "B";
        }
        else if (absValue >= 1000000)
        {
            floatValue /= 1000000;
            formattedValue = floatValue.ToString("F0") + "M";
        }
        else if (absValue >= 100000)
        {
            floatValue /= 100000;
            formattedValue = floatValue.ToString("F0") + "KK";
        }
        else if (absValue >= 1000)
        {
            floatValue /= 1000;
            formattedValue = floatValue.ToString("F0") + "K";
        }
        else
        {
            formattedValue = floatValue.ToString("F0");
        }

        return formattedValue;
    }

    public void SaveToProgress()
    {
        Progress.Instance.Coins = NumberOfCoins;
    }
}
