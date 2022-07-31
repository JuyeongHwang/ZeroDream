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



    public bool conversation { get; private set; }
    public bool lifting { get; private set; }
    public bool cantClick { get; private set; }

    public string goal ="";
    public bool missionStart = false;
    public bool missionEnd = false;

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
        
        print("수정수정");

        cantClick = (UIManager.instance.catNameWindow.isActive() || lifting); // || UIManager.instance.textEffect.isAnim
    }

    public void SetLifting(bool lift)
    {
        lifting = lift;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fall")
        {
            print("Reset");

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Camera.main.transform.position = new Vector3(-2, 10, 2);
            gameObject.transform.position = new Vector3(-2, 10, 2);
            //GameManager.instance.ResetPlayerPosition();
        }
    }

    public void SetGoal(string _g)
    {
        goal = _g;
    }

    //public void SetMission(bool start,bool complete)
    //{
    //    missionStart = start;
    //    missionComplete = complete;
    //}

    public void CheckLiftedItem(GameObject g)
    {
        if(g.name == "HuiEmotion(Clone)")
        {
            print("Destroy");
            GameManager.instance.belongEmotions[(int)EMOTION.HUI] = true;
            //missionComplete = true;
            Destroy(g);
        }
    }



}
