using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuiQuests : MonoBehaviour
{
    //??
    public ControlLand[] ControlLands;

    private DialogueManager dialogueManager;
    private QuestManager questManager;
    private PlayerState playerState;
    private CameraMovement cameraMovement;

    private GameObject Zero;
    public Transform Hui;
    public Transform Cat;
    public Material[] flowerMats;
    public Material[] HuiMats;
    // 희
    bool findHui = false;
    bool spawnHuiMemory = false;
    bool findHuiMemory = false;
    bool getHuiMemory = false;
    bool zeroHuiTalk = false;
    bool getBackCatColor = false;
    bool bfocusCat = false;
    bool nameCatName = false;
    bool findFlower = false;
    bool bfocusFlower = false;
    bool getBackFlowerColor = false;
    bool getBackAllColor = false;

    // Start is called before the first frame update
    void Start()
    {

        Zero = FindObjectOfType<PlayerInput>().gameObject;

        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
        cameraMovement = FindObjectOfType<CameraMovement>();

        for (int i = 0; i < HuiMats.Length; i++)
        {
            HuiMats[i].SetFloat("_blend", 0);
        }
        for (int i = 0; i < flowerMats.Length; i++)
        {
            flowerMats[i].SetFloat("_blend", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.IsStoryStateHui())
        {
            checkInCameraHui();

            if (questManager.questId == 10 && questManager.questAcitonIndex == 2 && dialogueManager.talkIndex == 2)
            {
                UIManager.instance.ShowQuestWindow();
            }
            if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 0)
            {
                SpawnHuiEmotion();
            }
            if (spawnHuiMemory && !findHuiMemory)
            {
                DiscoverHuiEmotion();
            }

            getHuiMemory = GameManager.instance.belongEmotions[0];
            if (getHuiMemory && GameManager.instance.spawnMemories[0].activeSelf)
            {
                UIManager.instance.ShowAndHideCautionWindow(true);
                UIManager.instance.UpdateCautionText("도화지를 획득하였습니다.\n도화지의 그림을 통해 제로가 잃어버린 기억을 되찾아주세요.");

                GameManager.instance.spawnMemories[0].SetActive(false);
                UIManager.instance.OnOffHuiNote(true);
            }
            if (getHuiMemory && !UIManager.instance.activateCaution)
            {
                if (!zeroHuiTalk)
                {
                    zeroHuiTalk = true;
                    dialogueManager.zeroTalk = true;
                    dialogueManager.Action(Zero);
                }
            }
            if (questManager.questId == 20 && questManager.questAcitonIndex == 3)
            {
                if (dialogueManager.talkIndex == 3)
                {
                    GameManager.instance.SetGameStateToStory();
                    if (!getBackCatColor) CatColorChange();
                    if (!bfocusCat) FocusCat();
                }
                else if (dialogueManager.talkIndex == 4)
                {
                    if (!nameCatName) SetCatName();
                    print("벽 걷기 활성화 & 설명");
                }
            }
            if (questManager.questId == 30 && questManager.questAcitonIndex == 2)
            {
                UIManager.instance.UpdateHuiFlowerText();

                if (dialogueManager.talkIndex == 0)
                {
                    //print("0");
                    ObjData objData = Hui.GetComponent<ObjData>();
                    if (objData._name != "제로")
                    {

                        objData._name = "제로";
                    }
                }
                else if (dialogueManager.talkIndex == 2)
                {
                    ObjData objData = Hui.GetComponent<ObjData>();
                    if (objData._name != "희")
                    {

                        objData._name = "희";
                    }
                }

                if (!getBackFlowerColor) FlowerColorChange();

            }
            if (questManager.questId == 30 && questManager.questAcitonIndex == 3)
            {
                if (!getBackAllColor) AllColorChange();

            }

            if (questManager.questId == 40)
            {
                EndHuiStory();
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

        if (catVal > 1.0f)
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
            UIManager.instance.ShowAllCanvas();
            cameraMovement.SetCameraSetting(Zero.transform, 3.0f, new Vector3(0, 5, -7));
            GameManager.instance.SetCamStateToFollow();
            GameManager.instance.SetGameStateToPlay();
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
            getBackAllColor = true;
        }
        UIManager.instance.HideAllCanvas(2.0f);
        val += Time.deltaTime * 0.2f;
        GameManager.instance.SetCamStateToFocus();
        cameraMovement.cameraOffset = new Vector3(0, 20, -14);
        cameraMovement.moveSmoothSpeed = 1.0f;
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

        for (int i = 0; i < ControlLands.Length; i++)
        {
            ControlLands[i].start = true;
        }
        if (!FindObjectOfType<StoryManager>().EnzoLand.activeSelf)
        {
            FindObjectOfType<StoryManager>().EnzoLand.SetActive(true);
        }
        cameraMovement.cameraOffset = new Vector3(-20, 10, 10);

        print("희 끝");
        

    }

    #endregion

}
