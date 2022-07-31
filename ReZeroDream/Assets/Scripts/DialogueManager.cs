using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<DialogueManager>();
            }

            return m_instance;
        }
    }
    private static DialogueManager m_instance; // 싱글톤이 할당될 static 변수

    public bool isAction { get; private set; }

    private TalkManager talkManager;
    private QuestManager questManager;

    public int talkIndex = 0;
    
    private void Start()
    {
        talkManager = GetComponent<TalkManager>();
        questManager = FindObjectOfType<QuestManager>();

        isAction = false;
        UIManager.instance.OnOffDialogueWindow(false);
    }

    public void Action(GameObject _scan)
    {
        ObjData objData = _scan.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc, objData._name);

    }

    private void Talk(int id, bool isNpc, string name)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //데이터 종료 체크
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;

            UIManager.instance.UpdateQuestUI(id);
            UIManager.instance.OnOffDialogueWindow(isAction);

            GameManager.instance.SetGameStateToPlay();
            GameManager.instance.SetUserStateToMove();
            return;
        }

        isAction = true;
        talkIndex++;
        UIManager.instance.OnOffDialogueWindow(isAction);
        UIManager.instance.UpdateDialogeText(name, talkData.Split(':')[0]);
        UIManager.instance.UpdateDialoguePortraitImage(talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])));
        GameManager.instance.SetGameStateToDialogue();

    }


}
