using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTankView : MonoBehaviour
{
    private EnemyTankController enemyTankController;
    private float movement;
    private float rotation;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private MeshRenderer[] childs;
    [SerializeField] private AudioClip driving;
    [SerializeField] private AudioClip idle;
    [SerializeField] private AudioSource source;

    public Transform firePoint; // assign in prefab

    [SerializeField] private NavMeshAgent agent;
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

    public void SetController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }
    //Use this method here because bullet is destroyed and the coroutine is not working 
    public void ApplyExplosionForce(Rigidbody rb, float force, Vector3 explosionPos, float radius)
    {
        rb.isKinematic = false;
        rb.AddExplosionForce(force, explosionPos, radius);
        StartCoroutine(ResetAfterImpact(rb));
    }

    private IEnumerator ResetAfterImpact(Rigidbody targetRb)
    {
        yield return new WaitForSeconds(0.5f);
        if (targetRb != null && targetRb.gameObject.activeInHierarchy)
        {
            targetRb.velocity = Vector3.zero;
            targetRb.angularVelocity = Vector3.zero;
            targetRb.isKinematic = true;
        }
    }

    public NavMeshAgent GetAgent() => agent;
}
