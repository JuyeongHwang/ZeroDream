using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private PlayerState playerState; //허락받는 부분
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private DialogueManager dialogueManager;

    [HideInInspector]public bool isLifting = false;
    private bool canLift = false;
    public GameObject liftedItem { get; private set; }

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        playerInput = GetComponent<PlayerInput>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

        Click();
        Enter();

        Lift();
        playerAnimator.SetBool("isLift", isLifting);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            canLift = true;
            liftedItem = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        canLift = false;
        liftedItem = null;
    }

    void Enter()
    {
        if(playerInput.enter)
        {
            Debug.Log("Enter");
            if (UIManager.instance.catName != "")
            {
                Debug.Log(UIManager.instance.catName);
                UIManager.instance.UpdateCatName(playerInput.scanObject.GetComponent<ObjData>());
                UIManager.instance.SetActiveCatNameImage(false);
            }

        }
    }
    void Click()
    {
        if (playerState.cantClick) return;

        if (playerInput.Lclick)
        {

            //if (playerState.missionStart && !playerState.missionComplete)
            //{
            //    Debug.Log(playerState.goal);
            //    return;
            //}
            //else if (playerState.missionStart && playerState.missionComplete)
            //{
            //    playerState.SetMission(false, false);
            //}


            if (playerInput.scanObject)
            {
                //Debug.Log("Click : " + playerInput.scanObject.name);
                dialogueManager.Action(playerInput.scanObject);
            }
        }
    }


    void Lift()
    {
        if (!canLift) return;

        if (playerInput.lift && !isLifting)
        {
            isLifting = true;
            playerState.CheckLiftedItem(liftedItem);
        }

        if (isLifting)
        {
            StartCoroutine(WaitLifting());
        }

    }

    IEnumerator WaitLifting()
    {
        yield return new WaitForSeconds(3.0f);
        isLifting = false;
    }

    
}
