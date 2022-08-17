using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;

    [SerializeField]private Transform player;

    Vector3 cameraOffset = new Vector3(0, 5, -7);

    public float moveSmoothSpeed = 10.0f;
    public float turnSmoothSpeed = 5.0f;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        target = player;
        transform.position = target.position + cameraOffset;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.IsCamStateFollow())
        {
            Move();
            //Rotate();
            transform.LookAt(target);
        }
        else if (GameManager.instance.IsCamStateFocus())
        {
            Move();
            transform.LookAt(target);
        }
        SetOrthoPerspec();
    }
    public void SetCameraSetting(Transform _target,float moveSpeed, Vector3 offset)
    {
        target = _target;
        moveSmoothSpeed = moveSpeed;
        cameraOffset = offset;
    }

    void Move()
    {
        Vector3 dPos = target.position;
        dPos += target.forward * cameraOffset.z;
        dPos += target.up * cameraOffset.y;
        transform.position = Vector3.Lerp(transform.position, dPos, moveSmoothSpeed * Time.deltaTime);
    }
    void Rotate()
    {
        float currYAngle = Mathf.LerpAngle(transform.eulerAngles.y, target.eulerAngles.y,
            turnSmoothSpeed * Time.deltaTime);
        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

    }

    void SetOrthoPerspec()
    {
        if (Camera.main.orthographic)
        {
            Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << LayerMask.NameToLayer("OutSide"));
            cameraOffset = new Vector3(0, 5, -7);
        }
        else
        {
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("OutSide");

            if (GameManager.instance.IsStoryStateHui())
            {
                cameraOffset = new Vector3(0, 3, -6);
            }
            else if (GameManager.instance.IsStoryStateEnjoy())
            {
                cameraOffset = new Vector3(0, 6, -6);
            }
        }
    }
}
