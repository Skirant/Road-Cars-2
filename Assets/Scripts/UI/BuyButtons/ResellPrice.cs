using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResellPrice : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI multiplierText;
    public Button increaseButton;

    private float price = 100.0f;
    private float multiplier = 1.0f;

    private void Start()
    {
        UpdateTexts();
        increaseButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        multiplier += 0.05f;
        price *= 2.5f;
        UpdateTexts();
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
        multiplierText.text = "x" + multiplier.ToString("F2");
    }
}
