using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] backSpawnPoints;
    [SerializeField] private Transform[] otherSpawnPoints;

    [SerializeField] private GameObject[] enemies;

    [SerializeField] private float defaultSpawnTime = 10f;
    [SerializeField] private float minSpawnTime;

    [Range(0.0f, 1.0f)] [SerializeField] private float decreasingTimeFactor = 0.4f;

    private float startSpawnTime;
    private float currentSpawnTime;

    void Start()
    {
        startSpawnTime = defaultSpawnTime;
        currentSpawnTime = startSpawnTime;
    }

    void Update()
    {
        if (currentSpawnTime <= 0) SelectEnemyToSpawn();
        else currentSpawnTime -= Time.deltaTime;

        if (startSpawnTime > minSpawnTime) startSpawnTime -= Time.deltaTime * decreasingTimeFactor;

    }

    private void SelectEnemyToSpawn()
    {
        currentSpawnTime = startSpawnTime;

        int index = Random.Range(0, enemies.Length);
        SelectPoint(enemies[index].GetComponent<EnemySpawnSettings>().canBeSpawnedInOtherPoints, index);
    }

    private void SelectPoint(bool includeOtherPoints, int enemyIndex)
    {
        int pointIndex;
        int pointType;

        if (includeOtherPoints)
        {
            int chance = Random.Range(1,10);
            if (chance >= 3)
            {
                pointIndex = Random.Range(0, backSpawnPoints.Length);
                pointType = 1;
            }
            else
            {
                pointIndex = Random.Range(0, otherSpawnPoints.Length);
                pointType = 2;
            }
        }
        else
        {
            pointIndex = Random.Range(0, otherSpawnPoints.Length);
            pointType = 2;
        }

        SpawnEnemy(enemyIndex, pointIndex, pointType);

    }

    private void SpawnEnemy(int enemyIndex, int pointIndex, int pointType)
    {
        if (pointType == 1)
        {
            Instantiate(enemies[enemyIndex], backSpawnPoints[pointIndex].position, Quaternion.identity);
        }
        else
        {
            Instantiate(enemies[enemyIndex], otherSpawnPoints[pointIndex].position, Quaternion.identity);
        }
        
    }

}
