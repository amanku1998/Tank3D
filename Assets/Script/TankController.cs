using UnityEngine;

public class TankController 
{
    private TankModel tankModel;
    private TankView tankView;
    private Rigidbody rb;

    private CameraController cameraController;

    public TankController(TankModel _tankModel, TankView _tankView , CameraController _cameraController)
    {
        tankModel = _tankModel;
        cameraController = _cameraController;
        tankView = GameObject.Instantiate<TankView>(_tankView);
        rb = tankView.GetRigidbody();

        tankModel.SetController(this);
        tankView.SetController(this);

        tankView.ChangeColor(tankModel.color);
        cameraController.SetTarget(tankView.transform);
    }

    public void Move(float movement, float movementSpeed)
    {
        rb.velocity = tankView.transform.forward * movement * movementSpeed;
    }

    public void Rotate(float rotate, float rotateSpeed)
    {
        Vector3 vector = new Vector3(0, rotate * rotateSpeed);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public float GetTankSpeed()
    {
        return tankModel.movementSpeed;
    }
    
    public float GetTankRotationSpeed()
    {
        return tankModel.rotationSpeed;
    }
}
