using TMPro;
using UnityEngine;

public class Barrier: MonoBehaviour
{
    [SerializeField] GameObject _bricksEffectPrefab;
    [SerializeField] int healthBarrier;

    [SerializeField] private TextMeshProUGUI _healthText;

    private void Start()
    {
        SetWeight(Progress.Instance.HealthBarrir);
        UpdateHP();
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
        if(value <= 0)
        {
            healthBarrier = 1;
        }
        else
        {
            healthBarrier = value;
        }        
    }

    public void SaveToProgress()
    {
        Progress.Instance.HealthBarrir = healthBarrier;
    }
}
