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
        talkData.Add(15000, new string[] { "햄버거 집을 보니 배가 고프네...:1" }); //햄버거집 처음 발견할 때 에 zeroTalk = true;


        //Quest Talks
        talkData.Add(10 + 3000, new string[] { "여기는... 어디지? 세상이 전부 흑백이야.:1" });
        talkData.Add(11 + 3000, new string[] { "저 아이는 누구지...? 왜 이런 곳에 있는 걸까 ?:1" });
        talkData.Add(12 + 1000, new string[] { "넌 날 전혀 모르는구나.:2", "나에 대해서 알아봐 줄래?:2" });

        talkData.Add(20 + 1000, new string[] { "난 어디에든 있어.:2", "주변을 둘러보고 오겠니?:2" });
        talkData.Add(21 + 3000, new string[] { "이게 뭐지? 더 가까이 다가가볼까?:1" });
        talkData.Add(22 + 3000, new string[] { "이 종이는 어디에 사용하는거지? 그림이 그려져있어...:1" });
        talkData.Add(23 + 2000, new string[] { "냐옹.:0", "부드럽고, 따뜻해... 무언가 차오르는 느낌이 느껴져.:1", ":0", "맞아. 내가 키우던 고양이 이름은...:1", "냐아.:0" });
        talkData.Add(24 + 1000, new string[] { "나를 찾아왔구나.:2", "기분이 좋아지지 않니?:2", "아직은 부족해 보이네. 날 조금 더 찾아와줘.:2", "네가 좋아했던 것들을 떠올려봐.:2", "너가 원하는 곳은 어디든 갈 수 있어.:2" });

        talkData.Add(30 + 10000, new string[] { "이건… 아닌 거 같아.:1" });
        talkData.Add(30 + 11000, new string[] { "익숙하고, 좋은 향기가 나... 지켜보는 것만으로도 좋아져...:1" });
        talkData.Add(32 + 1000, new string[] { "이젠 충분해 보이네.:2", "다음으로 나아가도 좋아.:2",
            "그러나 이 밖에는 위험한 형태로 존재하는 것들도 있으니\n조심하는 게 좋을 거야.:2", "모두가 나처럼 대화로 해결할 수 있다면 좋을텐데...:2" });
        talkData.Add(33 + 3000, new string[] { "길이 열렀어!:1", "색도 돌아온것 같아. 어떻게 된 일이지?.:1" });

        talkData.Add(40 + 3000, new string[] { "분위기가 달라졌어.:1"});
        talkData.Add(50 + 3000, new string[] { "저것이 아까 그 아이가 말한 위험한 존재인 건가.:1", "대화가 통하지 않는다고 했었지만... \n혹시 모르니까 한 번 다가가보자.:1" });

        talkData.Add(60 + 12000, new string[] { "경찰차다. 그러나 사람은 없는 것 같다.:1" });
        talkData.Add(60 + 13000, new string[] { "평범한 자동차이다.:1" });
        talkData.Add(60 + 14000, new string[] { "뭐지? 이 차는 뭔가 익숙해.:1", "그러고보니 얼마 전에 차를 타고 가족들과 함께 바다로 여행을 갔었지.:1", "그때 보았던 바다가 정말 예뻤었는데...:1" }); //이름 일단 제로로 (제로가 말한 것처럼 나와야 하니까)

        //talkData.Add(63 + 3000, new string[] { "우와! 여기는 사람이 많잖아?:1" });
        //talkData.Add(64 + 16000, new string[] { "이 자리만 비었네. 이 햄버거는 먹어도 되는 건가:1", "이 가게는 이상해. 직원도 없고... 이 자리의 주인이 있냐고 물어도 아무도 대답해주지 않아.:1", "에잇, 배고프니까 그냥 먹자!:1" });

        talkData.Add(80 + 1000, new string[] { " " });


        //portraitArr 0: none/ 1: zero/ 2: happy/ 3:enzo
        portraitData.Add(1000 + 2, portraitArr[2]); //해피
        portraitData.Add(3000 + 1, portraitArr[1]); //제로
        portraitData.Add(2000 + 0, portraitArr[0]); //고양이 > none
        portraitData.Add(2000 + 1, portraitArr[1]); //고양이 > zero
        portraitData.Add(4000 + 3, portraitArr[3]); //엔조 

        portraitData.Add(10000 + 1, portraitArr[1]); //이름모를꽃 > zero
        portraitData.Add(11000 + 1, portraitArr[1]); //향기로운꽃 > zero

        portraitData.Add(12000 + 1, portraitArr[1]); //경찰차 > zero
        portraitData.Add(13000 + 1, portraitArr[1]); //자동차 > zero
        portraitData.Add(14000 + 1, portraitArr[1]); //맞는차 > zero
        portraitData.Add(15000 + 1, portraitArr[1]); //햄버거집 > zero
        portraitData.Add(16000 + 1, portraitArr[1]); //햄버거 > zero


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
