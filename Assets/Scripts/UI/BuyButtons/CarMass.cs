using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CarMass : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI AdGet;

    public Button increaseButton;

    public PlayerModifier playerModifier;
    public CoinManager coinManager;

    private float price = 100.0f;
    private float multiplier = 1.0f;

    // Добавляем счетчик нажатий
    private int clickCount = 0;

    private void Start()
    {
        UpdateTexts();
        increaseButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Увеличиваем счетчик при каждом нажатии
        clickCount++;

        if (coinManager.RealCoinInThisLevel >= (int)price)
        {
            coinManager.SpendMoney((int)price);
            playerModifier.AddWeight((int)multiplier);
            multiplier *= 2.25f;
            price *= 2f;
            UpdateTexts();
        }

        // Если нажатие второе, переключаем видимость TextMeshProUGUI
        if (clickCount == 2)
        {
            priceText.gameObject.SetActive(false);
            AdGet.gameObject.SetActive(true);

            // Сбрасываем счетчик нажатий
            clickCount = 0;
        }
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
        priceText.text = FormatPrice(price);
        multiplierText.text = "+" + FormatPrice(multiplier);
    }

    private void AdYandex()
    {

    }
}
