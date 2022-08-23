using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{

    private bool isDown = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.name == "NPC_Monster")
        {
            
            if(!isDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -74.876f, transform.position.z), Time.deltaTime * 5);
                isDown = true;
                //audioSource.Play();
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -74.53f, transform.position.z), Time.deltaTime *3);
                isDown = false;

            }
        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "NPC_Monster")
        {

            if (!isDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -74.876f, transform.position.z), Time.deltaTime * 5);
                isDown = true;
                //audioSource.Play();
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -74.53f, transform.position.z), Time.deltaTime * 3);
                isDown = false;

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isDown)
        {
            isDown = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -74.53f, transform.position.z), Time.deltaTime * 3);

        }

    }
}
