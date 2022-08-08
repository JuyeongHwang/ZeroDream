using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform lookTarget;
    private Transform target;
    public Vector3 cameraOffset = new Vector3(0, 3, -7);


    public float moveSmoothSpeed = 0.25f;
    public float turnSmoothSpeed = 5.0f;

    
    void Start()
    {
        lookTarget = FindObjectOfType<PlayerMovement>().transform;
        target = FindObjectOfType<PlayerMovement>().transform;
        transform.position = target.position;
        //cameraOffset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        transform.LookAt(target);
        Move();
        Rotate();
    }

    private void LateUpdate()
    {
        transform.LookAt(lookTarget);
        //// CullingMask에 "Group" Layer를 추가합니다.
        //Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Group");
        //// CullingMask에 "Group" Layer를 제거합니다.
        //Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << LayerMask.NameToLayer("Group"));
        if (Camera.main.orthographic)
        {
            Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << LayerMask.NameToLayer("OutSide"));
            cameraOffset = new Vector3(0, 5, -7);
        }
        else
        {
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("OutSide");
            cameraOffset = new Vector3(0, 3, -6);
        }

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

    public void SetTargets(Transform _lookTarget, Transform _target)
    {
        lookTarget = _lookTarget;
        target = _target;
    }
}
