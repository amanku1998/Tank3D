using UnityEngine;

public class TankModel 
{
    private TankController tankController;

    public TankModel()
    {
        // Initialize the tank model properties here if needed
    }

    public void SetController(TankController _tankController)
    {
        tankController = _tankController;
    }

}
