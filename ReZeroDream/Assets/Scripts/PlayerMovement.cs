using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float runSpeed = 5f;
    public float rotateSpeed = 120f;
    public float jumpSpeed = 3f;
    public int jumpCount = 0;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private PlayerState playerState;

    private AudioSource playerAudio;
    public AudioClip jumpEndSound;
    public AudioClip jumpStartSound;

    private float animSpeed = 0.0f;
    bool isJumping = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();
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
            //jumpCount++;
            isJumping = true;
            playerAudio.clip = jumpStartSound;
            playerAudio.Play(); //점프
        }

        if (isJumping)
        {
            Vector3 moveDistance = transform.up * jumpSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + moveDistance / 2.0f);
            
            //왜 되는거지..?

        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isJumping)
        {
            isJumping = false;
            jumpCount = 0;

            playerAudio.clip = jumpEndSound;
            playerAudio.Play(); //착지
        }

    }

}
