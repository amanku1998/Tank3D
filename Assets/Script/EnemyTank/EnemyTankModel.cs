using UnityEngine;

public class EnemyTankModel 
{
    private EnemyTankController enemyTankController;

    public float movementSpeed;
    public float rotationSpeed;
    public EnemyTankType enemyTankType;

    public Material color;

    public float attackRange;
    public float maxHealth;
    public float rapidFireRange;

    public EnemyBulletTypes enemyBulletType;

    public EnemyTankModel(float movementSpeed, float rotationSpeed, EnemyTankType enemyTankType, Material color, float _attackRange, float _maxHealth, float _rapidFireRange)
    {
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
        this.enemyTankType = enemyTankType;
        this.color = color;
        this.attackRange = _attackRange;
        this.maxHealth = _maxHealth;
        this.rapidFireRange = _rapidFireRange;

        // Set bullet type based on tank type
        switch (enemyTankType)
        {
            case EnemyTankType.ASSAULT_TANK:
                enemyBulletType = EnemyBulletTypes.ASSAULT_TANK_BULLET;
                break;
            case EnemyTankType.SCOUT_TANK:
                enemyBulletType = EnemyBulletTypes.SCOUT_TANK_BULLET;
                break;
            case EnemyTankType.ARTILLERY_TANK:
                enemyBulletType = EnemyBulletTypes.ARTILLERY_TANK_BULLET;
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
