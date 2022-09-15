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

    private PlayerState playerState;
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    public DetailCollider footCollider;
    public AudioSource jumpAudio;
    public AudioSource playerAudio;
    public AudioClip jumpEndSound;
    public AudioClip jumpStartSound;
    private AudioClip StepSE;
    public AudioClip concreteStep;
    public AudioClip grassStep;
    public AudioClip waterStep;

    private float animSpeed = 0.0f;

    public enum MoveState { IDLE, WALK, RUN, JUMPSTART, JUMPEND };
    public MoveState moveState = MoveState.IDLE;
    enum GroundState { Ground, Wall};
    [SerializeField] GroundState groundState = GroundState.Ground;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();

        StepSE = concreteStep; //default
    }

    private void FixedUpdate()
    {
        if (GameManager.instance)
        {
            if (!GameManager.instance.IsGameStatePlay() || GameManager.instance.IsUserStateInteraction())
            {
                playerAudio.Stop();
                playerAnimator.SetFloat("Move", 0f);
                return;
            }
        }

        if (GameManager.instance.IsStoryStateHui())
        {
            moveSpeed = 2.5f;
            runSpeed = 5.0f;
            jumpSpeed = 20.0f;
        }
        if (GameManager.instance.IsStoryStateEnjoy())
        {
            moveSpeed = 2.5f;
            runSpeed = 5.0f;
            jumpSpeed =12.0f;
        }
        if (GameManager.instance.IsStoryStateWant())
        {
            print("속도 꼭 바꾸기");
            moveSpeed = 3.0f;
            runSpeed = 3.0f;
            jumpSpeed = 0.0f;
            animSpeed = 1.0f;

        }

        Move();
        Rotate();
        SoundEffect();

        if (GameManager.instance.IsStoryStateWant())
        {
            if (animSpeed > 1.0f) animSpeed = 1.0f;
            playerAnimator.SetFloat("Move", animSpeed);
            return;
        }
        playerAnimator.SetFloat("Move", animSpeed);


    }

    private void Update()
    {
        if (GameManager.instance)
        {
            if (!GameManager.instance.IsGameStatePlay() || GameManager.instance.IsUserStateInteraction())
            {
                playerAnimator.SetFloat("Move", 0f);
                return;
            }
        }

        if (GameManager.instance.IsStoryStateWant()) return;
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
            //moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
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
        if (playerState.bInside) return;
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
            moveState = MoveState.JUMPEND;
            jumpCount = 0;
            jumpAudio.clip = jumpEndSound;
            jumpAudio.Play();
            moveState = MoveState.IDLE;
            //if (footCollider.detailCollision)
            //{
            //}

        }

        if (collision.gameObject.tag == "school")
        {
            GameManager.instance.gameEnding = true;
        }


        if (collision.gameObject.tag == "barrier")
        {
            DialogueManager.instance.isAction = true;
            UIManager.instance.OnOffDialogueWindow(true);
            UIManager.instance.UpdateDialogeText("제로","더이상 갈 수 없나봐");
        }


        if (collision.gameObject.layer == 4)
        {
            StepSE = waterStep;
        }
        else
        {
            StepSE = concreteStep;
        }

        if(collision.gameObject.tag == "Wall")
        {
            if(groundState != GroundState.Wall)
            {
                groundState = GroundState.Wall;


                transform.Rotate(Vector3.right * -90, Space.Self);
                if (GameManager.instance.IsStoryStateEnjoy())
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, collision.transform.eulerAngles.y, collision.transform.eulerAngles.z);
                    Physics.gravity = -collision.gameObject.transform.up * 10;


                }
                else
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, -90);
                    Physics.gravity = new Vector3(-9.8f, 0, 0);
                }


            }
        }
        if(collision.gameObject.tag == "Ground")
        {
            if (groundState != GroundState.Ground)
            {
                groundState = GroundState.Ground;
                Physics.gravity = new Vector3(0, -9.8f, 0);
                if(collision.gameObject.name == "roof")
                {
                    transform.Rotate(Vector3.right * 90, Space.Self);
                }
                else
                {
                    transform.Rotate(Vector3.right * -90, Space.Self);
                }
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }

        }

    }

    public void SoundEffect()
    {
        if (moveState == MoveState.WALK)
        {

            if (!playerAudio.isPlaying)
            {
                playerAudio.clip = StepSE;
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
            playerAudio.loop = false;
            playerAudio.Stop();
        }
        if (GameManager.instance.IsStoryStateWant()) { return; }

        if (moveState == MoveState.RUN)
        {
            if (!playerAudio.isPlaying)
            {
                playerAudio.clip = StepSE;
                playerAudio.loop = true;
                playerAudio.Play();
            }
            if (playerAudio.pitch != 1.4f)
            {
                playerAudio.pitch = 1.4f;
            }
        }



    }

}
