using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;

public class CarMass : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI AdGet;

    public Button increaseButton;

    public PlayerModifier playerModifier;
    public CoinManager coinManager;

    private float priceCarMass = 100.0f;
    private float multiplierCarMass = 1.1f;

    // Добавляем счетчик нажатий
    private int clickCount = 0;

    private void OnEnable()
    {
        Progress.OnLoadComplete += LoadFromProgress;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        Progress.OnLoadComplete -= LoadFromProgress;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void Start()
    {
        LoadFromProgress();
    }

    private void OnButtonClick()
    {      
        if (coinManager.RealCoinInThisLevel >= (int)priceCarMass)
        {
            clickCount++;

            if (clickCount == 1)
            {
                coinManager.SpendMoney((int)priceCarMass);

                UpdatePrice();
                TriggerOnUpdateProgressDataCarMass();
                UpdateTexts();
            }
            else if (clickCount == 2)
            {
                coinManager.SpendMoney((int)priceCarMass);

                priceText.gameObject.SetActive(false);
                AdGet.gameObject.SetActive(true);
                TriggerOnUpdateProgressDataCarMass();
                UpdatePrice();
                UpdateTexts();
            }
        }

        if (clickCount >= 3)
        {
            YandexGame.RewVideoShow(0);            
        }
    }

    void Rewarded(int id)
    {
        UpdatePrice();
        TriggerOnUpdateProgressDataCarMass();
        UpdateTexts();
    }

    public void UpdatePrice()
    {
        playerModifier.AddWeight((int)multiplierCarMass);
        playerModifier.AddWeightGate((int)multiplierCarMass);
        multiplierCarMass *= 1.05f;
        priceCarMass *= 1.05f;

        print(clickCount);
    }

    private string FormatPrice(float value)
    {
        float floatValue = value;
        float absValue = Mathf.Abs(value);
        string formattedValue;

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

    private void UpdateTexts()
    {
        priceText.text = FormatPrice(priceCarMass);
        multiplierText.text = "+" + FormatPrice(multiplierCarMass);
    }

    public void TriggerOnUpdateProgressDataCarMass()
    {
        Progress.Instance.PlayerInfo.CarMassPrice = priceCarMass;
        Progress.Instance.PlayerInfo.CarMassMultiplier = multiplierCarMass;
    }

    public void LoadFromProgress()
    {
        priceCarMass = Progress.Instance.PlayerInfo.CarMassPrice;
        multiplierCarMass = Progress.Instance.PlayerInfo.CarMassMultiplier;

        // Удаление всех слушателей перед добавлением нового
        increaseButton.onClick.RemoveAllListeners();
        increaseButton.onClick.AddListener(OnButtonClick);

        UpdateTexts();
    }
}
