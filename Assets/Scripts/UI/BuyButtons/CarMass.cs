using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

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
    }

    private void OnDisable()
    {
        Progress.OnLoadComplete -= LoadFromProgress;
    }

    private void Start()
    {
        LoadFromProgress();
    }

    private void OnButtonClick()
    {
        // Увеличиваем счетчик при каждом нажатии
        clickCount++;

        if (coinManager.RealCoinInThisLevel >= (int)priceCarMass)
        {
            // Если нажатие второе, переключаем видимость TextMeshProUGUI
            if (clickCount == 2)
            {
                priceText.gameObject.SetActive(false);
                AdGet.gameObject.SetActive(true);

                coinManager.SpendMoney((int)priceCarMass);
                UpdatePrice();
            }
            else if(clickCount <= 1)
            {
                coinManager.SpendMoney((int)priceCarMass);
                UpdatePrice();
            }

            TriggerOnUpdateProgressDataCarMass();

            UpdateTexts();
        }

        if (clickCount >= 3)
        {
            //YandexGame.RewVideoShow(0);

            UpdatePrice();

            TriggerOnUpdateProgressDataCarMass();

            UpdateTexts();
        }
    }

    public void UpdatePrice()
    {
        playerModifier.AddWeight((int)multiplierCarMass);
        playerModifier.AddWeightGate((int)multiplierCarMass);
        multiplierCarMass *= 1.025f;
        priceCarMass *= 1.025f;
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
            formattedValue = floatValue.ToString("F0");
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
