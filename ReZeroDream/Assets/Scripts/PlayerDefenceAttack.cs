using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenceAttack : MonoBehaviour
{
    public Transform attackItemPos;
    private PlayerInput playerInput;
    private PlayerState playerState;
    private Animator playerAnimator;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerState = GetComponent<PlayerState>();
        playerAnimator = GetComponent<Animator>();

    }


    public GameObject liftedItem { get; private set; }
    void Update()
    {


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "moveCube")
        {
            playerAnimator.SetBool("Pushing", true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "moveCube")
        {
            playerAnimator.SetBool("Pushing", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        playerAnimator.SetBool("Pushing", false);
    }

    bool canLift = false;
    private void OnTriggerEnter(Collider other)
    {


    }
    private void OnTriggerStay(Collider other)
    {

    }
    private void OnTriggerExit(Collider other)
    {

    }

}
