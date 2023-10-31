using TMPro;
using UnityEngine;

public class Barrier: MonoBehaviour
{
    [SerializeField] GameObject _bricksEffectPrefab;
    [SerializeField] int healthBarrier;

    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private GameManager gameManager;

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

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        PlayerModifier playerModifier = other.attachedRigidbody.GetComponent<PlayerModifier>();

        if (playerModifier)
        {
            int playerDamage = playerModifier.HitBarrier(healthBarrier);
            healthBarrier -= playerModifier.Weight;
            if (healthBarrier <= 0)
            {
                Destroy(gameObject);
                Instantiate(_bricksEffectPrefab, transform.position, transform.rotation);
            }
            playerModifier.TakeDamage(playerDamage);

            UpdateHP();

            SaveToProgress();
        }
    }

    private void UpdateHP()
    {
        _healthText.text = healthBarrier.ToString();
    }

    public void SetWeight(int value)
    {
        if (value <= 0)
        {
            healthBarrier = gameManager._level * 100;
        }
        else
        {
            healthBarrier = value;
        }

        SaveToProgress();
    }

    public void SaveToProgress()
    {
        if(healthBarrier > 0)
        {
            Progress.Instance.PlayerInfo.HealthBarrir = healthBarrier;
        }
        else
        {
            Progress.Instance.PlayerInfo.HealthBarrir = gameManager._level*100;
        }
    }

    public void LoadFromProgress()
    {
        SetWeight(Progress.Instance.PlayerInfo.HealthBarrir);
        UpdateHP();
    }
}
