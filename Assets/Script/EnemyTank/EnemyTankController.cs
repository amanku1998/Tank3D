using UnityEngine;
using UnityEngine.AI;

public class EnemyTankController
{
    private EnemyTankModel enemyTankModel;
    private EnemyTankView enemyTankView;
    private Rigidbody rb;

    private EnemyBulletDataBase enemyBulletDatabase;

    private float currentLaunchForce = 6f;
    private float fireTimer;
    private Transform player;

    private NavMeshAgent navAgent;

    public EnemyTankController(EnemyTankModel _enemyTankModel, EnemyTankView _enemyTankView, EnemyBulletDataBase _enemyBulletDatabase)
    {
        enemyTankModel = _enemyTankModel;
        enemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);
        rb = enemyTankView.GetRigidbody();
        TankHealth health = enemyTankView.GetComponent<TankHealth>();
        if (health != null)
        {
            health.SetMaxHealthEnemyTank(enemyTankModel.maxHealth); // or whatever value you want for enemy max health
        }

        navAgent = enemyTankView.GetAgent();
        navAgent.updateRotation = false;
        navAgent.speed = enemyTankModel.movementSpeed;
        navAgent.angularSpeed = enemyTankModel.rotationSpeed;

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
                // slower fire rate
                TryFire(enemyTankModel.rapidFireRange); // slower fire rate
                break;

            case EnemyTankType.SCOUT_TANK:
                FlankPlayer();
                // rapid fire
                float randomRange = Random.Range(0.65f, enemyTankModel.rapidFireRange);
                TryFire(randomRange);
                break;

            case EnemyTankType.ARTILLERY_TANK:
                KeepDistance();
                // slow, long range
                TryFire(enemyTankModel.rapidFireRange);
                break;
        }

        AlignRotationWithAgent();
    }

    private void AlignRotationWithAgent()
    {
        if (navAgent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 direction = navAgent.velocity.normalized;
            RotateToPlayer(direction);
        }
    }

    private void MoveTowardPlayer()
    {
        float distance = Vector3.Distance(enemyTankView.transform.position, player.position);
        float attackRange = enemyTankModel.GetAttackRange(); // From model

        Vector3 direction = (player.position - enemyTankView.transform.position).normalized;
        RotateToPlayer(direction);

        if (distance > attackRange)
        {
            MoveTo(player.position); // Keep moving until close enough
        }
        else
        {
            navAgent.ResetPath(); // Stop moving when in range
        }
    }

    private void KeepDistance()
    {
        float distance = Vector3.Distance(enemyTankView.transform.position, player.position);
        float minDistance = 20; // e.g. 10f (too close)
        float maxDistance = enemyTankModel.GetAttackRange(); // e.g. 30f (ideal firing range)

        Vector3 toPlayer = (player.position - enemyTankView.transform.position).normalized;

        if (distance < minDistance)
        {
            //when Too close then retreat
            Vector3 retreatPos = enemyTankView.transform.position - toPlayer * 20f;
            MoveTo(retreatPos);
        }
        else if (distance > maxDistance)
        {
            //when Too far
            Vector3 advancePos = player.position - toPlayer * (maxDistance - 5f); // Stop short of max
            MoveTo(advancePos);
        }
        else
        {
            //In ideal range then hold position
            navAgent.ResetPath();
        }

        RotateToPlayer(player.position - enemyTankView.transform.position);
    }

    private void FlankPlayer()
    {
        Vector3 toPlayer = player.position - enemyTankView.transform.position;
        Vector3 flankDir = Vector3.Cross(Vector3.up, toPlayer).normalized;

        Vector3 sideTarget = player.position + flankDir * 5f;
        MoveTo(sideTarget);

        RotateToPlayer(player.position - enemyTankView.transform.position);
    }

    private void RotateToPlayer(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.RotateTowards(
            enemyTankView.transform.rotation,
            lookRotation,
            enemyTankModel.rotationSpeed * Time.deltaTime
        );
        enemyTankView.transform.rotation = rotation;
    }

    private void TryFire(float cooldown)
    {
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
        EnemyBulletData data = enemyBulletDatabase.GetBulletData(enemyTankModel.GetBulletType());

        EnemyBulletModel enemyBulletModel = new EnemyBulletModel(
            currentLaunchForce * data.speed,
            data.damage,
            data.bulletType
        );

        new EnemyBulletController(enemyBulletModel, data.bulletPrefab, enemyTankView.firePoint.forward, enemyTankView.firePoint.position);
    }

    public void MoveTo(Vector3 destination)
    {
        if (navAgent != null && navAgent.isOnNavMesh)
        {
            //updating destination if the current path is too old or far off
            if (Vector3.Distance(navAgent.destination, destination) > 1f)
            {
                navAgent.SetDestination(destination);
            }
        }
    }

    public EnemyTankModel GetTankModel() => enemyTankModel;
    public EnemyTankView GetTankView() => enemyTankView;
}
