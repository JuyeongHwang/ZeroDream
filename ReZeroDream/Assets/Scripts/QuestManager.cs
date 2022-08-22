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

    [HideInInspector] public string qwDescript;
    [HideInInspector] public string qwContent;
    [HideInInspector] public string qwReward;
    [HideInInspector] public string qwName;

    [HideInInspector] public bool focusing = false;

    private SpriteRenderer huiQuestSprite;
    public Sprite[] questImgArr;

    public GameObject huiQuestImg;
    public GameObject huiMemoryQuestImg;
    public GameObject catQuestImg;
    public GameObject flowerQuestImg;
    public GameObject[] carQuestImg;

    private void Start()
    {
        questList = new Dictionary<int, QuestData>(); //초기화
        GenerateQuestData();

        huiQuestSprite = huiQuestImg.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        nowDialogueObject = questList[questId].npcId[questAcitonIndex];

        if (questId == 10 && questAcitonIndex == 2)
        {
            huiQuestImg.SetActive(true);
        }
        else if (questId == 20 && questAcitonIndex == 1)
        {
            huiQuestSprite.sprite = questImgArr[1];
            huiMemoryQuestImg.SetActive(true);
        }
        else if (questId == 20 && questAcitonIndex == 4)
        {
            catQuestImg.SetActive(false);
            huiQuestSprite.sprite = questImgArr[0];
        }
        else if (questId == 30 && questAcitonIndex == 0)
        {
            huiQuestSprite.sprite = questImgArr[1];
            flowerQuestImg.SetActive(true);
        }
        else if (questId == 30 && questAcitonIndex == 2)
        {
            flowerQuestImg.SetActive(false);
        }
        else if (questId == 30 && questAcitonIndex == 3)
        {
            huiQuestImg.SetActive(false);
        }
        else if (questId == 30 && questAcitonIndex == 3)
        {
            huiQuestImg.SetActive(true);
            huiQuestSprite.sprite = questImgArr[2];
        }
        else if (questId == 60 && questAcitonIndex == 0)
        {
            for (int i = 0; i < 5; i++)
                carQuestImg[i].SetActive(true);
        }
        else if (questId == 60 && questAcitonIndex == 3)
        {
            for (int i = 0; i < 5; i++)
                carQuestImg[i].SetActive(false);
        }
    }


    void GenerateQuestData()
    {   //이 부분 세부 목록 내용 수정 필요
        questList.Add(10, new QuestData("???와 대화하기", new int[] { 3000, 3000, 1000 },
            new string[] { "", "", "???에게 대화를 걸어 이곳에 대한 정보를 얻어보자." }, new string[] { "", "", "???와 대화하기" }, new string[] { " ", " ", " " }));

        questList.Add(20, new QuestData("???에 대해 알아보기", new int[] { 1000, 3000, 3000, 2000, 1000 },
            new string[] { "알 수 없는 말을 하는 소녀이다. 소녀에게 다시 한 번 더 대화를 걸어 단서를 얻자.", "주변을 둘러보고 오자.", "주변을 둘러보고 오자.", "주변을 둘러보고 오자.", "마치 메모장의 찢긴 페이지 같은 종이를 획득했다. 다시 소녀에게 돌아가보자." },
            new string[] { "???와 대화하기", "두루마리 획득하기", "두루마리 획득하기", "고양이와 상호작용하기", "???와 대화하기" },
            new string[] { " ", "두루마리", "두루마리", "고양이에 대한 기억", "" }));

        questList.Add(30, new QuestData("???을 찾아보기", new int[] { 10000, 11000, 1000, 3000, 1000 },
            new string[] { "내가 좋아했던 것들에는 무엇이 있었을까? 주변을 둘러보며 찾아보자.", "내가 좋아했던 것들에는 무엇이 있었을까? 주변을 둘러보며 찾아보자.", "좋아하던 꽃에 대한 기억이 떠오른다. 소녀, 희를 찾아가 그 사실을 말해보자.", " ", "주변의 색이 변했다. 어떻게 된 일인지 희에게 물어보자." },
            new string[] { "꽃과 상호작용하기", "꽃과 상호작용하기", "희와 대화하기", "", "희와 대화하기" }, 
            new string[] { "꽃에 대한 기억", "꽃에 대한 기억", "온전해진 메모장의 첫 페이지", "", "다음 지역 잠금 해제" }));


        questList.Add(40, new QuestData("멈춰버린 도시에 대해 알아보기", new int[] { 3000, -10000, 3000 }, //일단 대화 뜨면 안되니까 
            new string[] { "아까 떨어지면서 옥상에서 무언가를 본 것 같다. 옥상 위를 확인해 이 멈춰버린 놀이공원? 도시에 대한 단서를 얻어보자.", "", "옥상에서 내려가 주변을 둘러보며 도화지의 그림에 대한 단서를 얻어보자." },
            new string[] { "옥상 위 확인하기", "", "주변 둘러보기" },
            new string[] { "", " ", "" }));

        questList.Add(50, new QuestData("멈춰버린 도시에 대해 알아보기(2)", new int[] { 3000 },
            new string[] { "도화지를 획득하니 존재하지 않았던 이상한 존재와 여러 발판들이 생겨났다. 이 도시와 관련이 있는지 발판을 직접 밟아보며 확인해보자."},
            new string[] { "발판 밟아보기" },
            new string[] { "" }));

        questList.Add(60, new QuestData("메모장 채우기", new int[] { 12000, 13000, 14000 },
            new string[] { "메모장에 알 수 없는 글자들이 있다. 즐거웠던 기억들에는 무엇이 있었을까? 주변을 둘러보며 찾아보자.", "메모장에 알 수 없는 글자들이 있다. 즐거웠던 기억들에는 무엇이 있었을까? 주변을 둘러보며 찾아보자.", "메모장에 알 수 없는 글자들이 있다.즐거웠던 기억들에는 무엇이 있었을까? 주변을 둘러보며 찾아보자." },
            new string[] { "자동차에 대해 조사하기\n햄버거에 대해 조사하기", "자동차에 대해 조사하기\n햄버거에 대해 조사하기", "자동차에 대해 조사하기\n햄버거에 대해 조사하기" },
            new string[] { "자동차에 대한 기억\n■■에 대한 단서\n햄버거에 대한 기억", "자동차에 대한 기억\n■■에 대한 단서\n햄버거에 대한 기억", "자동차에 대한 기억\n■■에 대한 단서\n햄버거에 대한 기억" }));

        //햄버거집 발견하면 questId == 70, zeroTalk = true
        //(자동차와 대화하지 않고 햄버거집 앞으로 일정 거리 내에 오는 경우도 마찬가지. 대신 그러면 메모장에서 자동차 그림이 사라지거나 X표시 되어도 좋을듯)
        questList.Add(70, new QuestData("메모장 채우기", new int[] { 3000, 3000, 4000, 15000, 4000 },
            new string[] { "햄버거집을 들어가 메모장의 햄버거 그림에 대한 단서를 얻고, 배고픔을 채워보자.", "직원에게 대화를 걸어 주문을 해보자.", "엔조가 말하는 '그 햄버거'란 무엇일까? 가게 곳곳을 살펴보며 다른 사람이 먹고 있지 않은 햄버거가 있는지 찾아보자.",
            "햄버거를 얻었다. 가게로 돌아가 엔조에게 대화를 걸어보자.", " " },
            new string[] { "햄버거집 들어가기", "직원과 대화하기", "주변을 둘러보며 햄버거 찾기", "엔조와 대화하기", " " },
            //그리고 가게 내부의 다른 npc랑도 대화 추가해도 좋을 것 같아요!
            new string[] { " ", " ", "햄버거", "온전해진 메모장", " " }));

        questList.Add(80, new QuestData("퀘스트 끝", new int[] { -10000 },
           new string[] { "오류 때문에 넣음." }, new string[] { " " }, new string[] { " " }));

        questList.Add(90, new QuestData("원트 시작", new int[] { -10000 },
        new string[] { "원트지역." }, new string[] { " " }, new string[] { " " }));
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
        else if (id == 14000 && questId == 60 && questAcitonIndex == 0 || id == 14000 && questId == 60 && questAcitonIndex == 1)
        {
            questId = 70;
            questAcitonIndex = 0;
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