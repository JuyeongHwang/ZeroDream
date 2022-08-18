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
    public Transform Hui;
    public Transform Cat;

    public Material[] flowerMats;
    public Material[] HuiMats;
    public ControlLand[] ControlLands;
    public Transform EnzoPos;
    public GameObject BurgerBarrier;
    // 희
    bool findHui = false;
    bool spawnHuiMemory = false;
    bool findHuiMemory = false;
    bool getHuiMemory = false;
    bool getBackCatColor = false;
    bool bfocusCat = false;
    bool nameCatName = false;
    bool findFlower = false;
    bool bfocusFlower = false;
    bool getBackFlowerColor = false;
    bool getBackAllColor = false;
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
        for (int i = 0; i < HuiMats.Length; i++)
        {
            HuiMats[i].SetFloat("_blend", 0);
        }
        for (int i = 0; i < flowerMats.Length; i++)
        {
            flowerMats[i].SetFloat("_blend", 0);
        }
        Zero = FindObjectOfType<PlayerInput>().gameObject;
    }

    void Update()
    {
        if (GameManager.instance.IsStoryStateHui())
        {
            checkInCameraHui();

            if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 0)
            {
                SpawnHuiEmotion();
            }
            if(spawnHuiMemory && !findHuiMemory)
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
            if (questManager.questId == 20 && questManager.questAcitonIndex == 3)
            {
                if(dialogueManager.talkIndex == 3)
                {
                    GameManager.instance.SetGameStateToStory();
                    if (!getBackCatColor) CatColorChange();
                    if (!bfocusCat) FocusCat();
                }
                else if(dialogueManager.talkIndex == 4)
                {
                    if (!nameCatName) SetCatName();
                }
            }

            if (questManager.questId == 30 && questManager.questAcitonIndex == 0)
            {
                if (dialogueManager.isAction)
                {
                    findFlower = (Zero.GetComponent<PlayerInput>().scanObject.GetComponent<ObjData>().id == 11000);
                }
                if (findFlower)
                {
                    if (!getBackFlowerColor) FlowerColorChange();
                    //if (!bfocusFlower) FocusFlower();
                    print("focus flower");
                }
            }

            if (questManager.questId == 30 && questManager.questAcitonIndex ==2 && dialogueManager.talkIndex ==1)
            {
                print("change color");
                //if ((Zero.GetComponent<PlayerInput>().scanObject == Hui))
                //    if (!getBackAllColor) AllColorChange();
            }
            if (questManager.questId == 40)
            {
                EndHuiStory();
            }

        }
        else if (GameManager.instance.IsStoryStateEnjoy())
        {
            getEnzoMemory = GameManager.instance.belongEmotions[1];
            if (getEnzoMemory && GameManager.instance.spawnMemories[1].activeSelf)
            {
                //dialogueManager.zeroTalk = true;
                //dialogueManager.Action(Zero);
                GameManager.instance.spawnMemories[1].SetActive(false);
                UIManager.instance.OnOffEnzoNote(true);
            }
        }
    }



    #region HAPPY - HUI
    public void checkInCameraHui()
    {
        if (findHui) return;


        Vector3 viewPos = Camera.main.WorldToViewportPoint(Hui.position);
        float distance = (Zero.transform.position - Hui.transform.position).magnitude;
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            if (distance <= 7.0f)
            {
                findHui = true;

                dialogueManager.zeroTalk = true;
                dialogueManager.Action(Zero);


                cameraMovement.SetCameraSetting(Hui, 0.1f, new Vector3(10, 3, -10));

                GameManager.instance.SetCamStateToFocus();
                GameManager.instance.SetGameStateToStory();

                StartCoroutine(endFocusingHui());
            }
        }

    }

    IEnumerator endFocusingHui()
    {
        yield return new WaitForSeconds(3.0f);

        cameraMovement.SetCameraSetting(Zero.transform, 3.0f, new Vector3(0, 5, -7));
        GameManager.instance.SetCamStateToFollow();
        GameManager.instance.SetGameStateToPlay();
    }

    #endregion

    #region SpawnEmotions

    void SpawnHuiEmotion()
    {
        if (spawnHuiMemory) return;
        GameManager.instance.spawnMemories[0].SetActive(true);
        spawnHuiMemory = true;

    }
    void DiscoverHuiEmotion()
    {
        float distance = Vector3.Distance(Zero.transform.position, GameManager.instance.spawnMemories[0].transform.position);
        if (distance <= 3.0f && !GameManager.instance.IsGameStateDialogue())
        {
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
            findHuiMemory = true;
        }
    }


    #endregion

    #region CAT

    float catVal = 0.0f;
    
    void CatColorChange()
    {

        if(catVal > 1.0f)
        {
            getBackCatColor = true;
        }
        catVal += Time.deltaTime * 0.5f;
        Cat.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetFloat("_blend", catVal);
    }

    void FocusCat()
    {
        bfocusCat = true;

        cameraMovement.SetCameraSetting(Cat, 0.3f, new Vector3(5, 5, 0));
        GameManager.instance.SetCamStateToFocus();

        UIManager.instance.HideAllCanvas(1);

        StartCoroutine(endFocusingCat());
    }

    IEnumerator endFocusingCat()
    {
        yield return new WaitForSeconds(3.0f);

        UIManager.instance.ShowAllCanvas();
        cameraMovement.SetCameraSetting(Zero.transform, 3.0f, new Vector3(0, 5, -7));
        GameManager.instance.SetCamStateToFollow();
        GameManager.instance.SetGameStateToPlay();
        dialogueManager.Action(Cat.gameObject);
    }

    void SetCatName()
    {
        nameCatName = true;
        UIManager.instance.OnOffCatNameWindow(true);
        GameManager.instance.SetGameStateToStory();
    }


    #endregion

    #region FLOWER
    float flowerVal = 0.0f;

    void FlowerColorChange()
    {
        if (flowerVal >= 0.8f)
        {
            getBackFlowerColor = true;
        }
        flowerVal += Time.deltaTime * 0.5f;
        for (int i = 0; i < flowerMats.Length; i++)
        {
            flowerMats[i].SetFloat("_blend", flowerVal);
        }
    }

    void FocusFlower()
    {
        bfocusFlower = true;

        cameraMovement.SetCameraSetting(Zero.transform, 0.3f, new Vector3(13, 20, -13));
        GameManager.instance.SetCamStateToFocus();

        UIManager.instance.HideAllCanvas(0.1f);

        StartCoroutine(EndFocusingFlower());
    }

    IEnumerator EndFocusingFlower()
    {
        yield return new WaitForSeconds(2.5f);

        UIManager.instance.ShowAllCanvas();
        cameraMovement.SetCameraSetting(Zero.transform, 3.0f, new Vector3(0, 5, -7));
        GameManager.instance.SetCamStateToFollow();
        GameManager.instance.SetGameStateToPlay();
    }

    #endregion

    #region Hui End
    float val = 0.0f;

    void AllColorChange()
    {
        if (val > 1.0f)
        {
            getBackAllColor = true;
        }
        val += Time.deltaTime * 0.2f;
        for (int i = 0; i < HuiMats.Length; i++)
        {
            HuiMats[i].SetFloat("_blend", val);
        }
    }

    void EndHuiStory()
    {
        //GameObject.Find("NPC_Hui").SetActive(false);
        //GameObject.Find("NPC_Cat").SetActive(false);

        Hui.gameObject.SetActive(false);
        Cat.gameObject.SetActive(false);

        for(int i = 0; i<ControlLands.Length; i++)
        {
            ControlLands[i].start = true;
        }
        if (!FindObjectOfType<StoryManager>().EnzoLand.activeSelf)
        {
            FindObjectOfType<StoryManager>().EnzoLand.SetActive(true);
        }
        cameraMovement.cameraOffset = new Vector3(10, 10, 10);

        print("희 끝");

    }

    #endregion

    void SpawnEnzoEmotion()
    {
        if (spawnEnzoMemory) return;
        GameManager.instance.spawnMemories[1].SetActive(true);
        spawnHuiMemory = true;

    }
}




    /*
     
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
            if (questManager.questId == 30 && questManager.questAcitonIndex == 2)
            {
                ObjData objData = NPCs[0].GetComponent<ObjData>();
                objData._name = "희";
                UIManager.instance.UpdateHuiFlowerText();
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
     
     
     */