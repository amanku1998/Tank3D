using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    private BulletController controller;
    private Rigidbody rb;

    [Header("Explosion Settings")]
    public LayerMask tankMask;
    public ParticleSystem impactEffect;
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;

    public AudioClip shootingClip;
    public AudioClip explosionClip;
    public AudioSource source;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void SetController(BulletController bulletController)
    {
        controller = bulletController;
    }

    public void Launch(Vector3 direction, float speed)
    {
        SetShootingAudio();
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        controller?.OnHit();
    }

    public void Explode(float damage)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);

        foreach (var collider in colliders)
        {
            // Only apply explosion force to enemy tanks
            if (collider.CompareTag("EnemyTank"))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                EnemyTankView enemyTankView = collider.GetComponent<EnemyTankView>();

                // Apply damage if target has a health script
                TankHealth tankHealth = collider.GetComponent<TankHealth>();
                if (tankHealth != null)
                {
                    if (rb != null)
                    {
                        enemyTankView.ApplyExplosionForce(rb, explosionForce, transform.position, explosionRadius);
                    }

                    tankHealth.TakeDamage(damage);
                }

            }

            if (collider.CompareTag("Player"))
            {
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }

                // Apply damage if target has a health script
                TankHealth tankHealth = collider.GetComponent<TankHealth>();
                if (tankHealth != null)
                {
                    tankHealth.TakeDamage(damage);
                }
            }
        }
        SetExplosionAudio();
        impactEffect.transform.parent = null;
        impactEffect.Play();

        Destroy(impactEffect.gameObject, impactEffect.main.duration);
        Destroy(gameObject);
    }

    private void SetShootingAudio()
    {
        source.clip = shootingClip;
        source.Play();
    }
    private void SetExplosionAudio()
    {
        AudioSource impactAudio = impactEffect.GetComponent<AudioSource>();
        if (impactAudio != null)
        {
            impactAudio.clip = explosionClip;
            impactAudio.Play();
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
