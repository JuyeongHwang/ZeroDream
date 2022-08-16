using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform lookTarget;
    public Transform target;

    public Transform player;
    public Vector3 cameraOffset = new Vector3(0, 3, -7);


    public float moveSmoothSpeed = 0.25f;
    public float turnSmoothSpeed = 5.0f;

    
    void Start()
    {
        player= FindObjectOfType<PlayerMovement>().transform;
        lookTarget = player;
        target = player;
        transform.position = target.position + cameraOffset;
        //cameraOffset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        transform.LookAt(target);
        Move();
        Rotate();
    }

    private void Update()
    {
        //if(target != player)
        //{
        //    if (GameManager.instance.IsGameStatePlay() || GameManager.instance.IsGameStateStory())
        //    {
        //        ResetCameraSetting();
        //    }
        //}

    }

    private void LateUpdate()
    {
        transform.LookAt(lookTarget);

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
    public void SetOffset(Vector3 offset)
    {
        cameraOffset = offset;
    }

    public void ResetCameraSetting()
    {
        SetTargets(player, player);
        SetOffset(new Vector3(0, 3, -6));
        moveSmoothSpeed = 0.8f;
    }

    IEnumerator Recovery()
    {
        yield return new WaitForSeconds(3.5f);
        ResetCameraSetting();

        if (GameManager.instance.IsGameStateStory())
        {
            UIManager.instance.ShowStoryMode();
            GameManager.instance.SetGameStateToDialogue();
        }
    }
}
