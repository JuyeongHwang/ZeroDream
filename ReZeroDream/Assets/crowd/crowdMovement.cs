using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdMovement : MonoBehaviour
{
    public Transform avoidTarget;
    public float speed = 4.0f;

    public List<GameObject> curCrowds;


    void Update()
    {
        if (!GameManager.instance.IsStoryStateWant()) return;
        //자신의 target 근처로 향하고, player는 피함.
        for (int i = 0; i < curCrowds.Count; i++)
        {

            Vector3 diffPlayer = avoidTarget.position - curCrowds[i].transform.position;
            float distancePlayer = diffPlayer.magnitude;
            Vector3 orientPlayer = diffPlayer / distancePlayer;

            if (distancePlayer < 4.0f)
            {
                Vector3 newPos = Vector3.Lerp(curCrowds[i].transform.position, curCrowds[i].transform.position - orientPlayer * 1.5f, speed * Time.deltaTime);
                curCrowds[i].transform.GetComponent<Rigidbody>().MovePosition(newPos);

                curCrowds[i].transform.GetComponent<Animator>().SetFloat("Blend", -1.0f, 0.0f, Time.deltaTime);
                curCrowds[i].transform.LookAt(avoidTarget);
            }
            else if(distancePlayer >= 4.0f && distancePlayer < 7.0f)
            {
                curCrowds[i].GetComponent<Animator>().SetFloat("Blend", 0.0f, 0.1f, Time.deltaTime);
                curCrowds[i].transform.LookAt(avoidTarget);
            }
            else if (distancePlayer >= 7.0f && distancePlayer < 15.0f)
            {
                //if (curCrowds[i].GetComponent<crowdCollision>().enterCollision) continue;
                Vector3 newPos = Vector3.Lerp(curCrowds[i].transform.position, curCrowds[i].transform.position + orientPlayer * 2.0f, speed * Time.deltaTime);
                curCrowds[i].transform.GetComponent<Rigidbody>().MovePosition(newPos);

                curCrowds[i].transform.GetComponent<Animator>().SetFloat("Blend", 2.0f, 0.1f, Time.deltaTime);
                curCrowds[i].transform.LookAt(avoidTarget);

            }
            else
            {
                curCrowds[i].GetComponent<Animator>().SetFloat("Blend", 0.0f, 0.1f, Time.deltaTime);
                curCrowds[i].transform.LookAt(avoidTarget);
            }
            
        }

    }


    //void Move(float distance, Vector3 orient, Transform crowd)
    //{

    //    if (distance > 12.0f && distance < 35.0f)
    //    {
    //        Vector3 newPos = Vector3.Lerp(crowd.position, crowd.position + orient * 2.0f, speed * Time.deltaTime);
    //        crowd.GetComponent<Rigidbody>().MovePosition(newPos);

    //        crowd.GetComponent<Animator>().SetFloat("Blend", 2.0f, 0.1f, Time.deltaTime);
    //        crowd.LookAt(target);

    //    }
    //    else if (distance < 5.0f)
    //    {
    //        Vector3 newPos = Vector3.Lerp(crowd.position, crowd.position - orient * 1.5f, speed * Time.deltaTime);
    //        crowd.GetComponent<Rigidbody>().MovePosition(newPos);

    //        crowd.GetComponent<Animator>().SetFloat("Blend", -1.0f, 0.0f, Time.deltaTime);
    //        crowd.LookAt(avoidTarget);

    //    }
    //    else
    //    {
    //        crowd.GetComponent<Animator>().SetFloat("Blend", 0.0f, 0.1f, Time.deltaTime);
    //        crowd.LookAt(target);
    //    }
    //}
}
