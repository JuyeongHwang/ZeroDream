using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrap : MonoBehaviour
{


    private CarController carControl;
    private void Start()
    {
        carControl = FindObjectOfType<CarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        carControl.isActive = true;
    }

    private void OnTriggerStay(Collider other)
    {
        carControl.isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        carControl.isActive = false;
    }
}
