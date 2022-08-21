using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRotateController : MonoBehaviour
{
    public BuildingMovement buildingMove;
    public bool isRotate = false;

    void Update()
    {
        if (isRotate)
        {
            if (buildingMove.isRotate) return;
            {
                buildingMove.isRotate = true;
            }
        }
        else if (!isRotate)
        {
            if (!buildingMove.isRotate) return;
            {
                buildingMove.isRotate = false;
            }
        }
    }
}
