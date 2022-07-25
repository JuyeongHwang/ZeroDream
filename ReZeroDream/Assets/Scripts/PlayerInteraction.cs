using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private Animator playerAnimator;
    private PlayerInput playerInput;
    private DialogueManager dialogueManager;


    [HideInInspector]public bool isLifting = false;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Click();
        Lift();



        playerAnimator.SetBool("isLift", isLifting);
    }

    void Click()
    {
        if (playerInput.Lclick && playerInput.scanObject)
        {
            //Debug.Log("Click : " + playerInput.scanObject.name);
            dialogueManager.Action(playerInput.scanObject);
        }
    }

    void Lift()
    {

        if (playerInput.lift && !isLifting)
        {
            isLifting = true;
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
