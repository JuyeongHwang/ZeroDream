using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class QuestImplementation : MonoBehaviour
{

    public Sprite enjoyFamilyImg;
    private DialogueManager dialogueManager;
    private QuestManager questManager;
    private PlayerState playerState;
    //private FollowCamera followCam;
    //private RotateCamera rotateCam;
    private CameraMovement cameraMovement;
    private crowdMovement crowdMovement;
    public FamilyMovement familyMovement;
    private GameObject Zero;



    public ControlLand[] ControlLands;

    //public GameObject[] MonsterTraps;
    //public Transform[] Monsters;
    //public GameObject[] Booms;


    public Transform Store;
    public Transform EnzoPos;
    public GameObject BurgerBarrier;

    public RawImage videoRaw;
    public VideoPlayer video;
    public VideoClip Burger;
    public VideoClip Happy;
    public VideoClip Normal;
    public VideoClip Bad;

    public Transform wantNpc;


    // 엔조
    bool findMonsters = false;
    bool zeroEnzoTalk = false;
    bool catchMonster = false;
    bool throwObj = false;
    bool pushTrap = false;
    bool spawnEnzoMemory = false;
    bool getEnzoMemory = false;
    bool findFamilyCar = false;
    bool eatBurger = false;
    bool findStore = false;
    bool enterStore = false;

    [SerializeField] float PlayTime = 0;
    [SerializeField] bool videoStart = false;
    [SerializeField] bool videoEnd = false;

    //원트
    bool zeroWantTalk = false;
    bool spawnWantMemory = false;
    bool getWantMemory = false;
    //bool firstMemory = false;
    //bool secondMem = false;
    //bool thirdMem = false;
    //bool forthMem = false;
    //bool fifthMem = false;
    //bool sixthMem = false;
    public Slider memrySlider;
    public List<bool> zeroMemories = new List<bool>();
    public List<bool> zeroFamily = new List<bool>();
    public Material[] wantMats;
    private void Start()
    {
        memrySlider.gameObject.SetActive(false);
        videoRaw.gameObject.SetActive(false);
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
        //followCam = FindObjectOfType<FollowCamera>();
        //rotateCam = FindObjectOfType<RotateCamera>();
        cameraMovement = FindObjectOfType<CameraMovement>();
        crowdMovement = FindObjectOfType<crowdMovement>();
        familyMovement = FindObjectOfType<FamilyMovement>();


        //for (int i = 0; i < Monsters.Length; i++)
        //{
        //    Monsters[i].gameObject.SetActive(false);
        //}
        //for (int i = 0; i < MonsterTraps.Length; i++)
        //{
        //    MonsterTraps[i].SetActive(false);
        //}
        //for (int i = 0; i < Booms.Length; i++)
        //{
        //    Booms[i].SetActive(false);
        //}
        for(int i = 0; i<6; i++)
        {
            zeroMemories.Add(false);
        }
        for (int i = 0; i < 3; i++)
        {
            zeroFamily.Add(false);
        }
        crowdMovement.enabled = false;
        familyMovement.gameObject.SetActive(false);
        familyMovement.enabled = false;
        Zero = FindObjectOfType<PlayerInput>().gameObject;
    }

    public bool family = false;
    public bool memory = false;

    void Update()
    {
        if (GameManager.instance.gameEnding)
        {
            memrySlider.gameObject.SetActive(false);
            GameManager.instance.MemoryPercent = memrySlider.value;
            for (int i = 0; i < zeroFamily.Count; i++)
            {
                if (zeroFamily[i] == true)
                {
                    family = true;
                }
                else
                {
                    family = false;
                    break;
                }
            }
            for (int i = 0; i < zeroMemories.Count; i++)
            {
                if (zeroMemories[i] == true)
                {
                    memory = true;
                }
                else
                {
                    memory = false;
                    break;
                }
            }
            if (GameManager.instance.findCar)//&& GameManager.instance.FamilyPercent >= 60.0f)
            {
                if(memory)
                {

                    video.clip = Normal;
                    Debug.Log("Normal Ending");
                }
                else if(memory && family)
                {
                    video.clip = Happy;
                    Debug.Log("Happy Ending");
                }
            }
            else
            {
                video.clip = Bad;
                Debug.Log("Bad Ending");
            }

            if (!videoStart)
            {
                GameManager.instance.SetGameStateToStory();
                videoStart = true;
                videoRaw.gameObject.SetActive(true);
                video.Play();
            }
            if (videoStart && !videoEnd)
            {
                PlayTime += Time.deltaTime;
                if (PlayTime >= video.clip.length)
                {
                    //GameManager.instance.SetGameStateToPlay();
                    //videoRaw.gameObject.SetActive(false);
                    //video.Stop();
                    //PlayTime = 0;
                    videoEnd = true;
                }
            }

            return;
        }

       
        if (GameManager.instance.IsStoryStateEnjoy())
        {
            getEnzoMemory = GameManager.instance.belongEmotions[1];
            if (getEnzoMemory && GameManager.instance.spawnMemories[1].activeSelf)
            {
                questManager.questId = 40;
                questManager.questAcitonIndex = 2;
                dialogueManager.zeroTalk = true;
                dialogueManager.Action(Zero);
                BurgerBarrier.SetActive(false);

                //for (int i = 0; i < MonsterTraps.Length; i++)
                //{
                //    MonsterTraps[i].SetActive(true);
                //}
                //for (int i = 0; i < Monsters.Length; i++)
                //{
                //    Monsters[i].gameObject.SetActive(true);
                //}

                GameManager.instance.spawnMemories[1].SetActive(false);
                UIManager.instance.OnOffEnzoNote(true);
            }

            //몬스터 발견 -> 제로 혼잣말 -> 주의사항 온

            if (getEnzoMemory)
            {
                //checkInCameraMonster();
            }
            if(questManager.questId == 60 && questManager.questAcitonIndex == 0)
            {
                if (findMonsters && !zeroEnzoTalk)
                {
                    zeroEnzoTalk = true;
                    UIManager.instance.ShowAndHideCautionWindow(true);
                    UIManager.instance.UpdateCautionText("놀이공원을 즐겨봐");

                    //for(int i = 0; i<Booms.Length; i++)
                    //{
                    //    Booms[i].SetActive(true);
                    //}
                }
            }


            //first find store
            if ( !findStore && findMonsters)
            {
                float distance = (Store.position - Zero.transform.position).magnitude;
                if(distance < 3.0f)
                {
                    findStore = true;
                    questManager.questId = 70;
                    questManager.questAcitonIndex = 0;
                    dialogueManager.zeroTalk = true;
                    dialogueManager.Action(Zero);
                }

            }
            //60 + 14000
            //find family car
            if (Zero.GetComponent<PlayerInput>().scanObject && !findFamilyCar)
            {
                ObjData obj = Zero.GetComponent<PlayerInput>().scanObject.GetComponent<ObjData>();
                questManager.questId = 60; questManager.questAcitonIndex = 0;
                if (obj.id == 14000 && GameManager.instance.IsGameStateDialogue() )
                {
                    findFamilyCar = true;
                    GameManager.instance.findCar = true;
                }
            }
            if (findFamilyCar && questManager.questId == 70 && questManager.questAcitonIndex == 0 && dialogueManager.talkIndex == 0)
            {
                UIManager.instance.UpdateEnjoyCar(enjoyFamilyImg);
                //Debug.Log("사진업데이트왜안해ㅠㅠ");
            }



            //enter the store
            if (Camera.main.orthographic && !enterStore)
            {
                questManager.questId = 70;
                questManager.questAcitonIndex = 1;
                dialogueManager.zeroTalk = true;
                dialogueManager.Action(Zero);
                enterStore = true;
            }

            if (questManager.questId == 70 && questManager.questAcitonIndex == 5)
            {
                if (!videoStart)
                {
                    GameManager.instance.SetGameStateToStory();
                    videoStart = true;
                    videoRaw.gameObject.SetActive(true);
                    video.clip = Burger;
                    video.Play();
                }
            }
            if (videoStart && !videoEnd)
            {
                PlayTime += Time.deltaTime;
                if ( PlayTime >= 20 )
                {
                    GameManager.instance.SetGameStateToPlay();
                    videoRaw.gameObject.SetActive(false);
                    video.Stop();
                    PlayTime = 0;
                    dialogueManager.zeroTalk = true;
                    dialogueManager.Action(Zero);
                    for(int i = 0; i< wantMats.Length; i++)
                    {
                        wantMats[i].SetFloat("_blend", 1);
                        //wantMats[i].SetColor("_Color", Color.HSVToRGB(193,193,193));
                    }
                    //questManager.questId = 90;
                    //questManager.questAcitonIndex = 0;
                    videoEnd = true;
                }
            }
            if (questManager.questId == 70 && questManager.questAcitonIndex == 5 && dialogueManager.talkIndex == 1 )
            {
                UIManager.instance.UpdateEnjoyBurgerText();
            }
        }

        else if (GameManager.instance.IsStoryStateWant())
        {
            getWantMemory = GameManager.instance.belongEmotions[2];
            if (getWantMemory && GameManager.instance.spawnMemories[2].activeSelf)
            {
                memrySlider.gameObject.SetActive(true);
                videoStart = false;
                videoEnd = false;
                questManager.questId = 80;
                questManager.questAcitonIndex = 1;
                dialogueManager.zeroTalk = true;
                dialogueManager.Action(Zero);

                //1. crowds / faimily 활성화로 변경.
                crowdMovement.enabled = true;

                GameManager.instance.spawnMemories[2].SetActive(false);
                UIManager.instance.OnOffWantNote(true);
            }

            if (GameManager.instance.IsGameStatePlay() && getWantMemory)
            {
                for (int i = 0; i < wantNpc.childCount; i++)
                {
                    wantNpc.GetChild(i).transform.position += Vector3.left * 0.5f * Time.deltaTime;
                }
            }

            //16000~21000


            if (Zero.GetComponent<PlayerInput>().scanObject)
            {
                ObjData obj = Zero.GetComponent<PlayerInput>().scanObject.GetComponent<ObjData>();
                if (obj.id == 16000&&!zeroMemories[0])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 0;
                    if (GameManager.instance.IsGameStateDialogue()) {

                        zeroMemories[0] = true;
                        memrySlider.value += 0.18f;
                        GameManager.instance.MemoryPercent += 0.18f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", 1 - memrySlider.value);
                        }
                    }
                }
                else if (obj.id == 17000 && !zeroMemories[1])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 1;
                    if (GameManager.instance.IsGameStateDialogue()) {

                        zeroMemories[1] = true;
                        memrySlider.value += 0.18f;
                        GameManager.instance.MemoryPercent += 0.18f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", 1 - memrySlider.value);
                        }
                    }
                        
                }
                else if (obj.id == 18000 && !zeroMemories[2])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 2;
                    if (GameManager.instance.IsGameStateDialogue()) {


                        zeroMemories[2] = true;
                        memrySlider.value += 0.18f;
                        GameManager.instance.MemoryPercent += 0.18f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", 1 - memrySlider.value);
                        }
                    }
                }
                else if (obj.id == 19000 && !zeroMemories[3])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 3;
                    if (GameManager.instance.IsGameStateDialogue()) {
  
                        zeroMemories[3] = true;
                        memrySlider.value += 0.18f;
                        GameManager.instance.MemoryPercent += 0.18f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", 1 - memrySlider.value);
                        }
                    }
                        
                }
                else if (obj.id == 20000 && !zeroMemories[4])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 4;
                    if (GameManager.instance.IsGameStateDialogue()) {

                        zeroMemories[4] = true;
                        memrySlider.value += 0.18f;
                        GameManager.instance.MemoryPercent += 0.18f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", 1 - memrySlider.value);
                        }
                    }
                        
                }
                else if (obj.id == 21000 && !zeroMemories[5])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 5;
                    if (GameManager.instance.IsGameStateDialogue()) {

                        zeroMemories[5] = true;
                        memrySlider.value += 0.18f;
                        GameManager.instance.MemoryPercent += 0.18f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", 1 - memrySlider.value);
                        }
                    }
                        
                }
                //===========
                if (obj.id == 5000 && !zeroFamily[0])
                {

                    questManager.questId = 90;
                    questManager.questAcitonIndex = 0;
                    if (GameManager.instance.IsGameStateDialogue())
                    {
                        zeroFamily[0] = true;
                        memrySlider.value -= 0.35f;
                        GameManager.instance.FamilyPercent += 0.35f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", memrySlider.value + 0.35f);
                        }
                    }

                }
                else if (obj.id == 6000 && !zeroFamily[1])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 0;
                    if (GameManager.instance.IsGameStateDialogue())
                    {
                        zeroFamily[1] = true;
                        memrySlider.value -= 0.35f;
                        GameManager.instance.FamilyPercent += 0.35f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", memrySlider.value + 0.35f);
                        }
                    }
                }
                else if (obj.id == 7000 && !zeroFamily[2])
                {
                    questManager.questId = 90;
                    questManager.questAcitonIndex = 0;
                    if (GameManager.instance.IsGameStateDialogue())
                    {
                        zeroFamily[1] = true;
                        memrySlider.value -= 0.35f;
                        GameManager.instance.FamilyPercent += 0.35f;
                        for (int i = 0; i < wantMats.Length; i++)
                        {
                            wantMats[i].SetFloat("_blend", memrySlider.value + 0.35f);
                        }
                    }
                }
            }

            //questId == 100 && questActionIndex == 1 일때 가족들이 활성화되면 되구, 대사를 퀘스트 안이 아니라 밖(기본 대사)로 빼서 대화 순서 상관없이 진행할 수 있습니다! npcId는 엄마가 5000, 아빠가 6000, 누나가 7000입니다.
            if (GameManager.instance.MemoryPercent > 0.9f && !familyMovement.enabled && GameManager.instance.findCar)
            {
                familyMovement.gameObject.SetActive(true);
                familyMovement.enabled = true;
            }


            //if (questManager.questId == 100 && questManager.questAcitonIndex == 1)
            //{
            //    zeroFamily[0] = true;
            //}
            //if (questManager.questId == 100 && questManager.questAcitonIndex == 2)
            //{
            //    zeroFamily[1] = true;
            //}
            //if (questManager.questId == 100 && questManager.questAcitonIndex == 3)
            //{
            //    zeroFamily[2] = true;
            //}
        }





    }






    #region ENJOY - Enzo
    void SpawnEnzoEmotion()
    {
        if (spawnEnzoMemory) return;
        GameManager.instance.spawnMemories[1].SetActive(true);
        spawnEnzoMemory = true;
        //spawnHuiMemory = true;

    }

    #endregion


    //#region Monster


    //public void checkInCameraMonster()
    //{
    //    if (findMonsters) return;

    //    for(int i= 0; i<Monsters.Length; i++)
    //    {
    //        Vector3 viewPos = Camera.main.WorldToViewportPoint(Monsters[i].position);
    //        float distance = (Zero.transform.position - Monsters[i].transform.position).magnitude;
    //        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
    //        {
    //            if (distance <= 30.0f)
    //            {
    //                findMonsters = true;

    //                cameraMovement.SetCameraSetting(Monsters[i], 0.1f, new Vector3(10, 3, -10));

    //                GameManager.instance.SetCamStateToFocus();
    //                GameManager.instance.SetGameStateToStory();

    //                StartCoroutine(endFocusingMonster());
    //            }
    //        }
    //    }

    //}

    //IEnumerator endFocusingMonster()
    //{
    //    yield return new WaitForSeconds(3.0f);

    //    cameraMovement.SetCameraSetting(Zero.transform, 3.0f, new Vector3(0, 5, -7));
    //    GameManager.instance.SetCamStateToFollow();

    //    questManager.questId = 50;
    //    questManager.questAcitonIndex = 0;
    //    dialogueManager.zeroTalk = true;
    //    dialogueManager.Action(Zero);
    //}



    //#endregion

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