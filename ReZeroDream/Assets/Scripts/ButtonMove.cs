using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{

    private bool isDown = false;
    public Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //if 안눌려 있으면 BtnDown
            //if 눌려 있으면 BtnUp
            if(!isDown)
            {
                animator.SetTrigger("Down");
                isDown = true;
                audioSource.Play();
                Debug.Log("Down");
            }

            else
            {
                animator.SetTrigger("Up");
                audioSource.Play();

                Debug.Log("Up");
            }
        }
        else
        {
            isDown = false;
        }
    }

}
