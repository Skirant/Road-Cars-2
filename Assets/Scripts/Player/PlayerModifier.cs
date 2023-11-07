using TMPro;
using UnityEngine;
using YG;

public class PlayerModifier : MonoBehaviour
{
    [SerializeField] int _weight;
    [SerializeField] int _weightInGame = 0;
    [SerializeField] TextMeshProUGUI _textWeight;
    //[SerializeField] int _damage;
    //[SerializeField] int _healthPlayer;

    public int Weight
    {
        get { return _weight; }
    }

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

    public void AddWeight(int value)
    {
        _weight += value;

        TextWeight();
    }

    public void AddWeightGate(int value)
    {
        _weightInGame += value;
        SaveProgressWeightPlayer();
    }

    public void SetWeight(int value)
    {
        if (value <= 0)
        {
            _weight = 1;
        }
        else
        {
            _weight = value;
            _weightInGame = value;
        }

        TextWeight();
    }

    [System.Obsolete]
    public int HitBarrier(int barrierHealth)
    {
        int damageToBarrier = Mathf.Min(_weight, barrierHealth);
        return damageToBarrier;
    }

    [System.Obsolete]
    public void TakeDamage(int damage)
    {
        _weight -= damage;
        if (_weight <= 0)
        {
            Die();
        }
    }

    [System.Obsolete]
    private void Die()
    {
        FindObjectOfType<GameManager>().ShowFinishWindow();
        Destroy(gameObject);
    }

    private void TextWeight()
    {
        _textWeight.text = FormatWeight(_weight) + " T";
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

    public void SaveProgressWeightPlayer()
    {
        Progress.Instance.PlayerInfo.Weight = _weightInGame;
    }

    private void LoadFromProgress()
    {
        SetWeight(Progress.Instance.PlayerInfo.Weight);
        //print(Progress.Instance.PlayerInfo.Weight);
    }
}
