using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CoinManager : MonoBehaviour
{
    public int NumberOfCoins;
    public int CoinsAddInLevel;
    public int RealCoinInThisLevel;
    [SerializeField] TextMeshProUGUI _textCoin;
    [SerializeField] TextMeshProUGUI _bonusCoin;

    [SerializeField] int _priceCoin = 10;

    [Space]
    public NoThanksButton noThanksButton;

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
        MoneyUpdate();
    }

    public void UpdatePriceCoin(float multiplier)
    {
        _priceCoin = Mathf.RoundToInt(_priceCoin * multiplier);

        SaveToProgress();
    }

    public void AddOne()
    {
        RealCoinInThisLevel += _priceCoin;
        CoinsAddInLevel += _priceCoin;
        MoneyUpdate();
    }

    public void SpendMoney(int value)
    {
        if (RealCoinInThisLevel >= value)
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

        ButtonOff();

        SaveToProgress();
    }

    public void ButtonOff()
    {
        noThanksButton.ButtonOff();
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
            formattedValue = floatValue.ToString("F2") + "M";
        }
        else if (absValue >= 100000)
        {
            floatValue /= 100000;
            formattedValue = floatValue.ToString("F2") + "KK";
        }
        else if (absValue >= 1000)
        {
            floatValue /= 1000;
            formattedValue = floatValue.ToString("F2") + "K";
        }
        else
        {
            formattedValue = floatValue.ToString("F0");
        }

        return formattedValue;
    }

    public void SaveToProgress()
    {
        Progress.Instance.PlayerInfo.Coins = NumberOfCoins;
        Progress.Instance.PlayerInfo._price = _priceCoin;
    }

    public void LoadFromProgress()
    {
        NumberOfCoins = Progress.Instance.PlayerInfo.Coins;
        RealCoinInThisLevel = Progress.Instance.PlayerInfo.Coins;
        _priceCoin = Progress.Instance.PlayerInfo._price;

        MoneyUpdate();
    }
}
