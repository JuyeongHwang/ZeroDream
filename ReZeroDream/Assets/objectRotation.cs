using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectRotation : MonoBehaviour
{
    [Range(0, 5)]
    public float speed = 2.0f;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles -= new Vector3(0, speed, 0) * Time.deltaTime;
    }
}
