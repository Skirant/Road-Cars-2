using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    [SerializeField] int _weight;
    [SerializeField] int _damage;

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
        _weight = value;
    }

    [System.Obsolete]
    public void HitBarrier()
    {
        if (_weight > 0)
        {
            _weight -= _damage;
        }
        else
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
