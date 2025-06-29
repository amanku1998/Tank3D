using UnityEngine;

public class TankModel 
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeed;
    public TankTypes tankType;

    public Material color;
    /// Bullet properties
    public BulletTypes bulletType;

    public TankModel(float movementSpeed, float rotationSpeed, TankTypes tankType, Material color)
    {
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
        this.tankType = tankType;
        this.color = color;

        // Set bullet type based on tank type
        switch (tankType)
        {
            case TankTypes.GreenTank:
                bulletType = BulletTypes.GreenBullet;
                break;
            case TankTypes.BlueTank:
                bulletType = BulletTypes.BlueBullet;
                break;
            case TankTypes.RedTank:
                bulletType = BulletTypes.RedBullet;
                break;
        }
    }

    public void SetController(TankController _tankController)
    {
        tankController = _tankController;
    }

    // Getters to know the current bullet type
    public BulletTypes GetBulletType() => bulletType;
}
