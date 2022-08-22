using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; //대화데이터를 저장할 dictionary 변수 (키: object id, 값: 대화내용)

    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr; //초상화 스프라이트 저장할 배열 

    private void Start()
    {
        talkData = new Dictionary<int, string[]>(); //초기화
        portraitData = new Dictionary<int, Sprite>();

        GenerateData();
    }

    //Talk Data
    void GenerateData()
    {
        talkData.Add(-1000, new string[] { "대화할 수 없나봐. 관련이 없는 것 같아.:1" });

        talkData.Add(1000, new string[] { "룰루랄라...:2" });
        talkData.Add(2000, new string[] { "냐옹:0" });
        talkData.Add(4000, new string[] { "햄버거가 다 팔렸어.:3" });
        
        //talkData.Add(5000, new string[] { "어서 학교로 가야 해.:0" }); //누나
        //talkData.Add(6000, new string[] { "어서 학교로 가야 해.:0" }); //아빠
        //talkData.Add(7000, new string[] { "어서 학교로 가야 해.:0" }); //엄마

        talkData.Add(10000, new string[] { "이름 모를 꽃이다.:1" });
        talkData.Add(11000, new string[] { "유독 향기로운 꽃이다.:1" });
        talkData.Add(12000, new string[] { "경찰차다. 그러나 사람은 없는 것 같다.:1" });
        talkData.Add(13000, new string[] { "평범한 자동차이다.:1" });
        talkData.Add(14000, new string[] { "무늬가 멋진 차네.:1", "어떤 기억이 떠오르는 것 같기도 해... 조금 뒤에 다시 조사해보자.:1" }); //퀘스트 대화 때문에 이름을 제로로 설정해서...
        


        //Quest Talks
        talkData.Add(10 + 3000, new string[] { "여기는... 어디지? 세상이 전부 흑백이야.:1" });
        talkData.Add(11 + 3000, new string[] { "저 아이는 누구지...? 왜 이런 곳에 있는 걸까?:1" });
        talkData.Add(12 + 1000, new string[] { "넌 날 전혀 모르는구나.:2", "나에 대해서 알아봐 줄래?:2" });

        talkData.Add(20 + 1000, new string[] { "난 어디에든 있어.:2", "주변을 둘러보고 오겠니?:2" });
        talkData.Add(21 + 3000, new string[] { "이게 뭐지? 더 가까이 다가가볼까?:1" });
        talkData.Add(22 + 3000, new string[] { "이 종이는 어디에 사용하는거지? 그림이 그려져있어...:1" });
        talkData.Add(23 + 2000, new string[] { "냐옹.:0", "부드럽고, 따뜻해... 무언가 차오르는 느낌이 느껴져.:1", ":0", "맞아. 내가 키우던 고양이 이름은...:1", "냐아.:0" });
        talkData.Add(24 + 1000, new string[] { "그 고양이를 만났구나.:2", "하지만 여전히 나를 모르는 것 같네.:2", "나를 조금 더 찾아와 줄래?\n너가 좋아했던 것들을 떠올려봐.:2" });

        
        talkData.Add(30 + 10000, new string[] { "이건... 아닌 거 같아.:1" });
        talkData.Add(30 + 11000, new string[] { "익숙하고 좋은 향기가 나...:1", "민들레꽃이구나. 꽃말은 행복...:1", "고양이와 민들레,\n이 종이에 적혀있던 건 모두 내가 좋아하던 것들이야.:1","그 아이는 이 모든 걸 어떻게 알고있는 걸까?:1"});

        talkData.Add(32 + 1000, new string[] { "이제 내가 무엇을 좋아했었는지 기억났어.:1", "도대체 너는 누구야?:1", "나는 희야. 나는 너가 좋아하는 것이라면\n무엇이든 될 수 있고, 어디에도 있을 수 있지.:2", "이제 나에 대해 알겠니?:2" });
        //색 변화 > 제로 혼잣말 여기서 하면 좋을듯
        talkData.Add(33 + 3000, new string[] { "색이 변했잖아?:1", "지금까지 내가 있던 곳이 이렇게 예쁜 곳이었구나...:1" });
        talkData.Add(34 + 1000, new string[] { "후후, 본래의 색을 되찾았을 뿐이야.:2", "아쉽지만 이젠 다음으로 나아가야 할 시간이야.:2", "너라면 잘 해낼 수 있을 거야.:2", "난 언제나 너와 함께하고 있으니, 앞으로는 날 잊지 말아줘.:2"});

        talkData.Add(40 + 3000, new string[] { "분위기가 달라졌어.:1", "여기는 도시가 멈춰있는 것만 같아.:1", "떨어지면서 저 건물 옥상에서 무언가를 본 것 같은데...:1" , "확인해볼까?:1" });
        talkData.Add(42 + 3000, new string[] { "아까 얻었던 도화지잖아?:1", "그렇지만 내용이 달라. 즐거웠던 기억이라...:1", "햄버거와 자동차라니, 무슨 의미일까?.:1", "다시 내려가 주변을 둘러보며 단서를 얻어보자.:1" });
        talkData.Add(50 + 3000, new string[] { "저건 뭐지?:1", "이상한 생명체가 돌아다니고 있어.\n분명 아까는 없었는데...:1", "무언가를 지키고 있는 것 같아.\n하지만 왠지 날 공격할 것 같지는 않아..:1", "그런데 저 발판들은 뭐지?:1", "발판마다 조금씩 다른 것 같으니 하나씩 밟으며 확인해보자.:1" });

        talkData.Add(60 + 12000, new string[] { "경찰차다. 그러나 사람은 없는 것 같다.:1" });
        talkData.Add(60 + 13000, new string[] { "평범한 자동차이다.:1" });
        talkData.Add(60 + 14000, new string[] { "뭐지? 이 차는 뭔가 익숙해.:1", "그러고보니 예전에 가족들과 함께 이 차를 타고 놀이공원에 갔었지.:1", "그때 정말 즐거웠었는데...:1" }); //이름 일단 제로로 (제로가 말한 것처럼 나와야 하니까)

        talkData.Add(70 + 3000, new string[] { "햄버거집을 보니 배가 고프네... 한 번 들어가볼까?:1" }); 
        talkData.Add(71 + 3000, new string[] { "우와, 여기는 사람이 많잖아! :1", "사람들이 먹고 있는 음식이 맛있어 보여.:1", "아, 배고파... 직원에게 가서 햄버거를 주문해보자.:1" });
        talkData.Add(72 + 4000, new string[] { "미안하지만 더이상 햄버거는 팔 수 없어.\n이미 재료가 다 떨어졌거든.:3", "뭐, 정 배고프다면 주변에서 직접 찾아봐.:3", "만약 햄버거를 얻어오면 자리와 음료 정도는 제공해 줄게.:3" });
        
        talkData.Add(73 + 15000, new string[] { "엔조가 말했던 햄버거가 설마 이건가?:1", "이 햄버거는 너무 커서 가게로 가져갈 수 없을 것 같은데...\n다시 엔조에게 가서 물어보자.:1" });
        talkData.Add(74 + 4000, new string[] { "뭐? 그 햄버거를 먹어도 되냐고?:3", "아니, 햄버거를 찾아오라는 건 농담이었어. :3", "자리에 앉아있으면 햄버거를 가져다줄게.:3" });
        //끝나면 시네마틱 재생
        talkData.Add(75 + 3000, new string[] { "맞아... 방과후에 항상 친구들과 축구를 한 후 햄버거집을 가곤 했었어.:1", "정말 즐거웠지만, 이젠 그때로 다시 돌아갈 수 없겠지...:1" });

        //이 대사 끝나면... 또 주변 지형이 바뀌는 건가? want로?
        //questId = 80 대사 & 퀘스트 내용수정 필요!!
        talkData.Add(80 + 3000, new string[] { "여긴 또 어딜까... 여전히 시간이 멈춰있어.:1", "사람들이... 어쩌고저쩌고 군중들에 대한 설명.:1", "아까 그 도화지가 또 있다는 설명.:1" });
        talkData.Add(81 + 3000, new string[] { "도화지를 얻었다는 설명:1 ", "등굣길 같다. 마찬가지로 걸어보면 도화지에 대한 단서를 얻고 기억을 찾을 수 있겠지 어쩌고저쩌고:1" });
        //(가족 찾았으면 욕망, 사랑, 슬픔 다 있고 못 찾았으면 욕망이랑 슬픔만 

        //순서대로 진행해야 해서 퀘스트 표시? 이미지 일단 차례대로 넣어놓긴 했는데 불필요한 것 같아서 비활성화했습니당
        //questId = 90 퀘스트 내용 수정 필요
        talkData.Add(90 + 16000, new string[] { "등교하는 학생들인 건가?:1", "시간이 멈춰있지만 사이가 좋아보이네.:1", "왠지 모르겠지만 앞으로도 저 아이들이 잘 지냈으면 좋겠어.:1" });
        talkData.Add(91 + 17000, new string[] { "우와, 축구를 하고 있잖아? 재밌겠다!:1", "시간이 다시 흐르면 나도 함께할 수 있냐고 물어봐야지!:1" });
        talkData.Add(92 + 18000, new string[] { "이 사람들은 등굣길에서 생일파티를 하고 있네:1", "뭔가 이상한 것 같지만... 분명한 건 즐거워 보여.:1", "올해 내 생일파티는 어떤 모습이었더라...?:1" });
        talkData.Add(93 + 19000, new string[] { "아까 그 애잖아. 왜 혼자있는 걸까?:1", "뒷모습이 슬퍼보여.\n왠지 나까지 외로워지는 기분이야:1" });
        talkData.Add(94 + 20000, new string[] { "이번에도 혼자네...:1", "함께 축구를 하던 친구들은 어디로 간 걸까?:1", "그런데 저 모습이 왜이렇게 익숙하지?" });
        talkData.Add(95 + 21000, new string[] { "맞아... 올해의 내 생일파티는 이랬었지.:1", "아무도 오지 않았고, 초대할 친구조차 없었어:1", "울고 있는 저 소년도, 친구들과 놀며 즐거워하던 소년도 모두 '나'구나.:1", "어느날 갑자기 아무 이유 없이 점점 소외당하고 무시당하던 '나'...:1", "이제 다 기억났어." });

        // 뭔가 이러고.... 메모장들이 쫙 나타나거나 하면 좋을듯... 시네마틱으로?
        //그리고 zeroTalk = true, 제로혼잣말 나와야함

        //questId = 100 퀘스트 내용 수정 필요
        talkData.Add(100 + 3000, new string[] { "나는 수면제를 먹고 잠들었었지.:1", "그렇다면 이곳은 내 꿈속 세상인 걸까?:1", "차라리 이대로 깨어나고 싶지 않아.\n어차피 날 좋아하는 사람은 아무도 없는 걸.:1" });
        // 배드 엔딩인 경우 여기서 바로 배드엔딩으로
        
        // 노멀 or 해피면 가족들과 대화 가능하게 계속 진행/ 가족들 대사 내용 수정해야 함. 현재 npc랑 ObjData도 없음.
        talkData.Add(101 + 5000, new string[] { "제로야 엄마다.:0" });
        talkData.Add(102 + 6000, new string[] { "제로야 아빠다.:0" });
        talkData.Add(103 + 7000, new string[] { "제로야 누나다.:0" });
        
        // 노멀 or 해피 엔딩으로 어떻게 잘...




        //portraitArr 0: none/ 1: zero/ 2: happy/ 3:enzo
        portraitData.Add(1000 + 2, portraitArr[2]); //해피
        portraitData.Add(1000 + 1, portraitArr[1]); //해피 >> 제로
        portraitData.Add(3000 + 1, portraitArr[1]); //제로
        portraitData.Add(2000 + 0, portraitArr[0]); //고양이 > none
        portraitData.Add(2000 + 1, portraitArr[1]); //고양이 > zero
        portraitData.Add(4000 + 3, portraitArr[3]); //엔조 
        portraitData.Add(5000 + 0, portraitArr[0]); //제로엄마 
        portraitData.Add(6000 + 0, portraitArr[0]); //아빠 
        portraitData.Add(7000 + 0, portraitArr[0]); //누나 

        portraitData.Add(10000 + 1, portraitArr[1]); //이름모를꽃 > zero
        portraitData.Add(11000 + 1, portraitArr[1]); //향기로운꽃 > zero

        portraitData.Add(12000 + 1, portraitArr[1]); //경찰차 > zero
        portraitData.Add(13000 + 1, portraitArr[1]); //자동차 > zero
        portraitData.Add(14000 + 1, portraitArr[1]); //맞는차 > zero
        portraitData.Add(15000 + 1, portraitArr[1]); //햄버거 > zero

        portraitData.Add(16000 + 1, portraitArr[1]); 
        portraitData.Add(17000 + 1, portraitArr[1]);
        portraitData.Add(18000 + 1, portraitArr[1]); 
        portraitData.Add(19000 + 1, portraitArr[1]);
        portraitData.Add(20000 + 1, portraitArr[1]);
        portraitData.Add(21000 + 1, portraitArr[1]);


    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id)) //talkData에 현재 진행 중인 아이디가 없을 때
        {
            if (!talkData.ContainsKey(id - id % 10)) //해당 퀘스트에 지정된 대사 아예 x
            {
                if (talkData.ContainsKey(id - id % 100))
                    return GetTalk(id - id % 100, talkIndex);
                else
                    return GetTalk(-1000, talkIndex); //"대화할 수 없나봐. 관련이 없는 것 같아." 
            }

            else //해당 퀘스트와 관련은 있지만 대화의 순서가 맞지 않을 때
            {
                return GetTalk(id - id % 10, talkIndex); //반복 대화
            }
        }

        if (talkIndex == talkData[id].Length)
        {//talkIndex와 대화의 문장 개수를 비교해 대화의 끝 확인
            return null;
        }
        else
            return talkData[id][talkIndex]; //인덱스에 따라 한 문장씩 순서대로
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

}
