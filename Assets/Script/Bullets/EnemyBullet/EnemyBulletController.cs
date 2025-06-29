using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController
{
    private EnemyBulletModel enemyBulletModel;
    private EnemyBulletView enemyBulletView;

    public EnemyBulletController(EnemyBulletModel model, EnemyBulletView prefab, Vector3 direction, Vector3 spawnPos)
    {
        this.enemyBulletModel = model;
        this.enemyBulletView = GameObject.Instantiate(prefab, spawnPos, Quaternion.LookRotation(direction));
        enemyBulletModel.SetController(this);
        enemyBulletView.SetController(this);

        enemyBulletView.Launch(direction, model.GetSpeed());
    }

    public void OnHit()
    {
        enemyBulletView.Explode(enemyBulletModel.GetDamage()); ;
    }
}
