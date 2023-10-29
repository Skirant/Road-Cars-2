using System.Diagnostics;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [Header("$CoinManager")]
    public int Coins;

    [Header("$PlayerModifier")]
    public int Weight;

    [Space]
    public int HealthBarrir;
    public int LevelNumber;

    [Header("$CarMass")]
    public float CarMassPrice;
    public float CarMassMultiplier;

    [Header("$ResellPrice")]
    public float ResellPricePrice;
    public float ResellPriceMultiplier;

    public static Progress Instance;

    private void OnEnable()
    {
        CarMass.OnUpdateProgressData += UpdateProgressData;
        ResellPrice.OnUpdateResellData += UpdateResellData;
    }

    private void OnDisable()
    {
        CarMass.OnUpdateProgressData -= UpdateProgressData;
        ResellPrice.OnUpdateResellData -= UpdateResellData;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateProgressData(float price, float multiplier)
    {
        CarMassPrice = price;
        CarMassMultiplier = multiplier;
    }
    private void UpdateResellData(float priceResellPrice, float multiplierResellPrice)
    {
        ResellPricePrice = priceResellPrice;
        ResellPriceMultiplier = multiplierResellPrice;
    }
}
