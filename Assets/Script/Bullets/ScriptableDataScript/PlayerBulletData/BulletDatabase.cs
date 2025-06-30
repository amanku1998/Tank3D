using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletDatabase", menuName = "ScriptableObject/Bullet Database")]
public class BulletDatabase : ScriptableObject
{
    public List<BulletData> bullets;

    public BulletData GetBulletData(BulletTypes type)
    {
        return bullets.Find(b => b.bulletType == type);
    }
}
