using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoveController : MonoBehaviour
{
    public BuildingMovement buildingMove;
    public bool isMove = false;

    void Update()
    {
        if (isMove)
        {
            if (buildingMove.isMove) return;
            {
                buildingMove.isMove = true;
            }
        }
        else if (!isMove)
        {
            if (!buildingMove.isMove) return;
            {
                buildingMove.isMove = false;
            }
        }
    }
}
