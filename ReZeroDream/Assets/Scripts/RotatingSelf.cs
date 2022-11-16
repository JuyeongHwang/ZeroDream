using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSelf : MonoBehaviour
{
    private float speed = 10f;
    void Update()
    {
        Rotating();
    }
    void Rotating()
    {
        transform.Rotate(new Vector3(0,  0, speed * Time.deltaTime));
    }
}
