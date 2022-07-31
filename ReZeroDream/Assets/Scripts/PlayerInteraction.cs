using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private PlayerState playerState; //허락받는 부분
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private DialogueManager dialogueManager;

    enum LiftState { ReadyLift, StartLift, EndLift};
    LiftState liftState = LiftState.EndLift;
    public GameObject liftedItem { get; private set; }

    void Start()
    {
        Debug.Log("수정 필요");
        playerState = GetComponent<PlayerState>();
        playerInput = GetComponent<PlayerInput>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

        Click();
        Lift();
    }

    void Click()
    {
        clickUI();

        if (playerInput.Lclick)
        {
            if (!playerInput.scanObject) return;
            if (GameManager.instance.IsGameStateStory() || GameManager.instance.IsGameStateSetting()) return;
            if (playerInput.scanObject.name == "NPC_Cat")
            {
                if (GameManager.instance.spawnEmotions[0] && !GameManager.instance.belongEmotions[0]) return;
            }

            if (!GameManager.instance.IsUserStateHear())
            {
                if (playerInput.scanObject.GetComponent<ObjData>().isNpc)
                    playerInput.scanObject.transform.LookAt(transform);

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



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item" && liftState == LiftState.EndLift)
        {
            liftState = LiftState.ReadyLift;
            liftedItem = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        liftState = LiftState.EndLift;
        liftedItem = null;
    }

    void Lift()
    {
        liftUI();
        playerAnimator.SetBool("isLift", liftState == LiftState.StartLift ? true : false);

        if (liftState == LiftState.ReadyLift)
        {
            if (playerInput.lift)
            {
                GameManager.instance.SetUserStateToInteration();
                liftState = LiftState.StartLift;
                playerState.CheckLiftedItem(liftedItem);
                StartCoroutine(WaitLifting());
            }
        }
    }

    void liftUI()
    {
        UIManager.instance.OnOffPlayerLiftImage(liftState == LiftState.ReadyLift ? true : false);
    }


    IEnumerator WaitLifting()
    {
        yield return new WaitForSeconds(3.0f);
        GameManager.instance.SetUserStateToMove();
        liftState = LiftState.EndLift;
    }

    
}
