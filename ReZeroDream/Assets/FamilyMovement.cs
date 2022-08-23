using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 4.0f;


    public List<GameObject> family;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < family.Count; i++)
        {
            Vector3 diffPlayer = target.position - family[i].transform.position;
            float distancePlayer = diffPlayer.magnitude;
            Vector3 orientPlayer = diffPlayer / distancePlayer;

            if (distancePlayer >= 20.0f)
            {
                //if (curCrowds[i].GetComponent<crowdCollision>().enterCollision) continue;
                Vector3 newPos = Vector3.Lerp(family[i].transform.position, family[i].transform.position + orientPlayer * 2.0f, speed * Time.deltaTime);
                family[i].transform.GetComponent<Rigidbody>().MovePosition(newPos);

                family[i].transform.GetComponent<Animator>().SetFloat("Blend", 2.0f, 0.1f, Time.deltaTime);
                family[i].transform.LookAt(target);
            }
            else
            {
                family[i].GetComponent<Animator>().SetFloat("Blend", 0.0f, 0.1f, Time.deltaTime);
                family[i].transform.LookAt(target);
            }

        }
    }


}
