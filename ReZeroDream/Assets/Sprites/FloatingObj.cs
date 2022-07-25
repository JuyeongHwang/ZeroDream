using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    float time;
    private void Update()
    {
        time += Time.deltaTime;
        transform.position = new Vector3( transform.position.x,  Mathf.Cos(time),transform.position.z) ;
    }
}
