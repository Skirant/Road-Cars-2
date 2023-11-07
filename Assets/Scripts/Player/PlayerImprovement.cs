using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CarPrefabInfo
{
    public GameObject carPrefab;
    public int weight;
}

public class PlayerImprovement : MonoBehaviour
{
    public CarPrefabInfo[] carPrefabs;
    private GameObject currentCar;
    private PlayerModifier playerModifier;
    public GameObject standardCar;
    public Slider progressSlider; // Новый слайдер

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
        CarPrefabInfo nextCarInfo = null;
        CarPrefabInfo nowCar = null;

        for (int i = 0; i < carPrefabs.Length; i++)
        {
            if (weight >= carPrefabs[i].weight)
            {
                selectedCarInfo = carPrefabs[i];
                if (i + 1 < carPrefabs.Length)
                {
                    nextCarInfo = carPrefabs[i + 1];
                    nowCar = carPrefabs[i];
                }
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

        if (nextCarInfo != null)
        {
            progressSlider.minValue = nowCar.weight;
            progressSlider.maxValue = nextCarInfo.weight;
            progressSlider.value = weight;
        }
        else
        {
            progressSlider.value = progressSlider.maxValue;
        }
    }

    private void ChangeCarModel(GameObject newModelPrefab)
    {
        foreach (Transform child in standardCar.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newModel = Instantiate(newModelPrefab, Vector3.zero, Quaternion.identity);
        newModel.transform.SetParent(standardCar.transform, false);
    }

    public PlayerModifier GetPlayerModifier()
    {
        return playerModifier;
    }
}
