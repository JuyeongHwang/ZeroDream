using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    public float speed = 1.0f;
    float time;
    private void Update()
    {
        //print(Mathf.Cos(time));
        time += Time.deltaTime;
        transform.position += Vector3.up * Mathf.Cos(time) * speed;
    }
}
