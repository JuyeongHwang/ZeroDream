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
