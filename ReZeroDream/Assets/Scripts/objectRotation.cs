using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectRotation : MonoBehaviour
{

    public bool ferris = false;
    public GameObject[] ferrisObjs;
    
    [Range(0, 5)]
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (ferris)
        {
            for(int i = 0; i<ferrisObjs.Length; i++)
            {
                ferrisObjs[i].transform.position = transform.GetChild(i).transform.position;
            }
            transform.eulerAngles -= new Vector3(0, 0, speed) * Time.deltaTime;
        }
        else
        {

            transform.eulerAngles -= new Vector3(0, speed, 0) * Time.deltaTime;
        }
    }
}
