using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailCollider : MonoBehaviour
{ 
    [HideInInspector] public bool detailCollision = false;
    private void OnTriggerEnter(Collider other)
    {
        detailCollision = true;
    }
    private void OnTriggerExit(Collider other)
    {
        detailCollision = false;

    }
}
