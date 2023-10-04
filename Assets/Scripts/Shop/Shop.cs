using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] CoinManager _coinManager;

    PlayerModifier _playerModifier;

    [System.Obsolete]
    private void Start()
    {
        _playerModifier = FindObjectOfType<PlayerModifier>();
    }

    public void BuyWeight()
    {
        if(_coinManager.NumberOfCoins >= 20)
        {
            _coinManager.SpendMoney(20);
            Progress.Instance.Coins = _coinManager.NumberOfCoins;
            Progress.Instance.Weight += 25;
            _playerModifier.SetWeight(Progress.Instance.Weight);
        }
    }
}
