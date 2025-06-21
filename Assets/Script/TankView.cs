using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetController(TankController _tankController)
    {
        tankController = _tankController;
    }

}
