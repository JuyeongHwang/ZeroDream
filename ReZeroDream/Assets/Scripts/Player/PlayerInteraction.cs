using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private PlayerMovement playermovement;
    private PlayerState playerState; //허락받는 부분
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private DialogueManager dialogueManager;
    private QuestManager questManager;
    private CameraMovement camMove;
    //enum LiftState { ReadyLift, StartLift, EndLift};
    //LiftState liftState = LiftState.EndLift;
    public Transform throwItemPos;
    public GameObject liftedItem { get; private set; }
    public GameObject throwItem { get; private set; }
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        playerState = GetComponent<PlayerState>();
        playerInput = GetComponent<PlayerInput>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerAnimator = GetComponent<Animator>();
        camMove = GetComponent<CameraMovement>();
    }


    private void Update()
    {

        Click();
        Lift();

        if (GameManager.instance.IsUserStateThrowReady() && throwItem)
        {
            throwItem.transform.position = throwItemPos.position;
            //throwItem.transform.SetParent(throwItemPos, true);
        }
    }

    void Click()
    {
        clickUI();

        if (playerInput.Lclick)
        {
            if (GameManager.instance.IsGameStateStory()) return;

            if (GameManager.instance.IsUserStateThrowReady())
            {
                GameManager.instance.SetUserStateToThrow();
                throwItem.GetComponent<throwSleepBall>().Launch();
                GameManager.instance.SetUserStateToMove();
                return;
            }


            //강제로 킨 경우
            if (GameManager.instance.IsGameStatePlay() && dialogueManager.isAction)
            {
                UIManager.instance.OnOffDialogueWindow(false);

                GameManager.instance.SetGameStateToPlay();
                GameManager.instance.SetUserStateToMove();
            }
            //제로 혼잣말
            if (questManager.nowDialogueObject == 3000 && dialogueManager.zeroTalk)
            {
                if (!GameManager.instance.IsUserStateHear())
                {
                    dialogueManager.Action(gameObject); //이 script가 붙어있는 gameObject를 반환. 즉 gameobject == zero
                }
                return;
            }

            if (!playerInput.scanObject)
            {
                if (dialogueManager.isAction)
                {
                    //강종
                    dialogueManager.talkIndex = 0;
                    dialogueManager.isAction = false;
                    UIManager.instance.OnOffDialogueWindow(false);

                    GameManager.instance.SetGameStateToPlay();
                    GameManager.instance.SetUserStateToMove();
                }
                return;
            }

            if (playerInput.scanObject.name == "NPC_Cat")
            {
                if (GameManager.instance.spawnMemories[0] && !GameManager.instance.belongEmotions[0]) return;
            }

            if (!GameManager.instance.IsUserStateHear())
            {
                if (playerInput.scanObject.GetComponent<ObjData>().isNpc)
                {
                    if (!GameManager.instance.IsStoryStateWant())
                        playerInput.scanObject.transform.LookAt(transform);
                }

                dialogueManager.Action(playerInput.scanObject);
            }

        }
    }

    void clickUI()
    {
        
        UIManager.instance.OnOffPlayerClickImage(((playerInput.scanObject) ? true : false));
        if(!GameManager.instance.IsGameStatePlay() || !GameManager.instance.IsUserStateMove())
        {
            UIManager.instance.OnOffPlayerClickImage(false);
        }

    }


    bool canLift = false;
    bool canThrow = false;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Ground")
        {
            if(playermovement.groundState != PlayerMovement.GroundState.Ground)
            {
                playermovement.groundState = PlayerMovement.GroundState.Ground;
                Physics.gravity = new Vector3(0, -9.8f, 0);
                transform.Rotate(Vector3.right * 90, Space.Self);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }

        if (other.tag == "Item")
        {
            canLift = true;
            liftedItem = other.gameObject;
        }
        if(other.tag == "attackItem")
        {
            canThrow = true;
            throwItem = other.gameObject;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item")
        {
            canLift = true;
            liftedItem = other.gameObject;
        }
        if (other.tag == "attackItem")
        {
            canThrow = true;
            throwItem = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        canLift = false;
        liftedItem = null;
        if (!GameManager.instance.IsUserStateThrowReady())
        {

            canThrow = false;
            throwItem = null;

        }

    }

    bool firstThrow = false;
    void Lift()
    {
        liftUI();

        if (playerInput.lift)
        {
            if (canLift)
            {
                canLift = false;
                playerState.CheckLiftedItem(liftedItem);

            }
            if (throwItem)
            {
                if (!firstThrow)
                {
                    firstThrow = true;
                    Time.timeScale = 0f;
                    UIManager.instance.ShowAndHideCautionWindow(true);
                    UIManager.instance.UpdateCautionText("몬스터를 재울 수 있는 초록색 아이템과 깨울 수 있는 분홍색 아이템이 있습니다.\n아이템을 이용하여 몬스터를 원하는 트랩에 고정시켜보세요.");
                }
                GameManager.instance.SetUserStateToThrowReady();
            }
        }

        //playerAnimator.SetBool("isLift", liftState == LiftState.StartLift ? true : false);

        //if (liftState == LiftState.ReadyLift)
        //{
        //    if (playerInput.lift)
        //    {
        //        GameManager.instance.SetUserStateToInteration();
        //        liftState = LiftState.StartLift;
        //        playerState.CheckLiftedItem(liftedItem);
        //        StartCoroutine(WaitLifting());
        //    }
        //}
    }

    void liftUI()
    {
        UIManager.instance.OnOffPlayerLiftImage(canLift? true : false);
    }

    
}
