using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId = 0; //현재 진행 중인 퀘스트 아이디
    public int questAcitonIndex = 0; //퀘스트 대화순서 변수
    public int nowDialogueObject = -1000; 

    Dictionary<int, QuestData> questList; //퀘스트 데이터 저장 

    //public AudioClip complete = null;


    //public GameObject Happy;
    ////희
    //public GameObject questImg1_1; //해피 생성
    //public GameObject questImg1_2; //해피 완료
    //public GameObject questImg1_3;//해피 진행중
    //public GameObject questImg2_1; //고양이 생성
    //public GameObject questImg3_1_1; //꽃 생성
    //public GameObject questImg3_1_2;//꽃 생성

    ////락
    //public GameObject questImg4_1; //몬스터 생성
    //public GameObject questImg4_2; //몬스터 진행                        
    ////public GameObject questImg5_1; //감정구슬 생성
    //public GameObject questImg6_1_1; //차 1 생성
    //public GameObject questImg6_1_2; //차 2 생성
    //public GameObject questImg6_1_3; //차 3 생성
    //public GameObject questImg7_1; //햄버거집 생성
    //public GameObject questImg8_1; //햄버거 생성

    //public CatNameChange catNameChange;
    //public GameObject catNameImage;


    public string qwDescript;
    public string qwContent;
    public string qwReward;
    public string qwName;

    [HideInInspector] public bool focusing = false;

    private void Start()
    {
        questList = new Dictionary<int, QuestData>(); //초기화
        GenerateQuestData();

    }

    void Update()
    {
        nowDialogueObject = questList[questId].npcId[questAcitonIndex];

        //퀘스트 생성, 진행, 완료 표시
        //if (questId == 10 && questAcitonIndex == 0)
        //{
        //    questImg1_1.SetActive(true);
        //}
        //else if (questId == 20 && questAcitonIndex == 1)
        //{
        //    questImg1_1.SetActive(false);
        //    questImg1_3.SetActive(true);
        //    //if(SpawnEmotions.instance.isPleasureBelong)
        //    //    questImg2_1.SetActive(true);

        //}
        //else if (questId == 20 && questAcitonIndex == 2)
        //{
        //    questImg2_1.SetActive(false);
        //}
        //else if (questId == 30 && questAcitonIndex == 0)
        //{
        //    questImg1_3.SetActive(false);
        //    questImg3_1_1.SetActive(true);
        //    questImg3_1_2.SetActive(true);
        //}
        //else if (questId == 30 && questAcitonIndex == 2)
        //{
        //    questImg3_1_1.SetActive(false);
        //    questImg3_1_2.SetActive(false);
        //    //OnPlayClickSound();
        //    questImg1_2.SetActive(true);
        //    ObjData objData = Happy.GetComponent<ObjData>();
        //    objData.name = "희";
        //}
        //else if (questId == 40 && questAcitonIndex == 0)
        //{
        //    questImg4_1.SetActive(true);
        //}
        //else if (questId == 70 && questAcitonIndex == 0)
        //{
        //    //DialogueManager.instance.SetQuestWindow(70);
        //    questImg4_1.SetActive(false);
        //    //questImg5_1.SetActive(false);
        //    questImg6_1_1.SetActive(true);
        //    questImg6_1_2.SetActive(true);
        //    questImg6_1_3.SetActive(true);
        //    questImg7_1.SetActive(true);
        //}
        //else if (questId == 70 && questAcitonIndex == 3) //메모장 채우기 퀘스트 완료
        //{
        //    questImg6_1_1.SetActive(false);
        //    questImg6_1_2.SetActive(false);
        //    questImg6_1_3.SetActive(false);
        //}
    }


    void GenerateQuestData()
    {
        questList.Add(10, new QuestData("???와 대화하기", new int[] { 1000 },
            new string[] { "???에게 대화를 걸어 이곳에 대한 정보를 얻어보자." }, new string[] { "???와 대화하기" }, new string[] { " " }));

        questList.Add(20, new QuestData("???에 대해 알아보기", new int[] { 1000, 2000, 1000 },
            new string[] { "알 수 없는 말을 하는 소녀이다. 소녀에게 다시 한 번 더 대화를 걸어 단서를 얻자.", "주변을 둘러보고 오자.", "알 수 없는 구슬을 획득했다. 다시 소녀에게 돌아가보자." },
            new string[] { "???와 대화하기", "구슬 획득하기 \n고양이와 상호작용하기", "???와 대화하기" },
            new string[] { " ", "구슬", " " }));

        questList.Add(30, new QuestData("???을 찾아보기", new int[] { 10000, 11000, 1000 },
            new string[] { "내가 좋아했던 것들에는 무엇이 있었을까? 주변을 둘러보며 찾아보자.", "내가 좋아했던 것들에는 무엇이 있었을까? 주변을 둘러보며 찾아보자.", "좋아하던 꽃에 대한 기억이 떠오른다. 소녀를 찾아가 그 사실을 말해보자." },
            new string[] { "주변 둘러보기", "주변 둘러보기", "희와 대화하기" }, //근데 여기서 이렇게 감정이라고 언급해도 되는 건가용..??
            new string[] { " ", " ", "활성화된 감정 구슬 \n다음 지역 잠금 해제" })); //마찬가지

        questList.Add(40, new QuestData("새로운 지역 조사하기", new int[] { -10000 }, //일단 대화 뜨면 안되니까 해피 번호로
            new string[] { "희와의 대화를 마치니 새로운 지역이 열렸다. 이곳을 돌아다니며 조사해보자." },
            new string[] { "새로운 지역 조사하기" },
            new string[] { " " }));

        questList.Add(50, new QuestData("위험한 존재에 대해 알아보기", new int[] { 3000 }, //대사 없는 경우는 -10000으로/ 제로 혼잣말은 3000으로
           new string[] { "희가 말했던 위험한 존재와 조우했다. 위험한 존재가 지키고 있는 구슬을 가져와 보자." },
           new string[] { "구슬 획득하기" },
           new string[] { "구슬 \n락 메모장 활성화" }));

        questList.Add(60, new QuestData("위험한 존재에 대해 알아보기", new int[] { -10000 }, 
           new string[] { "희가 말했던 위험한 존재와 조우했다. 위험한 존재가 지키고 있는 구슬을 가져와 보자." },
           new string[] { "구슬 획득하기" },
           new string[] { "구슬 \n메모장의 새로운 페이지 활성화" }));

        questList.Add(70, new QuestData("메모장 채워보기", new int[] { 12000, 13000, 14000, 3000, 16000 },
         new string[] { "메모장에 알 수 없는 글자들이 있다. 관련된 기억들을 찾아보자.", "메모장에 알 수 없는 글자들이 있다. 관련된 기억들을 찾아보자.", "메모장에 알 수 없는 글자들이 있다. 관련된 기억들을 찾아보자.", "메모장에 알 수 없는 글자들이 있다. 관련된 기억들을 찾아보자.", "메모장에 알 수 없는 글자들이 있다. 관련된 기억들을 찾아보자." },
         new string[] { "여행과 관련된 것들 조사하기 \n배고픔 채우기", "여행과 관련된 것들 조사하기 \n배고픔 채우기", "여행과 관련된 것들 조사하기 \n배고픔 채우기", "여행과 관련된 것들 조사하기 \n배고픔 채우기", "여행과 관련된 것들 조사하기 \n배고픔 채우기" },
         new string[] { "여행 사진, ■■에 대한 단서 \n다음 지역 잠금 해제", "여행 사진, ■■에 대한 단서 \n다음 지역 잠금 해제", "여행 사진, ■■에 대한 단서 \n다음 지역 잠금 해제", "여행 사진, ■■에 대한 단서 \n다음 지역 잠금 해제", "여행 사진, ■■에 대한 단서 \n다음 지역 잠금 해제" }));

        questList.Add(80, new QuestData("퀘스트 끝", new int[] { 1000 },
           new string[] { "오류 때문에 넣음." }, new string[] { " " }, new string[] { " " }));
    }

    public int GetQuestTalkIndex(int id) //npc id받고 퀘스트번호(퀘스트토크인덱스) 반환하는 함수
    {
        return questId + questAcitonIndex;
    }


    public string CheckQuest(int id)
    {

        if (id == questList[questId].npcId[questAcitonIndex])
        {
            questAcitonIndex++; //퀘스트 정해진 순서에 맞게 대화를 하고 대화가 끝나면 퀘스트 순서 하나씩 올리기
        }

        // 꽃 대화 순서 무시를 위한 코드
        else if (id == 11000 && questId == 30 && questAcitonIndex == 0)
        {
            questAcitonIndex = 2;
        }
        //자동차(여행) 대화 순서 무시
        else if (id == 14000 && questId == 70 && questAcitonIndex == 0 || id == 14000 && questId == 70 && questAcitonIndex == 1)
        {
            questAcitonIndex = 3;
        }

        if (questAcitonIndex == questList[questId].npcId.Length)
        {   //questActionIndex가 npcId 배열의 크기와 같으면, 즉 퀘스트 대화순서가 끝에 도달했을 때 퀘스트 번호 증가(다음 퀘스트 실행)
            NextQuest();
        }

        //퀘스트창
        qwDescript = questList[questId].questDescription[questAcitonIndex];
        qwContent = questList[questId].questContent[questAcitonIndex];
        qwReward = questList[questId].questReward[questAcitonIndex];

        return qwName = questList[questId].questName; //현재 진행중인 퀘스트 이름 반환
    }

    public void NextQuest()
    {
        questId += 10;
        questAcitonIndex = 0;
    }

}