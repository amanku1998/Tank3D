using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSelection : MonoBehaviour
{
    public TankSpwaner tankSpwaner;
    public void BlueTankSelected()
    {
        tankSpwaner.CreateTank(TankTypes.BlueTank);
        this.gameObject.SetActive(false);
    }

    public void RedTankSelected()
    {
        tankSpwaner.CreateTank(TankTypes.RedTank);
        this.gameObject.SetActive(false);
    }

    public void GreenTankSelected()
    {
        tankSpwaner.CreateTank(TankTypes.GreenTank);
        this.gameObject.SetActive(false);
    }
}
