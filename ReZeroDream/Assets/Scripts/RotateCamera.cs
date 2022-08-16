using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public GameObject target;
    
    private float xRotateMove, yRotateMove;

    public float rotateSpeed = 1.0f;
    private FollowCamera fc;

    private void Start()
    {
        Debug.Log("수정 필요");
        fc = GetComponent<FollowCamera>();
        fc.enabled = true;
    }
    private void Update()
    {
        //Debug.Log("수정");
        if (GameManager.instance.IsGameStateSetting()) return;
        if (GameManager.instance.IsGameStateDialogue()) return;
        if (GameManager.instance.IsGameStateStory()) return;
        //if(!GameManager.instance.IsGameStatePlay())
        if (Input.GetMouseButton(0))
        {
            fc.enabled = false;
            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            Vector3 stagePosition = target.transform.position;

            transform.RotateAround(stagePosition, Vector3.right, -yRotateMove);
            transform.RotateAround(stagePosition, Vector3.up, xRotateMove);

            transform.LookAt(stagePosition);
        }
        else
        {
            fc.enabled = true;
        }
        //else
        //{
        //    fc.enabled = true;

        //    if (Input.GetKey(KeyCode.T))
        //    {
        //        rotation = true;
        //    }
        //    else
        //    {
        //        rotation = false;
        //    }
        //    rotate();

        //}

    }


    bool rotation = true;
    public void rotate(bool _rot)
    {
        rotation = _rot;
        target = fc.target.gameObject;

        fc.enabled = false;
        xRotateMove += Time.deltaTime * 0.05f;
        Vector3 stagePosition = target.transform.position;
        transform.RotateAround(stagePosition, Vector3.up, xRotateMove);
        transform.LookAt(stagePosition);


    }
    IEnumerator Recovery()
    {
        yield return new WaitForSeconds(3.0f);
        fc.enabled = true;
    }

}
