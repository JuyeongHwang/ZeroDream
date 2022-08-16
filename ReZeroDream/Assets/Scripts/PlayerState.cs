using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public string goal ="";

    private PlayerInteraction playerInteraction;
    private DialogueManager dialogue;



    private void Start()
    {

        dialogue = FindObjectOfType<DialogueManager>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fall" && GameManager.instance.IsStoryStateHui())
        {
            print("Reset");

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Camera.main.transform.position = new Vector3(-2, 10, 2);
            gameObject.transform.position = new Vector3(-2, 10, 2);
            //GameManager.instance.ResetPlayerPosition();
        }

        if (other.gameObject.tag == "Inside")
        {
            print("건물 진입");

            Camera.main.orthographic = true;
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
        }

        if (g == GameManager.instance.spawnMemories[1])
        {

        }
    }



}
