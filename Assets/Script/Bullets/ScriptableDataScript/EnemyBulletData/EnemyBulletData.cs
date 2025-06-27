using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBulletData", menuName = "ScriptableObject/EnemyBulletData")]
public class EnemyBulletData : ScriptableObject
{
    public EnemyBulletTypes bulletType;
    public EnemyBulletView bulletPrefab;
    public float speed;
    public float damage;
    public Material bulletMaterial;
}
