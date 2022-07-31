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

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
    }


    void Update()
    {
        if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 0)
        {
            SpawnHuiEmotion();
        }
        if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 4)
        {
            if (UIManager.instance.catNameWindow.isActive()) return;
            SetCatName();
            GameManager.instance.SetGameStateToStory();
        }
        if(questManager.questId == 40)
        {
            EndHuiStory();
        }
    }


    void SpawnHuiEmotion()
    {
        if (GameManager.instance.spawnEmotions[0]) return;

        Debug.Log("spawn hui emotion");
        Instantiate(EmotionPrefabs, new Vector3(-10, 5, 3), Quaternion.identity);
        GameManager.instance.spawnEmotions[0] = true;

    }

    void SetCatName()
    {
        Debug.Log("고양이 이름 짓기 활성화");
        UIManager.instance.OnOffCatNameWindow(true);
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
        print("끝");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
