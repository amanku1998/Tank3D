using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBulletDataBase", menuName = "ScriptableObject/EnemyBulletDataBase")]
public class EnemyBulletDataBase : ScriptableObject
{
    public List<EnemyBulletData> enemyBullets;

    public EnemyBulletData GetBulletData(EnemyBulletTypes type)
    {
        return enemyBullets.Find(b => b.bulletType == type);
    }
}
