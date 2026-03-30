using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject[] powerUpPrefabs;

    [System.Serializable]
public class Wave
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}
    public Wave[] waves;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave currentWave = waves[i];
            List<Transform> selectedPoints = GetRandomSpawnPoints(currentWave.numberOfRandomSpawnPoint);
            SpawnPowerUps(currentWave.numberOfPowerUp);
            yield return new WaitForSeconds(currentWave.delayStart);

            for (int e = 0; e < currentWave.totalSpawnEnemies; e++)
            {
                int pointIndex = Random.Range(0, selectedPoints.Count);
                Instantiate(enemyPrefab, selectedPoints[pointIndex].position, Quaternion.identity);
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }

            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return new WaitForSeconds(1);
            }

            Debug.Log("Wave " + (i + 1) + " Completed!");

            List<Transform> GetRandomSpawnPoints(int count)
            {
                List<Transform> allPoints = new List<Transform>(spawnPoints);
                List<Transform> selected = new List<Transform>();

                for (int i = 0; i < count && allPoints.Count > 0; i++)
                {
                    int randomIndex = Random.Range(0, allPoints.Count);
                    selected.Add(allPoints[randomIndex]);
                    allPoints.RemoveAt(randomIndex);

                }
                
                return selected;

            }
        }
    }
    void SpawnPowerUps(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int pointIndex = Random.Range(0, spawnPoints.Length);
            int prefabIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject selectedPrefab = powerUpPrefabs[prefabIndex];
            Instantiate(selectedPrefab, spawnPoints[pointIndex].position, Quaternion.identity);
        }
    }

    void RandomSpawn()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

}