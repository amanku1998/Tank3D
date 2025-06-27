using UnityEngine;

public class EnemyTankModel 
{
    private EnemyTankController enemyTankController;

    public float movementSpeed;
    public float rotationSpeed;
    public EnemyTankType enemyTankType;

    public Material color;

    public float attackRange;
    public EnemyBulletTypes enemyBulletType;

    public EnemyTankModel(float movementSpeed, float rotationSpeed, EnemyTankType enemyTankType, Material color)
    {
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
        this.enemyTankType = enemyTankType;
        this.color = color;

        // Set bullet type based on tank type
        switch (enemyTankType)
        {
            case EnemyTankType.ASSAULT_TANK:
                enemyBulletType = EnemyBulletTypes.ASSAULT_TANK_BULLET;
                attackRange = 10f;
                break;
            case EnemyTankType.SCOUT_TANK:
                enemyBulletType = EnemyBulletTypes.SCOUT_TANK_BULLET;
                attackRange = 15f;
                break;
            case EnemyTankType.ARTILLERY_TANK:
                enemyBulletType = EnemyBulletTypes.ARTILLERY_TANK_BULLET;
                attackRange = 25f;
                break;
        }
    }

    public void SetController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }

    // Getters to know the current bullet type
    public EnemyBulletTypes GetBulletType() => enemyBulletType;

    public float GetAttackRange() => attackRange;
}
