using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyTank
    {
        public float movementSpeed;
        public float rotationSpeed;
        public EnemyTankType tankType;
        public Material color;
        public float attackRange;
        public float maxHealth;
        public float rapidFireRange;
    }

    public List<EnemyTank> enemyTankList;
    public List<Transform> spawnPoints;

    public EnemyTankView enemyTankView;

    [SerializeField] private EnemyBulletDataBase enemyBulletDatabase;

    public void OnStartGame()
    {
        CreateTank(); 
    }

    public void CreateTank()
    {
        int randomIndex = Random.Range(0, enemyTankList.Count);
        //int randomIndex = 2;
        EnemyTank randomData = enemyTankList[randomIndex];

        EnemyTankModel enemyTankModel = new EnemyTankModel(
            randomData.movementSpeed,
            randomData.rotationSpeed,
            randomData.tankType,
            randomData.color,
            randomData.attackRange,
            randomData.maxHealth,
            randomData.rapidFireRange
        );

        EnemyTankController enemyTankController = new EnemyTankController(enemyTankModel, 
            enemyTankView, enemyBulletDatabase);

        // Assign random position
        Transform spawnPos = GetRandomSpawnPoint();
        enemyTankController.GetTankView().transform.position = spawnPos.position;
        enemyTankController.GetTankView().transform.rotation = spawnPos.rotation;

        // Assign spawner to health script
        TankHealth health = enemyTankController.GetTankView().GetComponent<TankHealth>();
        if (health != null)
        {
            health.SetSpawner(this);
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index];
    }
}
