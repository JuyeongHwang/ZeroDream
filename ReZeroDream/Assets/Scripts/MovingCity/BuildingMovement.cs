using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    public bool isRotate = false;
    public Transform Offset;
    public float buildingRotateDegree = 20.0f;

    public bool isMove = false;
    public float buildingMoveSpeed = 1.0f;

    public Transform buildingStartPos;
    public Transform buildingEndPos;


    void Update()
    {
        if (isMove)
        {
            BuildingMoveAndCheck();
        }

        if (isRotate)
        {
            Offset.Rotate(Vector3.up * Time.deltaTime * buildingRotateDegree);
            //Offset.GetChild(0).transform.Rotate(Vector3.up * Time.deltaTime * buildingRotateDegree);
            //Offset.GetChild(2).transform.Rotate(Vector3.up * Time.deltaTime * buildingRotateDegree);
        }
    }

    void BuildingMoveAndCheck()
    {
        //1. check
        if ((transform.position - buildingEndPos.position).magnitude < 1.1f) //¿Ö 1.1f?
        {
            buildingMoveSpeed = -1.0f;
        }
        if((transform.position - buildingStartPos.position).magnitude < 1.1f)
        {
            buildingMoveSpeed = 1.0f;
        }
        //2.move
        transform.position += transform.forward * Time.deltaTime * buildingMoveSpeed;
    }
}
