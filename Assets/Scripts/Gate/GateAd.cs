using UnityEngine;
using YG;
using YG.Example;

public class GateAd : MonoBehaviour
{
    [SerializeField] int _value;

    [SerializeField] DefrmationType _defrmationType;

    [SerializeField] float DisableRadius = 5f;

    private PlayerImprovement playerImprovement;
    private PlayerModifier playerModifier;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void OnTriggerEnter(Collider other)
    {
        playerImprovement = other.attachedRigidbody.GetComponent<PlayerImprovement>();
        playerModifier = other.attachedRigidbody.GetComponent<PlayerModifier>();

        YandexGame.RewVideoShow(0);
    }

    void Rewarded(int id)
    {
        if (playerImprovement)
        {
            // �������� ������� ��� ������
            int currentWeight = playerImprovement.GetPlayerModifier().Weight;

            // ������� ��������� ������
            CarPrefabInfo nextCar = null;
            for (int i = 0; i < playerImprovement.carPrefabs.Length; i++)
            {
                if (currentWeight < playerImprovement.carPrefabs[i].weight)
                {
                    nextCar = playerImprovement.carPrefabs[i];
                    break;
                }
            }

            // ���� ���� ��������� ������, ��������� ���, ����������� ��� �������� � ���
            if (nextCar != null)
            {
                int weightToAdd = nextCar.weight - currentWeight;
                playerImprovement.GetPlayerModifier().AddWeight(weightToAdd);
            }
        }

        if (playerModifier)
        {
            playerModifier.AddWeight(_value);
        }

        Destroy(gameObject);
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
}

