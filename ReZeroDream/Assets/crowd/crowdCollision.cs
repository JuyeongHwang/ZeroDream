using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdCollision : MonoBehaviour
{
    [HideInInspector] public bool enterCollision = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "crowd")
            enterCollision = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "crowd")
            enterCollision = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        enterCollision = false;
    }
}
