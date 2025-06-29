using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int playerNumber = 1;              // Used to identify the different players.
    public Rigidbody shell;                   // Prefab of the shell.
    public Transform fireTransform;           // A child of the tank where the shells are spawned.
    public Slider aimSlider;                  // A child of the tank that displays the current launch force.

    public float minLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    public float maxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    public float maxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.

    private string fireButton;                // The input axis that is used for launching shells.
    private float currentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float chargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool fired;                       // Whether or not the shell has been launched with this button press.

    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }
    private void Start()
    {
        // The fire axis is based on the player number.
        fireButton = "Fire" + playerNumber;

        // The rate that the launch force charges up is the range of possible forces by the max charge time. Or Speed = distance / time.
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }
    private void Update()
    {
        // The slider should have a default value of the minimum launch force.
        aimSlider.value = minLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched
        if (currentLaunchForce >= maxLaunchForce && !fired)
        {
            //use the max force and launch the shell.
            currentLaunchForce = maxLaunchForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed
        else if (Input.GetButtonDown(fireButton))
        {
            //reset the fired flag and reset the launch force.
            fired = false;
            currentLaunchForce = minLaunchForce;
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet
        else if (Input.GetButton(fireButton) && !fired)
        {
            // Increment the launch force and update the slider.
            currentLaunchForce += chargeSpeed * Time.deltaTime;

            aimSlider.value = currentLaunchForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet
        else if (Input.GetButtonUp(fireButton) && !fired)
        {
            //launch the shell.
            Fire();
        }
    }

    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = currentLaunchForce * fireTransform.forward;

        // Reset the launch force.  This is a precaution in case of missing button events.
        currentLaunchForce = minLaunchForce;
    }
}
