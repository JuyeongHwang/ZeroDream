using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public bool isActive = false;
    public float speed = 1.5f;
    public Transform StartPos;
    public Transform EndPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            CarMoveAndCheck();

            for(int i = 3; i<=6; i++)
            {
                transform.GetChild(i).transform.Rotate(Vector3.right, Space.Self);
            }
        }



    }

    void CarMoveAndCheck()
    {
        //1. check
        if ((transform.position - EndPos.position).magnitude < 1.1f)
        {
            //StartCoroutine(ReSpawnDelay());
            //isActive = false;
            //transform.gameObject.SetActive(false);

            transform.position = StartPos.position;
        }

        //2.move
        transform.position += transform.forward * Time.deltaTime * speed;

    }
    //빌딩에는 필요 없는 부분
    IEnumerator ReSpawnDelay()
    {
        yield return new WaitForSeconds(2.0f);
        transform.gameObject.SetActive(true);
        isActive = true;
        transform.position = StartPos.position;
    }
}
