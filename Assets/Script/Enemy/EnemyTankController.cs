using UnityEngine;

public class EnemyTankController
{
    private EnemyTankModel enemyTankModel;
    private EnemyTankView enemyTankView;
    private Rigidbody rb;

    private EnemyBulletDataBase enemyBulletDatabase;

    private float currentLaunchForce = 6f;
    private float fireTimer;
    private Transform player;

    public EnemyTankController(EnemyTankModel _enemyTankModel, EnemyTankView _enemyTankView, EnemyBulletDataBase _enemyBulletDatabase)
    {
        enemyTankModel = _enemyTankModel;
        enemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);
        rb = enemyTankView.GetRigidbody();

        enemyTankModel.SetController(this);
        enemyTankView.SetController(this);

        enemyTankView.ChangeColor(enemyTankModel.color);
        enemyBulletDatabase = _enemyBulletDatabase;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Start AI update loop
        enemyTankView.gameObject.AddComponent<EnemyTankUpdateLoop>().Init(this);
    }

    public void Update()
    {
        if (player == null) return;

        fireTimer += Time.deltaTime;

        switch (enemyTankModel.enemyTankType)
        {
            case EnemyTankType.ASSAULT_TANK:
                MoveTowardPlayer();
                TryFire(1.5f); // slower fire rate
                break;

            case EnemyTankType.SCOUT_TANK:
                FlankPlayer();
                TryFire(0.5f); // rapid fire
                break;

            case EnemyTankType.ARTILLERY_TANK:
                KeepDistance();
                TryFire(2.5f); // slow, long range
                break;
        }
    }

    private void MoveTowardPlayer()
    {
        //Vector3 direction = (player.position - enemyTankView.transform.position).normalized;
        //Move(1f);
        //RotateToPlayer(direction);

        float distance = Vector3.Distance(enemyTankView.transform.position, player.position);
        float attackRange = enemyTankModel.GetAttackRange(); // From model

        Vector3 direction = (player.position - enemyTankView.transform.position).normalized;
        RotateToPlayer(direction);

        if (distance > attackRange)
        {
            Move(1f); // Keep moving until close enough
        }
        else
        {
            Move(0f); // Stop when within range
        }
    }

    private void KeepDistance()
    {
        float distance = Vector3.Distance(enemyTankView.transform.position, player.position);
        if (distance < 20f)
        {
            Vector3 dir = (enemyTankView.transform.position - player.position).normalized;
            Move(1f); // back away
            RotateToPlayer(-dir);
        }
        else
        {
            Move(0f); // stop
        }

        RotateToPlayer(player.position - enemyTankView.transform.position);
    }

    private void FlankPlayer()
    {
        Vector3 toPlayer = player.position - enemyTankView.transform.position;
        Vector3 flankDir = Vector3.Cross(Vector3.up, toPlayer).normalized;
        Move(1f);
        RotateToPlayer(flankDir);
    }

    private void RotateToPlayer(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.RotateTowards(enemyTankView.transform.rotation, lookRotation, enemyTankModel.rotationSpeed * Time.deltaTime);
        enemyTankView.transform.rotation = rotation;
    }

    private void TryFire(float cooldown)
    {
        //if (fireTimer >= cooldown)
        //{
        //    fireTimer = 0;
        //    Fire();
        //}

        float distance = Vector3.Distance(enemyTankView.transform.position, player.position);
        float attackRange = enemyTankModel.GetAttackRange();

        if (distance > attackRange) return; //Don’t fire if player is too far

        if (fireTimer >= cooldown)
        {
            fireTimer = 0;
            Fire();
        }
    }

    private void Fire()
    {
       // fired = true;

        EnemyBulletData data = enemyBulletDatabase.GetBulletData(enemyTankModel.GetBulletType());

        EnemyBulletModel enemyBulletModel = new EnemyBulletModel(
            currentLaunchForce * data.speed,
            data.damage,
            data.bulletType
        );

        new EnemyBulletController(enemyBulletModel, data.bulletPrefab, enemyTankView.firePoint.forward, enemyTankView.firePoint.position);
    }

    public void Move(float movement)
    {
        rb.velocity = enemyTankView.transform.forward * movement * enemyTankModel.movementSpeed;
    }

    public EnemyTankModel GetTankModel() => enemyTankModel;
    public EnemyTankView GetTankView() => enemyTankView;
}
