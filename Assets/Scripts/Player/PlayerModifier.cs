using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    [SerializeField] int _weight;
    //[SerializeField] int _damage;
    //[SerializeField] int _healthPlayer;

    public int Weight
    {
        get { return _weight; }
    }

    private void Start()
    {
        SetWeight(Progress.Instance.Weight);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            AddWeight(20);
        }
    }

    public void AddWeight(int value)
    {
        _weight += value;
        print("ZALUPA");
    }

    public void SetWeight(int value)
    {
        if(value <= 0) 
        {
            _weight = 1;
        }
        else
        {
            _weight = value;
        }
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
}
