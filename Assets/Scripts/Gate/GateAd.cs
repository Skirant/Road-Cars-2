using UnityEngine;
using YG;

public class GateAd : MonoBehaviour
{
    [SerializeField] int _value;

    [SerializeField] DefrmationType _defrmationType;

    [SerializeField] Progress progress;

    [SerializeField] float DisableRadius = 5f;

    private void Awake()
    {
        progress = FindAnyObjectByType<Progress>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerImprovement playerImprovement = other.attachedRigidbody.GetComponent<PlayerImprovement>();

        YandexGame.RewVideoShow(0);

        if (playerImprovement)
        {
            // Получаем текущий вес игрока
            int currentWeight = playerImprovement.GetPlayerModifier().Weight;

            // Находим следующую машину
            CarPrefabInfo nextCar = null;
            for (int i = 0; i < playerImprovement.carPrefabs.Length; i++)
            {
                if (currentWeight < playerImprovement.carPrefabs[i].weight)
                {
                    nextCar = playerImprovement.carPrefabs[i];
                    break;
                }
            }

            // Если есть следующая машина, добавляем вес, необходимый для перехода к ней
            if (nextCar != null)
            {
                int weightToAdd = nextCar.weight - currentWeight;
                playerImprovement.GetPlayerModifier().AddWeight(weightToAdd);
            }
        }


        PlayerModifier playerModifier = other.attachedRigidbody.GetComponent<PlayerModifier>();

        if (playerModifier)
        {
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
}
