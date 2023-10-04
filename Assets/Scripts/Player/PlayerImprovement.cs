using UnityEngine;

[System.Serializable]
public class CarPrefabInfo
{
    public GameObject carPrefab;
    public int weight;
}

public class PlayerImprovement : MonoBehaviour
{
    public CarPrefabInfo[] carPrefabs; // массив с префабами машин
    private GameObject currentCar; // ссылка на текущую машину
    private PlayerModifier playerModifier;
    public GameObject standardCar;

    private void Start()
    {
        playerModifier = Object.FindFirstObjectByType<PlayerModifier>();
        UpdateCarPrefab();
    }

    private void Update()
    {
        UpdateCarPrefab();
    }

    private void UpdateCarPrefab()
    {
        int weight = playerModifier.Weight;
        CarPrefabInfo selectedCarInfo = null;

        foreach (CarPrefabInfo carInfo in carPrefabs)
        {
            if (weight >= carInfo.weight)
            {
                selectedCarInfo = carInfo;
            }
            else
            {
                break;
            }
        }

        if (selectedCarInfo != null && currentCar != selectedCarInfo.carPrefab)
        {
            currentCar = selectedCarInfo.carPrefab;
            ChangeCarModel(currentCar);
        }
    }

    private void ChangeCarModel(GameObject newModelPrefab)
    {
        // Удаляем все текущие дочерние объекты машины
        foreach (Transform child in standardCar.transform)
        {
            Destroy(child.gameObject);
        }

        // Добавляем новую модель машины
        GameObject newModel = Instantiate(newModelPrefab, Vector3.zero, Quaternion.identity);
        newModel.transform.SetParent(standardCar.transform, false);
    }
}
