using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float runSpeed = 5f;
    public float rotateSpeed = 120f;
    public float jumpSpeed = 3f;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private PlayerState playerState;

    private float animSpeed = 0.0f;
    bool isJumping = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        //움직임 제어 체크
        playerState = GetComponent<PlayerState>(); 
    }

    private void FixedUpdate()
    {
        if (playerState.conversation || playerState.lifting)
        {
            playerAnimator.SetFloat("Move", 0f);
            return;
        }


        Move();
        Rotate();
        playerAnimator.SetFloat("Move", animSpeed);


    }

    private void Update()
    {
        if (playerState.conversation || playerState.lifting)
        {
            
            return;
        }

        Jump();
        playerAnimator.SetBool("isJump", isJumping);
    }



    private void Move()
    {
        Vector3 moveDistance = Vector3.zero;
        if (playerInput.run && playerInput.move == 1.0f)
        {
            moveDistance = playerInput.move * transform.forward * runSpeed * Time.deltaTime;
            animSpeed = 2.0f;
        }
        else 
        {
            moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
            animSpeed = playerInput.move;
        }

        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);

    }

    private void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);

    }


    private void Jump()
    {
        if (playerInput.jump && !isJumping)
        {
            isJumping = true;
        }

        if (isJumping)
        {
            Vector3 moveDistance = transform.up * jumpSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + moveDistance / 2.0f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }

}
