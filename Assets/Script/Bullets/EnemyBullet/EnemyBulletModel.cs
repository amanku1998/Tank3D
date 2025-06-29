using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletModel
{
    private EnemyBulletController controller;

    public float speed;
    public float damage;
    public EnemyBulletTypes enemyBulletType;

    public EnemyBulletModel(float speed, float damage, EnemyBulletTypes type)
    {
        this.speed = speed;
        this.damage = damage;
        this.enemyBulletType = type;
    }

    public void SetController(EnemyBulletController ctrl) => controller = ctrl;
    public float GetSpeed() => speed;
    public float GetDamage() => damage;
}
