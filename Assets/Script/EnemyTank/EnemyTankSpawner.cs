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


    public void CreateTank(EnemyTankType tankType)
    {
        //int randomIndex = Random.Range(0, enemyTankList.Count);
        // EnemyTank randomData = enemyTankList[randomIndex];

        EnemyTank tankData = enemyTankList.Find(t => t.tankType == tankType);
        if (tankData == null)
        {
            Debug.LogWarning($"No data found for tank type: {tankType}");
            return;
        }

        EnemyTankModel enemyTankModel = new EnemyTankModel(
            tankData.movementSpeed,
            tankData.rotationSpeed,
            tankData.tankType,
            tankData.color,
            tankData.attackRange,
            tankData.maxHealth,
            tankData.rapidFireRange
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
