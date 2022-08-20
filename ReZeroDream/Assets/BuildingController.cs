using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public BuildingMovement[] buildingMove;
    private CarController carControl;

    //bool change = false;

    void Start()
    {
        carControl = FindObjectOfType<CarController>();
    }

    void Update()
    {
        if (carControl.isActive)
        {
            if (buildingMove[0].isActive) return;
            for (int i = 0; i < buildingMove.Length; i++)
            {
                buildingMove[i].isActive = true;
            }

        }
        else if (!carControl.isActive)
        {
            if (!buildingMove[0].isActive) return;
            for (int i = 0; i < buildingMove.Length; i++)
            {
                buildingMove[i].isActive = false;
            }
        }
    }
}
