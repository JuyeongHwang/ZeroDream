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
        if (other.gameObject.tag == "Fall")
        {
            print("Reset");

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Camera.main.transform.position = new Vector3(-2, 10, 2);
            gameObject.transform.position = new Vector3(-2, 10, 2);
            //GameManager.instance.ResetPlayerPosition();
        }
    }


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
