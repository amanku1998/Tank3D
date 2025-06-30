using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool m_UseRelativeRotation = true;
    private Quaternion m_RelativeRotation;

    private void Start()
    {
        //Finds The rotation Of The Canvas
        m_RelativeRotation = transform.parent.localRotation;
    }

    private void Update()
    {
        if (m_UseRelativeRotation)
            //Set Rotation
            transform.rotation = m_RelativeRotation;
    }
}
