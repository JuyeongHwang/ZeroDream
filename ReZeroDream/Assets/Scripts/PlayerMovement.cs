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

    public DetailCollider footCollider;
    public AudioSource jumpAudio;
    public AudioSource playerAudio;
    public AudioClip jumpEndSound;
    public AudioClip jumpStartSound;
    public AudioClip concreteStep;
    public AudioClip grassStep;
    public AudioClip waterStep;

    private float animSpeed = 0.0f;

    enum MoveState { IDLE, WALK, RUN, JUMPSTART, JUMPEND };
    [SerializeField] MoveState moveState = MoveState.IDLE;

    private void Start()
    {
        Debug.Log("수정 필요");
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsGameStatePlay() || GameManager.instance.IsUserStateInteraction())
        {
            playerAnimator.SetFloat("Move", 0f);
            return;
        }

        Move();
        Rotate();
        SoundEffect();
        playerAnimator.SetFloat("Move", animSpeed);


    }

    private void Update()
    {
        if (!GameManager.instance.IsGameStatePlay() || GameManager.instance.IsUserStateInteraction())
        {
            playerAnimator.SetFloat("Move", 0f);
            return;
        }

        Jump();
        playerAnimator.SetBool("isJump", moveState == MoveState.JUMPSTART);
    }



    private void Move()
    {
        Vector3 moveDistance = Vector3.zero;
        if (playerInput.run && playerInput.move == 1.0f)
        {
            if (moveState != MoveState.JUMPSTART) moveState = MoveState.RUN;

            moveDistance = playerInput.move * transform.forward * runSpeed * Time.deltaTime;
            animSpeed = 2.0f;
        }
        else
        {

            moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
            animSpeed = playerInput.move;
            if (moveDistance == Vector3.zero)
            {
                if (moveState != MoveState.JUMPSTART) moveState = MoveState.IDLE;
            }
            else
            {
                if (moveState != MoveState.JUMPSTART) moveState = MoveState.WALK;
            }
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
        if (playerInput.jump && moveState != MoveState.JUMPSTART)
        {
            moveState = MoveState.JUMPSTART;
            jumpAudio.clip = jumpStartSound;
            jumpAudio.Play();
        }

        if (moveState == MoveState.JUMPSTART)
        {
            Vector3 moveDistance = transform.up * jumpSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + moveDistance / 2.0f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (moveState == MoveState.JUMPSTART)
        {
            if (footCollider.detailCollision)
            {
                moveState = MoveState.JUMPEND;
                jumpCount = 0;
                jumpAudio.clip = jumpEndSound;
                jumpAudio.Play();
                moveState = MoveState.IDLE;
            }



        }

    }

    //1. Move 함수 안에 SE 넣기
    //2. Move 함수 밖에 SE 제어하는 함수 따로 만들기
    public void SoundEffect()
    {
        //1.4, 2.5

        if (moveState == MoveState.RUN)
        {
            if (!playerAudio.isPlaying)
            {
                playerAudio.clip = concreteStep;
                playerAudio.loop = true;
                playerAudio.Play();
            }
            if (playerAudio.pitch != 1.4f)
            {
                playerAudio.pitch = 1.4f;
            }
        }
        if (moveState == MoveState.WALK)
        {

            if (!playerAudio.isPlaying)
            {
                playerAudio.clip = concreteStep;
                playerAudio.loop = true;
                playerAudio.Play();
            }
            if (playerAudio.pitch != 0.8f)
            {
                playerAudio.pitch = 0.8f;
            }
        }
        if (moveState == MoveState.IDLE || moveState == MoveState.JUMPSTART)
        {
            playerAudio.Stop();
        }

    }

}
