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
    //private FollowCamera followCam;
    //private RotateCamera rotateCam;
    private CameraMovement cameraMovement;

    private GameObject Zero;

    public GameObject[] NPCs;
    public ControlLand[] ControlLands;
    public Transform EnzoPos;
    public GameObject BurgerBarrier;
    // 희
    bool findHui = false;
    bool spawnHuiMemory = false;
    bool findHuiMemory = false;
    bool getHuiMemory = false;
    bool getBackCatColor = false;
    bool nameCatName = false;
    bool findFlower = false;
    bool getBackFlowerColor = false;

    // 엔조
    bool catchMonster = false;
    bool spawnEnzoMemory = false;
    bool getEnzoMemory = false;
    bool findFamilyCar = false;
    bool eatBurger = false;


    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
        //followCam = FindObjectOfType<FollowCamera>();
        //rotateCam = FindObjectOfType<RotateCamera>();
        cameraMovement = FindObjectOfType<CameraMovement>();

        Zero = FindObjectOfType<PlayerInput>().gameObject;
    }

    void Update()
    {
        if (GameManager.instance.IsStoryStateHui())
        {
            if (!findHui)
            {
                DiscoverHui();
            }

            if (!spawnHuiMemory)
            {
                if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 0)
                {
                    SpawnHuiEmotion();
                }
            }
            if (spawnHuiMemory && !findHuiMemory)
            {
                DiscoverHuiEmotion();
            }

            getHuiMemory = GameManager.instance.belongEmotions[0];
            if (getHuiMemory && GameManager.instance.spawnMemories[0].activeSelf)
            {
                dialogueManager.zeroTalk = true;
                dialogueManager.Action(Zero);
                GameManager.instance.spawnMemories[0].SetActive(false);
                UIManager.instance.OnOffHuiNote(true);
            }

            if (!getBackCatColor)
            {
                if (questManager.questId == 20 && questManager.questAcitonIndex ==3  && dialogueManager.talkIndex == 3)
                {
                    getBackCatColor = true;
                    focusCat();
                    GameManager.instance.SetGameStateToStory();
                }
            }
            if (getBackCatColor && !nameCatName)
            {
                if (questManager.questId == 20 && questManager.questAcitonIndex == 3 && dialogueManager.talkIndex == 4)
                {
                    GameManager.instance.SetGameStateToStory();
                    SetCatName();
                }
                nameCatName = UIManager.instance.BNameCatName();
            }
            if (findFlower)
            {
                print("벽 걷기 활성화 & 희 : 너가 원하는 곳은 어디든 갈 수 있어. ");
                if (questManager.questId == 30 && questManager.questAcitonIndex == 0)
                {
                    focusFlower();
                }
            }

            if (questManager.questId == 40)
            {
                EndHuiStory();
            }
        }
        else if (GameManager.instance.IsStoryStateEnjoy())
        {

            getEnzoMemory = GameManager.instance.belongEmotions[1];
            if (!getEnzoMemory)
            {
                SpawnAFollowEnzoMemory();
            }

            if (getEnzoMemory && GameManager.instance.spawnMemories[1].activeSelf)
            {
                print("햄버거집 오픈 ");
                BurgerBarrier.SetActive(false);
                GameManager.instance.spawnMemories[1].SetActive(false);
                UIManager.instance.OnOffEnjoyNote(true);//enjoy로 바꿔야함

                print("수정필요한 부분 (quest)");
                questManager.questId = 70;
            }


        }

    }

    //====================================
    void DiscoverHui()
    {
        Transform hui = GameObject.Find("NPC_Hui").transform;
        float distance = Vector3.Distance(Zero.transform.position, hui.position);
        if (distance <= 7.0f && !GameManager.instance.IsGameStateDialogue())
        {
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);

            GameManager.instance.SetCamStateToFocus();
            GameManager.instance.SetGameStateToStory();
            focusHui(hui);
        }
    }
    void focusHui(Transform hui)
    {
        findHui = true;

        cameraMovement.SetCameraSetting(hui, 0.1f, transform.forward * 4);

        StartCoroutine(EndfocusingHui());
    }

    IEnumerator EndfocusingHui()
    {
        yield return new WaitForSeconds(3.0f);
        //followCam.ResetCameraSetting();
        GameManager.instance.SetGameStateToPlay();
        GameManager.instance.SetCamStateToFollow();
        cameraMovement.SetCameraSetting(playerState.transform, 3f, new Vector3(0, 5, -7));
    }

    //====================================

    void SpawnHuiEmotion()
    {
        if (GameManager.instance.spawnMemories[0].activeSelf && 
            GameManager.instance.belongEmotions[0]) return;
        GameManager.instance.spawnMemories[0].SetActive(true);
        spawnHuiMemory = true;

    }
    void DiscoverHuiEmotion()
    {
        float distance = Vector3.Distance(Zero.transform.position, GameManager.instance.spawnMemories[0].transform.position);
        if (distance <= 1.5f && !GameManager.instance.IsGameStateDialogue())
        {
            GameManager.instance.SetGameStateToDialogue();
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
            findHuiMemory = true;
        }
    }

    //====================================
    void focusCat()
    {
        Transform cat = GameObject.Find("NPC_Cat").transform;
        
        if(cameraMovement.target != cat)
        {
            cameraMovement.SetCameraSetting(cat, 0.5f, cat.right * -7 + cat.up * 3);
        }

        StartCoroutine(EndfocusingCat());
    }

    IEnumerator EndfocusingCat()
    {
        yield return new WaitForSeconds(3.0f);

        GameManager.instance.SetGameStateToDialogue();
        GameManager.instance.SetCamStateToFollow();
        cameraMovement.SetCameraSetting(playerState.transform, 3f, new Vector3(0, 5, -7));
    }




    //float catVal = 0.0f;
    //void focusCat()
    //{
    //    //FFE500
    //    GameManager.instance.SetGameStateToStory();
    //    Transform cat = GameObject.Find("NPC_Cat").transform;

    //    if (catVal <= 0.0f)
    //    {
    //        Camera.main.transform.position = cat.position + new Vector3(2, 2, 2);
    //        Camera.main.transform.LookAt(cat);
    //    }
    //    catVal += Time.deltaTime * 1.5f;
    //    cat.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetFloat("_blend", catVal);

    //    //followCam.SetTargets(cat, cat);
    //    //followCam.moveSmoothSpeed = 0.3f;

    //    //followCam.SetOffset(new Vector3(2, 3, -6));

    //    StartCoroutine(focusingCat(cat));
    //}

    //IEnumerator focusingCat(Transform cat)
    //{
    //    yield return new WaitForSeconds(3.5f);

    //    if (!getBackCatColor)
    //    {
    //        print("back");
    //        //followCam.ResetCameraSetting();
    //        getBackCatColor = true;
    //        dialogueManager.Action(cat.gameObject);
    //    }

    //}

    //====================================
    void SetCatName()
    {
        UIManager.instance.OnOffCatNameWindow(true);
    }

    //====================================
    void focusFlower()
    {
    }

    IEnumerator EndFocusingFlower()
    {
        yield return new WaitForSeconds(2.5f);
    }

    void EndHuiStory()
    {
        //GameObject.Find("NPC_Hui").SetActive(false);
        //GameObject.Find("NPC_Cat").SetActive(false);

        NPCs[0].SetActive(false);
        NPCs[1].SetActive(false);
        for(int i = 0; i<ControlLands.Length; i++)
        {
            ControlLands[i].start = true;
        }
        //GameManager.instance.SetStoryStateToEnjoy();
        print("끝");
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    void SpawnAFollowEnzoMemory()
    {
        GameManager.instance.spawnMemories[1].SetActive(true);
        GameManager.instance.spawnMemories[1].transform.position = EnzoPos.position;
    }

}
