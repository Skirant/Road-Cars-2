using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] int _value;

    [SerializeField] DefrmationType _defrmationType;

    [SerializeField] GateAppearaence _gateAppearaence;

    public Progress progress;

    public float DisableRadius = 5f;

    private void OnValidate()
    {
        _gateAppearaence.UpdateVisual(_defrmationType, _value);
    }

    private void Awake()
    {
        progress = FindAnyObjectByType<Progress>();
    }

    private void Start()
    {
        GenerateRandomValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerModifier playerModifier = other.attachedRigidbody.GetComponent<PlayerModifier>();

        if (playerModifier)
        {
            float _valueSign = Mathf.Sign(_value); // ��������� ���� ����� :citation[1]

            if (_valueSign == 1f)
            {
                FindObjectOfType<AudioManager>().Play("Update");
            }
            else if (_valueSign == -1f)
            {
                FindObjectOfType<AudioManager>().Play("Decline");
            }

            playerModifier.AddWeight(_value);
        }

        Destroy(gameObject);

        if (other.CompareTag("Player"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, DisableRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.isTrigger && collider.gameObject.CompareTag("Gate"))
                {
                    collider.enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, DisableRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.isTrigger && collider.gameObject.CompareTag("Gate"))
                {
                    collider.enabled = true;
                }
            }
        }
    }

    private void GenerateRandomValue()
    {
        float randomPercentage = Random.Range(0f, 1f);
        int weightPercentage = progress.PlayerInfo.Weight;

        if (randomPercentage < 0.525f) // 52.5% �����������
        {
            _value = Random.Range(1, (int)(weightPercentage * 1.01f)); // �������� � ��������� �� 1 �� 100% �� Weight
        }
        else if (randomPercentage < 0.7f) // 17.5% �����������
        {
            _value = Random.Range(-(int)(weightPercentage * 1.01f), 0); // �������� � ��������� �� -100% �� Weight �� -1
        }
        else if (randomPercentage < 0.85f) // 15% �����������
        {
            _value = Random.Range(1, (int)(weightPercentage * 2.01f)); // �������� � ��������� �� 1 �� 200% �� Weight
        }
        else if (randomPercentage < 0.95f) // 10% �����������
        {
            _value = Random.Range(-(int)(weightPercentage * 2.01f), 0); // �������� � ��������� �� -200% �� Weight �� -1
        }
        else // 5% �����������
        {
            _value = Random.Range(1, (int)(weightPercentage * 3.01f)); // �������� � ��������� �� 1 �� 300% �� Weight
        }

        _gateAppearaence.UpdateVisual(_defrmationType, _value);
    }
}
