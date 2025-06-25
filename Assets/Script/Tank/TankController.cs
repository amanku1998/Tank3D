using UnityEngine;

public class TankController 
{
    private TankModel tankModel;
    private TankView tankView;
    private Rigidbody rb;

    private CameraController cameraController;
    private BulletDatabase bulletDatabase;

    private float currentLaunchForce;
    private float minLaunchForce = 3f;
    private float maxLaunchForce = 5f;
    private float maxChargeTime = 0.35f;
    private float chargeSpeed;
    private bool fired;

    public TankController(TankModel _tankModel, TankView _tankView , CameraController _cameraController)
    {
        tankModel = _tankModel;
        tankView = GameObject.Instantiate<TankView>(_tankView);
        rb = tankView.GetRigidbody();
        cameraController = _cameraController;

        tankModel.SetController(this);
        tankView.SetController(this);

        tankView.ChangeColor(tankModel.color);
        cameraController.SetTarget(tankView.transform);

        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        currentLaunchForce = minLaunchForce;

        bulletDatabase = Resources.Load<BulletDatabase>("BulletDatabase");
    }

    public void HandleShootInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fired = false;
            currentLaunchForce = minLaunchForce;
            tankView.aimSlider.value = currentLaunchForce;
        }
        else if (Input.GetKey(KeyCode.Space) && !fired)
        {   
            currentLaunchForce += chargeSpeed * Time.deltaTime;
            Debug.Log("currentLaunchForce :" + currentLaunchForce + "maxLaunchForce :" + maxLaunchForce);

            // Clamp to max value so the force doesn't exceed
            if (currentLaunchForce >= maxLaunchForce)
            {
                currentLaunchForce = maxLaunchForce;
            }

            tankView.aimSlider.value = currentLaunchForce;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !fired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        fired = true;

        BulletData data = bulletDatabase.GetBulletData(tankModel.GetBulletType());

        BulletModel bulletModel = new BulletModel(
            currentLaunchForce * data.speed,
            data.damage,
            data.bulletType
        );

        new BulletController(bulletModel, data.bulletPrefab, tankView.firePoint.forward, tankView.firePoint.position);

        currentLaunchForce = minLaunchForce;
        tankView.aimSlider.value = currentLaunchForce;
    }

    public void Move(float movement)
    {
        rb.velocity = tankView.transform.forward * movement * tankModel.movementSpeed;
    }

    public void Rotate(float rotate)
    {
        Vector3 vector = new Vector3(0, rotate * tankModel.rotationSpeed);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public TankModel GetTankModel() => tankModel;
    public TankView GetTankView() => tankView;
}
