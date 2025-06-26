using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    [SerializeField]private float maxHealth = 100f;

    public Slider slider;                             // The slider to represent how much health the tank currently has.
    public Image fillImage;                           // The image component of the slider.
    public Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color zeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public GameObject explosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.

    private AudioSource explosionAudio;               // The audio source to play when the tank explodes.
    private ParticleSystem explosionParticles;        // The particle system the will play when the tank is destroyed.
    private float currentHealth;
    private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?

    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to the particle system on it.
        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();

        // Get a reference to the audio source on the instantiated prefab.
        explosionAudio = explosionParticles.GetComponent<AudioSource>();

        // Disable the prefab so it can be activated when it's required.
        explosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // When the tank is enabled, reset the tank's health and whether or not it's dead.
        currentHealth = maxHealth;
        m_Dead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }

    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        slider.value = currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (currentHealth <= 0f && !m_Dead)
        {
            Die();
        }
    }

    private void Die()
    {
        // Set the flag so that this function is only called once.
        m_Dead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on.
        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding.
        explosionParticles.Play();

        // Play the tank explosion sound effect.
        explosionAudio.Play();

        // Turn the tank off.
        gameObject.SetActive(false);
    }
}
