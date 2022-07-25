using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public enum EMOTION
    {
        HUI = 0,
        ENJOY,
        WANT
    };


    public bool[] belongEmotions = new bool[3] { false,false,false};
    
    public bool conversation { get; private set; }
    public bool lifting { get; private set; }
    
    public bool cantClick { get; private set; }

    public string goal ="";

    //mission start상태인데 missioncomplete가 아니라면 click 무시

    public bool missionStart = false;
    public bool missionComplete = false;

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
        cantClick = (UIManager.instance.catNameImage.activeSelf || UIManager.instance.textEffect.isAnim);
    }


    public void SetGoal(string _g)
    {
        goal = _g;
    }
    public void SetMission(bool start,bool complete)
    {
        missionStart = start;
        missionComplete = complete;
    }

    public void CheckLiftedItem(GameObject g)
    {
        if(g.name == "HuiEmotion")
        {
            belongEmotions[(int)EMOTION.HUI] = true;
            missionComplete = true;
            Destroy(g);
        }
    }



}
