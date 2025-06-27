using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "ScriptableObject/BulletData")]
public class BulletData : ScriptableObject
{
    public BulletTypes bulletType;
    public BulletView bulletPrefab;
    public float speed;
    public float damage;
    public Material bulletMaterial;
}
