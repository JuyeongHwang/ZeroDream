using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool conversation { get; private set; }
    public bool lifting { get; private set; }
    
    [SerializeField]
    private string goal ="";

    private PlayerInteraction playerInteraction;
    private DialogueManager dialogue;

    private void Start()
    {
        dialogue = FindObjectOfType<DialogueManager>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
        conversation = dialogue.isAction;
        lifting = playerInteraction.isLifting;
    }


    public void SetGoal(string _g)
    {
        goal = _g;
    }




}
