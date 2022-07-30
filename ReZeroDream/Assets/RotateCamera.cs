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
        fc = GetComponent<FollowCamera>();
        fc.enabled = true;
    }
    private void Update()
    {
        
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

        if (Input.GetKey(KeyCode.T))
        {
            rotation = true;
        }
        else
        {
            rotation = false;
        }
        rotate();
    }

    bool rotation = true;
    void rotate()
    {
        if (rotation)
        {
            fc.enabled = false;
            xRotateMove += Time.deltaTime * 0.1f;
            Vector3 stagePosition = target.transform.position;
            transform.RotateAround(stagePosition, Vector3.up, xRotateMove);
            transform.LookAt(stagePosition);
        }
        else
        {
            fc.enabled = true;

        }
    }

}
