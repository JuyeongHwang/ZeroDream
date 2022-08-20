using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    public bool isActive = false;
    public float buildingRotateDegree = 20.0f;
    public float buildingMoveSpeed = 1.0f;

    public Transform buildingStartPos;
    public Transform buildingEndPos;


    void Update()
    {
        if (isActive)
        {
            BuildingMoveAndCheck();

            transform.Rotate(Vector3.up * Time.deltaTime * buildingRotateDegree);
        }
    }

    void BuildingMoveAndCheck()
    {
        //1. check
        if ((transform.position - buildingEndPos.position).magnitude < 1.1f) //왜 1.1f?
        {
            //StartCoroutine(ReSpawnDelay());
            //isActive = false;
            //transform.gameObject.SetActive(false);

            transform.position = buildingStartPos.position;
        }

        //2.move
        transform.position += transform.forward * Time.deltaTime * buildingMoveSpeed;

    }
    ////빌딩에는 필요 없는 부분
    //IEnumerator ReSpawnDelay()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    transform.gameObject.SetActive(true);
    //    isActive = true;
    //    transform.position = StartPos.position;
    //}
}
