using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;
    private float movement;
    private float rotation;

    public Rigidbody rb;

    public MeshRenderer[] childs;

    private void Update()
    {
        Movement();

        if(movement != 0)
        {
            tankController.Move(movement, tankController.GetTankSpeed());
        }

        if (rotation != 0)
        {
            tankController.Rotate(rotation, tankController.GetTankRotationSpeed());
        }
    }

    private void Movement()
    {
        movement = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
    }

    public void SetController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public void ChangeColor(Material color)
    {
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].material = color;
        }
    }
}
