using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<Transform> spawnPoints;

    public GameObject specialPrefab;

    [SerializeField] int _numberOfPoints = 10;
    [SerializeField] int _distance = 1;

    int lastSpawnedPrefabIndex = -1;

    [System.Obsolete]
    void Start()
    {
        CreateSpawnPoints(_distance, _numberOfPoints); // Создает _numberOfPoints точек спавна через каждые 15 единиц
        SpawnRandomPrefabsAtAllPoints();
    }

    void CreateSpawnPoints(float distance, int numberOfPoints)
    {
        for (int i = 1; i < numberOfPoints; i++)
        {
            Vector3 spawnPointPosition = new Vector3(0, 0, i * distance);
            Transform spawnPoint = new GameObject("SpawnPoint").transform;
            spawnPoint.position = spawnPointPosition;
            spawnPoints.Add(spawnPoint);
        }
    }

    [System.Obsolete]
    void SpawnRandomPrefabsAtAllPoints()
    {
        if (prefabs.Count == 0 || spawnPoints.Count == 0)
        {
            Debug.LogWarning("Необходимо добавить префабы и точки спавна.");
            return;
        }

        int currentLevel = Progress.Instance.PlayerInfo.LevelNumber; // Используйте LevelNumber из Progress
        bool isEvenLevel = currentLevel % 2 == 0;
        int specialPrefabSpawnPointIndex = isEvenLevel ? Random.Range(3, spawnPoints.Count) : -1;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            GameObject randomPrefab;

            if (isEvenLevel && specialPrefab != null && i == specialPrefabSpawnPointIndex)
            {
                randomPrefab = specialPrefab;
            }
            else
            {
                int randomPrefabIndex = GetRandomPrefabIndex();
                while (!isEvenLevel && prefabs[randomPrefabIndex] == specialPrefab)
                {
                    randomPrefabIndex = GetRandomPrefabIndex();
                }
                randomPrefab = prefabs[randomPrefabIndex];
                lastSpawnedPrefabIndex = randomPrefabIndex;
            }

            Vector3 spawnPosition = new Vector3(randomPrefab.transform.position.x, randomPrefab.transform.position.y, spawnPoint.position.z);
            Instantiate(randomPrefab, spawnPosition, spawnPoint.rotation);
        }
    }

    int GetRandomPrefabIndex()
    {
        int randomIndex = -1;
        float probability = 1f;

        while (randomIndex == -1 || (randomIndex == lastSpawnedPrefabIndex && Random.value > probability))
        {
            randomIndex = Random.Range(0, prefabs.Count);
            probability *= 0.5f;
        }

        return randomIndex;
    }
}

