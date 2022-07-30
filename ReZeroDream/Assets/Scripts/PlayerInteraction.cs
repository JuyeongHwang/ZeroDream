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
                //고양이를 find해서 넘기는게 더 나을듯..
                UIManager.instance.UpdateCatName(playerInput.scanObject.GetComponent<ObjData>());
                UIManager.instance.SetActiveCatNameImage(false);

                dialogueManager.Action(playerInput.scanObject);
            }

        }
    }
    void Click()
    {
        clickUI();

        if (playerState.cantClick) return;

        if (playerInput.Lclick)
        {
            if (!playerInput.scanObject) return;

            if (playerInput.scanObject.name == "NPC_Cat")
            {
                if (GameManager.instance.spawnEmotions[0] && !GameManager.instance.belongEmotions[0])
                {
                    return;
                }
            }

            if(playerInput.scanObject.GetComponent<ObjData>().isNpc)
                playerInput.scanObject.transform.LookAt(transform);
    
            dialogueManager.Action(playerInput.scanObject);
        }
    }

    void clickUI()
    {
        UIManager.instance.SetPlayerClickImage(((playerInput.scanObject) ? true : false));
        if(playerState.cantClick || playerState.conversation)
        {
            UIManager.instance.SetPlayerClickImage(false);
        }
    }

    void Lift()
    {
        liftUI();
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

    void liftUI()
    {
        UIManager.instance.SetPlayerLiftImage(canLift && !isLifting);
    }


    IEnumerator WaitLifting()
    {
        yield return new WaitForSeconds(3.0f);
        isLifting = false;
        canLift = false;
    }

    
}
