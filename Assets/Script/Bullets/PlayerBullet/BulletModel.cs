
public class BulletModel
{
    private BulletController controller;

    public float speed;
    public float damage;
    public BulletTypes bulletType;

    public BulletModel(float speed, float damage, BulletTypes type)
    {
        this.speed = speed;
        this.damage = damage;
        this.bulletType = type;
    }

    public void SetController(BulletController ctrl) => controller = ctrl;
    public float GetSpeed() => speed;
    public float GetDamage() => damage;
}
