using UnityEngine;

[System.Serializable]
public class CarPrefabInfo
{
    public GameObject carPrefab;
    public int weight;
}

public class PlayerImprovement : MonoBehaviour
{
    public CarPrefabInfo[] carPrefabs; // ������ � ��������� �����
    private GameObject currentCar; // ������ �� ������� ������
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
        // ������� ��� ������� �������� ������� ������
        foreach (Transform child in standardCar.transform)
        {
            Destroy(child.gameObject);
        }

        // ��������� ����� ������ ������
        GameObject newModel = Instantiate(newModelPrefab, Vector3.zero, Quaternion.identity);
        newModel.transform.SetParent(standardCar.transform, false);
    }
}
