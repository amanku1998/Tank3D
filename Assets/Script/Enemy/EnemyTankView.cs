using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
