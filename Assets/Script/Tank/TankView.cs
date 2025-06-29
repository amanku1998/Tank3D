using UnityEngine;
using UnityEngine.UI;

public class TankView : MonoBehaviour
{
    private TankController tankController;
    private float movement;
    private float rotation;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private MeshRenderer[] childs;
    [SerializeField] private AudioClip driving;
    [SerializeField] private AudioClip idle;
    [SerializeField]private AudioSource source;

    public Transform firePoint; // assign in prefab
    public Slider aimSlider;  // A child of the tank that displays the current launch force.

    private void Update()
    {
        GetInput();

        if(movement != 0)
        {
            tankController.Move(movement);
        }

        if (rotation != 0)
        {
            tankController.Rotate(rotation);
        }

        PlayMovementAudio();
        //
        tankController.HandleShootInput();
    }

    private void PlayMovementAudio()
    {
        if (rb.velocity != Vector3.zero && source.clip != driving)
        {
            source.clip = driving;
            source.Play();
        }
        else if (rb.velocity == Vector3.zero && source.clip != idle)
        {
            source.clip = idle;
            source.Play();
        }
    }
    private void GetInput()
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
