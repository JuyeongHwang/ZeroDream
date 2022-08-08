using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds; //재생 속도 변수: 1초에 몇 개
    public bool isAnim;
    string targetMsg; //표시할 대화 메시지
    TextMeshProUGUI msgText;
    AudioSource audioSource;
    int index; 
    float interval;
    

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg) //대화 문자열 받는 함수
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart() //시작
    {
        msgText.text = "";
        index = 0;

        isAnim = true; //텍스트 애니메이션 변수

        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);

    }

    void Effecting() //재생 중
    {
        GameManager.instance.SetUserStateToHear();
        if (msgText.text == targetMsg) // 대화 다 출력
        {
            GameManager.instance.SetUserStateToInteration();
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; //string: char의 배열
        if(targetMsg[index] != ' ' && targetMsg[index] != '.')
            audioSource.Play();
        
        index++;
        Invoke("Effecting", interval); //Invoke: 시간차 반복 호출
    }

    void EffectEnd() //종료
    {
        isAnim = false;
    }
}
