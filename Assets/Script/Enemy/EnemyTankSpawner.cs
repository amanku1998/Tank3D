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
    }

    public List<EnemyTank> enemyTankList;
    public List<Transform> spawnPoints;

    public EnemyTankView enemyTankView;

    [SerializeField] private EnemyBulletDataBase enemyBulletDatabase;

    private void Start()
    {
        //CreateTank();
    }

    public void OnStartGame()
    {
        CreateTank(); // 👈 Call only now
    }

    //public void CreateTank(EnemyTankType enemyTankType)
    //{
    //    if (enemyTankType == EnemyTankType.ASSAULT_TANK)
    //    {
    //        EnemyTankModel enemyTankModel = new EnemyTankModel(enemyTankList[0].movementSpeed, enemyTankList[0].rotationSpeed, enemyTankList[0].tankType, enemyTankList[0].color);
    //        EnemyTankController enemyTankController = new EnemyTankController(enemyTankModel, enemyTankView);
    //    }
    //    else if (enemyTankType == EnemyTankType.SCOUT_TANK)
    //    {
    //        EnemyTankModel enemyTankModel = new EnemyTankModel(enemyTankList[1].movementSpeed, enemyTankList[1].rotationSpeed, enemyTankList[1].tankType, enemyTankList[1].color);
    //        EnemyTankController enemyTankController = new EnemyTankController(enemyTankModel, enemyTankView);
    //    }
    //    else if (enemyTankType == EnemyTankType.ARTILLERY_TANK)
    //    {
    //        EnemyTankModel enemyTankModel = new EnemyTankModel(enemyTankList[2].movementSpeed, enemyTankList[2].rotationSpeed, enemyTankList[2].tankType, enemyTankList[2].color);
    //        EnemyTankController enemyTankController = new EnemyTankController(enemyTankModel, enemyTankView);
    //    }
    //}

    public void CreateTank()
    {
        //int randomIndex = Random.Range(0, enemyTankList.Count);
        int randomIndex = 0;
        EnemyTank randomData = enemyTankList[randomIndex];

        EnemyTankModel enemyTankModel = new EnemyTankModel(
            randomData.movementSpeed,
            randomData.rotationSpeed,
            randomData.tankType,
            randomData.color
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
