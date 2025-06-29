using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankUpdateLoop : MonoBehaviour
{
    public EnemyTankController controller;

    public void Init(EnemyTankController tankController)
    {
        controller = tankController;
        //Debug.Log("EnemyTankUpdateLoop initialized for " + controller);
    }
    private void Update()
    {
        controller?.Update();
    }
}
