using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TankWaveManager : MonoBehaviour
{
    public TankWaveData[] waves;
    public EnemyTankSpawner spawner;
    public TextMeshProUGUI waveText;
    public AudioSource waveStartSound;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private Queue<EnemyTankType> remainingToSpawn = new Queue<EnemyTankType>();

    public void StartWave()
    {
        StartCoroutine(StartWaveRoutine());
    }

    private IEnumerator StartWaveRoutine()
    {
        waveText.text = $"Wave {currentWaveIndex + 1}";
        waveText.gameObject.SetActive(true);
        waveStartSound?.Play();

        yield return new WaitForSeconds(2f);
        waveText.gameObject.SetActive(false);

        TankWaveData wave = waves[currentWaveIndex];

        List<EnemyTankType> allTypes = new List<EnemyTankType>();

        foreach (var tankInfo in wave.tanksInWave)
        {
            for (int i = 0; i < tankInfo.spawnCount; i++)
                allTypes.Add(tankInfo.tankType);
        }

        // Shuffle for variety
        for (int i = 0; i < allTypes.Count; i++)
        {
            EnemyTankType temp = allTypes[i];
            int rand = Random.Range(i, allTypes.Count);
            allTypes[i] = allTypes[rand];
            allTypes[rand] = temp;
        }

        // Split into initial spawn and remaining queue
        int spawnNow = Mathf.Min(wave.initialSpawnCount, allTypes.Count);
        for (int i = 0; i < spawnNow; i++)
            SpawnTank(allTypes[i]);

        for (int i = spawnNow; i < allTypes.Count; i++)
            remainingToSpawn.Enqueue(allTypes[i]);
    }

    private void SpawnTank(EnemyTankType type)
    {
        spawner.CreateTank(type);
        enemiesAlive++;
    }

    // Call this from each enemy tank's OnDeath
    public void OnTankDestroyed()
    {
        enemiesAlive--;

        if (remainingToSpawn.Count > 0)
        {
            EnemyTankType next = remainingToSpawn.Dequeue();
            SpawnTank(next);
        }
        else if (enemiesAlive == 0)
        {
            // Finished this wave
            currentWaveIndex++;

            if (currentWaveIndex < waves.Length)
                StartCoroutine(StartWaveRoutine());
            else
                Debug.Log("All waves completed!");
        }
    }
}
