using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRotateTrap : MonoBehaviour
{
    private BuildingRotateController buildingRotateControl;
    private void Start()
    {
        buildingRotateControl = FindObjectOfType<BuildingRotateController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        buildingRotateControl.isRotate = true;
    }

    private void OnTriggerStay(Collider other)
    {
        buildingRotateControl.isRotate = true;
    }

    private void OnTriggerExit(Collider other)
    {
        buildingRotateControl.isRotate = false;
    }
}
