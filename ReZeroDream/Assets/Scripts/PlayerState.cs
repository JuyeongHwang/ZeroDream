using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public string goal ="";

    private PlayerInteraction playerInteraction;
    private DialogueManager dialogue;
    private QuestManager questManager;

    public bool bInside = false;
    private void Start()
    {

        dialogue = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fall" )
        {
            if (GameManager.instance.IsStoryStateHui())
            {

                gameObject.transform.position = new Vector3(-2, 4, 2);
            }
            else if (GameManager.instance.IsStoryStateEnjoy())
            {
                gameObject.transform.position = new Vector3(-2, -74, 0.5f);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
            FindObjectOfType<PlayerMovement>().moveState = PlayerMovement.MoveState.IDLE;
            Physics2D.gravity = new Vector3(0, -9.8f, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Camera.main.transform.position = new Vector3(-2, 10, 2);

        }

        if (other.gameObject.tag == "Inside")
        {
            print("건물 진입");
            bInside = true;
            Camera.main.orthographic = true;
        }

        if (GameManager.instance)
        {
            if (other.gameObject.name == "EnjoyPlane" && !GameManager.instance.IsStoryStateEnjoy())
            {
                //FindObjectOfType<QuestManager>().questId = 50;
                GameManager.instance.SetGameStateToStory();
                GameManager.instance.SetStoryStateToEnjoy();

                GetComponent<PlayerMovement>().jumpSpeed = 8.0f;
                Physics.gravity = new Vector3(0, -9.8f, 0);
                //print("분위기가 달라졌어");
            }
            
            if (other.gameObject.name == "WantPlane" && !GameManager.instance.IsStoryStateWant())
            {
                //FindObjectOfType<QuestManager>().questId = 50;
                GameManager.instance.SetGameStateToStory();
                GameManager.instance.SetStoryStateToWant();

                GetComponent<PlayerMovement>().jumpSpeed = 0.0f;
                print("want 지역");
            }
        }



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Inside")
        {
            bInside = true;
            Camera.main.orthographic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bInside = false;
        Camera.main.orthographic = false;
    }
    public void CheckLiftedItem(GameObject g)
    {
        if(g == GameManager.instance.spawnMemories[0])
        {
            GameManager.instance.belongEmotions[0] = true;

            questManager.catQuestImg.SetActive(true);
        }

        if (g == GameManager.instance.spawnMemories[1])
        {
            GameManager.instance.belongEmotions[1] = true;
        }
        if (g == GameManager.instance.spawnMemories[2])
        {
            GameManager.instance.belongEmotions[2] = true;
        }
    }



}
