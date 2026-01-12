using UnityEngine;

public class TankModel 
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeed;
    public TankTypes tankType;

    public Material color;
    public TankModel(float movementSpeed, float rotationSpeed, TankTypes tankType, Material color)
    {
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
        this.tankType = tankType;
        this.color = color;
    }

    public void SetController(TankController _tankController)
    {
        tankController = _tankController;
    }

}
