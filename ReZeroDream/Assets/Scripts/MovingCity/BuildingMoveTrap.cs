using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoveTrap : MonoBehaviour
{
    private BuildingMoveController buildingMoveControl;
    private void Start()
    {
        buildingMoveControl = FindObjectOfType<BuildingMoveController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        buildingMoveControl.isMove = true;
    }

    private void OnTriggerStay(Collider other)
    {
        buildingMoveControl.isMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        buildingMoveControl.isMove = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        buildingMoveControl.isMove = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        buildingMoveControl.isMove = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        buildingMoveControl.isMove = false;
    }
}
