using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public string goal ="";

    private PlayerInteraction playerInteraction;
    private DialogueManager dialogue;
    private QuestManager questManager;

    private void Start()
    {

        dialogue = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fall" && GameManager.instance.IsStoryStateHui())
        {
            print("Reset");

            transform.eulerAngles = new Vector3(0, 0, 0);
            FindObjectOfType<PlayerMovement>().moveState = PlayerMovement.MoveState.IDLE;
            Physics2D.gravity = new Vector3(0, -9.8f, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Camera.main.transform.position = new Vector3(-2, 10, 2);
            gameObject.transform.position = new Vector3(-2, 4, 2);
            //GameManager.instance.ResetPlayerPosition();
        }

        if (other.gameObject.tag == "Inside")
        {
            print("건물 진입");

            Camera.main.orthographic = true;
        }

        if (GameManager.instance)
        {
            if (other.gameObject.name == "EnjoyPlane" && !GameManager.instance.IsStoryStateEnjoy())
            {
                FindObjectOfType<QuestManager>().questId = 50;
                GameManager.instance.SetGameStateToStory();
                GameManager.instance.SetStoryStateToEnjoy();

                Physics.gravity = new Vector3(0, -9.8f, 0);
                print("분위기가 달라졌어");
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Inside")
        {

            Camera.main.orthographic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
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
    }



}
