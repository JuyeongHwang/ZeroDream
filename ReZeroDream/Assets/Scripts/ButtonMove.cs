using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{

    private bool isDown = false;
    public Animator animator;
    private AudioSource audioSource;

    private void Update()
    {
        Debug.Log(isDown);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    //문제) 두번째 부딪혔을 때 다시 올라갔다 내려감.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if 안눌려 있으면 BtnDown
            //if 눌려 있으면 BtnUp
            if (!isDown)
            {
                animator.SetTrigger("Down");
                isDown = true;
                //audioSource.Play();
                Debug.Log("Down");
            }

            else
            {
                animator.SetTrigger("Up");
                //audioSource.Play();
                isDown = false;

                Debug.Log("Up");
            }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //isDown = !isDown;
            Debug.Log("Exit");
        }

    }

}
