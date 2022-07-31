using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakMeshTime : MonoBehaviour
{
    public float time = 0.0f;

    bool complete = false;
    void Update()
    {
        time += Time.deltaTime;

        if (time > 3.0f && !complete)
        {
            breakChildren();
            complete = true;
        }
        
    }

    void breakChildren()
    {
        for(int i = 0; i<transform.childCount; i++)
        {

            transform.GetChild(i).gameObject.AddComponent<breakMesh>();
            transform.GetChild(i).gameObject.GetComponent<breakMesh>().reDestroy = true;
            transform.GetChild(i).gameObject.GetComponent<breakMesh>().ExplodeForce = 0.0f;
            transform.GetChild(i).gameObject.GetComponent<breakMesh>().CutCascades = 3;
            transform.GetChild(i).gameObject.GetComponent<breakMesh>().paraent = this.transform;
            

        }

    }

    
}
