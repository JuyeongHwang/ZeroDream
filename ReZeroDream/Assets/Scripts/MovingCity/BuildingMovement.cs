using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    public bool isRotate = false;
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
            transform.Rotate(Vector3.up * Time.deltaTime * buildingRotateDegree);
            transform.GetChild(0).transform.Rotate(Vector3.up * Time.deltaTime * buildingRotateDegree);
            transform.GetChild(2).transform.Rotate(Vector3.up * Time.deltaTime * buildingRotateDegree);
        }
    }

    void BuildingMoveAndCheck()
    {
        //1. check
        if ((transform.position - buildingEndPos.position).magnitude < 1.1f) //¿Ö 1.1f?
        {
            //StartCoroutine(ReSpawnDelay());
            //isActive = false;
            //transform.gameObject.SetActive(false);

            transform.position = buildingStartPos.position;
        }

        //2.move
        transform.position += transform.forward * Time.deltaTime * buildingMoveSpeed;
    }
}
