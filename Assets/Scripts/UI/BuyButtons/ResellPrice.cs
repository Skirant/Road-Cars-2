using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;
using static CarMass;

public class ResellPrice : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI AdGet;

    public Button increaseButton;

    public CoinManager coinManager;

    private float priceResellPrice = 100.0f;
    private float multiplierResellPrice = 1.1f;

    // ��������� ������� �������
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

    private void LoadFromProgress()
    {
        priceResellPrice = Progress.Instance.PlayerInfo.ResellPricePrice;
        multiplierResellPrice = Progress.Instance.PlayerInfo.ResellPriceMultiplier;

        // ������� ��� ������������ ��������� ������� ����� ����������� ������
        increaseButton.onClick.RemoveAllListeners();
        increaseButton.onClick.AddListener(OnButtonClick);

        UpdateTexts();
    }

    private void OnButtonClick()
    {
        if (coinManager.RealCoinInThisLevel >= (int)priceResellPrice)
        {
            clickCount++;

            if (clickCount == 1)
            {
                coinManager.SpendMoney((int)priceResellPrice);
                coinManager.UpdatePriceCoin(multiplierResellPrice);
                multiplierResellPrice += 0.1f;
                priceResellPrice *= 2.5f;
                TriggerOnUpdateProgressDataResellPrice();
                UpdateTexts();
            }
            else if (clickCount == 2)
            {
                coinManager.SpendMoney((int)priceResellPrice);
                coinManager.UpdatePriceCoin(multiplierResellPrice);

                priceText.gameObject.SetActive(false);
                AdGet.gameObject.SetActive(true);

                multiplierResellPrice += 0.1f;
                priceResellPrice *= 2.5f;
                TriggerOnUpdateProgressDataResellPrice();
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
        multiplierResellPrice += 0.1f;
        priceResellPrice *= 2.5f;
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
            formattedValue = floatValue.ToString("F0");
        }

        return formattedValue;
    }

    private void UpdateTexts()
    {
        priceText.text = FormatPrice(priceResellPrice);
        multiplierText.text = "x" + multiplierResellPrice.ToString("F2");
    }

    public void TriggerOnUpdateProgressDataResellPrice()
    {
        Progress.Instance.PlayerInfo.ResellPricePrice = priceResellPrice;
        Progress.Instance.PlayerInfo.ResellPriceMultiplier = multiplierResellPrice;
    }
}
