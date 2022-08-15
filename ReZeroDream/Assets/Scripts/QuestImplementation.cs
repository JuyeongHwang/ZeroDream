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
    private FollowCamera followCam;
    private RotateCamera rotateCam;

    private GameObject Zero;
    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
        followCam = FindObjectOfType<FollowCamera>();
        rotateCam = FindObjectOfType<RotateCamera>();

        Zero = FindObjectOfType<PlayerInput>().gameObject;

        if (GameManager.instance.IsStoryStateHui()) 
        {

            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }


    bool catCutScene = false;
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
        if (questManager.questId == 20 && questManager.questAcitonIndex == 2 && dialogueManager.talkIndex == 3)
        {
            if (!catCutScene)
            {
                catCutScene = true;
                //focusingCat();
            }
        }
        if (questManager.questId == 20 && questManager.questAcitonIndex == 2 && dialogueManager.talkIndex == 4)
        {
            if (UIManager.instance.catNameWindow.isActive()) return;
            //focusing cat
            GameManager.instance.SetGameStateToStory();
            SetCatName();


        }
        if (questManager.questId == 40)
        {
            EndHuiStory();
        }
    }

    void DiscoverHui()
    {
        Transform hui = GameObject.Find("NPC_Hui").transform;
        float distance = Vector3.Distance(Zero.transform.position, hui.position);
        if (distance <= 7.0f && !GameManager.instance.IsGameStateDialogue())
        {
            focusingHui(hui);
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }
    void SpawnHuiEmotion()
    {
        if (GameManager.instance.spawnMemories[0].activeSelf && 
            GameManager.instance.belongEmotions[0]) return;
        GameManager.instance.spawnMemories[0].SetActive(true);

    }
    void DiscoverHuiEmotion()
    {
        float distance = Vector3.Distance(Zero.transform.position, GameManager.instance.spawnMemories[0].transform.position);
        if (distance <= 1.5f && !GameManager.instance.IsGameStateDialogue())
        {
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }
    void SetCatName()
    {
        Debug.Log("고양이 이름 짓기 활성화");
        UIManager.instance.OnOffCatNameWindow(true);
    }

    void focusingHui(Transform hui)
    {
        //camera focusing
        followCam.SetTargets(hui, hui);
        followCam.moveSmoothSpeed = 0.05f;
        followCam.SetOffset(new Vector3(0, 3, 6));
    }

    void focusingCat()
    {
        Debug.Log("focusing");
        GameManager.instance.SetGameStateToStory();
        Transform cat = GameObject.Find("NPC_Cat").transform;
        followCam.SetTargets(cat, cat);
        rotateCam.rotate(true);    
    }
    void focusingFlower()
    {
        //camera focusing & rotation
    }

    void EndHuiStory()
    {
        GameObject.Find("NPC_Hui").SetActive(false);
        GameObject.Find("NPC_Cat").SetActive(false);

        GameManager.instance.SetStoryStateToEnjoy();
        print("끝");
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
