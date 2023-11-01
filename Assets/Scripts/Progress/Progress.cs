using UnityEngine;
using YG;

[System.Serializable]
public class PlayerInfo
{
    [Header("$CoinManager")]
    public int Coins;
    public int _price;

    [Header("$PlayerModifier")]
    public int Weight;

    [Header("$Barrier")]
    public int HealthBarrir;

    [Header("$GameManager")]
    public int LevelNumber;

    [Header("$CarMass")]
    public float CarMassPrice;
    public float CarMassMultiplier;

    [Header("$ResellPrice")]
    public float ResellPricePrice;
    public float ResellPriceMultiplier;
}

public class Progress : MonoBehaviour
{
    public delegate void LoadCompleteHandler();
    public static event LoadCompleteHandler OnLoadComplete;

    public PlayerInfo PlayerInfo;

    public static Progress Instance;

    public class DataHolder
    {
        public static Progress ProgressInstance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            DataHolder.ProgressInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    public void MySave()
    {
        YandexGame.savesData.Coins = PlayerInfo.Coins;
        YandexGame.savesData._price = PlayerInfo._price;
        YandexGame.savesData.Weight = PlayerInfo.Weight;
        YandexGame.savesData.HealthBarrir = PlayerInfo.HealthBarrir;
        YandexGame.savesData.LevelNumber = PlayerInfo.LevelNumber;
        YandexGame.savesData.CarMassPrice = PlayerInfo.CarMassPrice;
        YandexGame.savesData.CarMassMultiplier = PlayerInfo.CarMassMultiplier;
        YandexGame.savesData.ResellPricePrice = PlayerInfo.ResellPricePrice;
        YandexGame.savesData.ResellPriceMultiplier = PlayerInfo.ResellPriceMultiplier;

        YandexGame.SaveProgress();
    }

    public void GetLoad()
    {
        PlayerInfo.Coins = YandexGame.savesData.Coins;
        PlayerInfo._price = YandexGame.savesData._price;
        PlayerInfo.Weight = YandexGame.savesData.Weight;
        PlayerInfo.HealthBarrir = YandexGame.savesData.HealthBarrir;
        PlayerInfo.LevelNumber = YandexGame.savesData.LevelNumber;
        PlayerInfo.CarMassPrice = YandexGame.savesData.CarMassPrice;
        PlayerInfo.CarMassMultiplier = YandexGame.savesData.CarMassMultiplier;
        PlayerInfo.ResellPricePrice = YandexGame.savesData.ResellPricePrice;
        PlayerInfo.ResellPriceMultiplier = YandexGame.savesData.ResellPriceMultiplier;

        OnLoadComplete?.Invoke();
    }
}

