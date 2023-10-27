using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public GameObject prefab;
    public List<Transform> spawnPoints;

    [SerializeField] float _randomXSpawn;
    [SerializeField] float _randomZSpawn;

    [SerializeField] int _minCoinSpawn = 0;
    [SerializeField] int _maxCoinSpawnn = 3;

    void Start()
    {
        SpawnRandomPrefabsAtAllPoints();
    }

    void SpawnRandomPrefabsAtAllPoints()
    {
        if (prefab == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("Необходимо добавить префаб и точки спавна.");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            int numPrefabsToSpawn = Random.Range(_minCoinSpawn, _maxCoinSpawnn);
            for (int i = 0; i < numPrefabsToSpawn; i++)
            {
                float randomX = Random.Range(-_randomXSpawn, _randomXSpawn); // В бок
                float randomZ = Random.Range(-_randomZSpawn, _randomZSpawn); // Вперед

                Vector3 spawnPosition = spawnPoint.position + new Vector3(randomX, 0, randomZ);
                spawnPosition.y = 1; // Высота спавна

                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
