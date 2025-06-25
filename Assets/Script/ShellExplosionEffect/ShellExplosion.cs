using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask tankMask;
    public ParticleSystem explosionParticles;
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;

    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

        }

        explosionParticles.transform.parent = null;

        explosionParticles.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.duration);
        Destroy(gameObject);
    }

}
