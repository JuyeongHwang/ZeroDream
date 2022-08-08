using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestImplementation : MonoBehaviour
{

    public enum EMOTION
    {
        HUI = 0,
        ENJOY,
        WANT
    };

    public GameObject EmotionPrefabs;
    private DialogueManager dialogueManager;
    private QuestManager questManager;
    private PlayerState playerState;

    private GameObject Zero;
    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
        Zero = FindObjectOfType<PlayerInput>().gameObject;

        if (GameManager.instance.IsStoryStateHui()) 
        {

            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }


    void Update()
    {
        if (questManager.questId == 10 && questManager.questAcitonIndex == 1)
        {
            DiscoverHui();
        }
        if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 0)
        {
            SpawnHuiEmotion();
            DiscoverHuiEmotion();
        }
        if (questManager.questId == 20 && questManager.questAcitonIndex == 2 && dialogueManager.talkIndex == 4)
        {
            if (UIManager.instance.catNameWindow.isActive()) return;
            SetCatName();
            focusingCat();
            GameManager.instance.SetGameStateToStory();
        }
        if (questManager.questId == 40)
        {
            EndHuiStory();
        }
    }

    void DiscoverHui()
    {
        float distance = Vector3.Distance(Zero.transform.position, GameObject.Find("NPC_Hui").transform.position);
        if (distance <= 7.0f && !GameManager.instance.IsGameStateDialogue())
        {
            focusingHui();
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }
    void SpawnHuiEmotion()
    {
        if (GameManager.instance.spawnEmotions[0]) return;

        GameObject g = Instantiate(EmotionPrefabs, new Vector3(-10, 5, 3), Quaternion.identity);
        GameManager.instance.spawnEmotions[0] = g;

    }
    void DiscoverHuiEmotion()
    {
        float distance = Vector3.Distance(Zero.transform.position, GameManager.instance.spawnEmotions[0].transform.position);
        if (distance <= 1.5f && !GameManager.instance.IsGameStateDialogue())
        {
            focusingHui();
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }
    void SetCatName()
    {
        Debug.Log("고양이 이름 짓기 활성화");
        UIManager.instance.OnOffCatNameWindow(true);
    }

    void focusingHui()
    {
        //camera focusing
    }
    void focusingCat()
    {
        //change color
        //camera focusing & rotation
    }
    void focusingFlower()
    {
        //camera focusing & rotation
    }

    void EndHuiStory()
    {
        GameManager.instance.SetStoryStateToEnjoy();
        print("끝");
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
