using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public string[] questDescription;
    public string[] questContent;
    public string[] questReward;
    public int[] npcId;

    public QuestData(string name, int[] npc, string[] descript, string[] content, string[] reward) //구조체 생성을 위해 매개변수 생성자 작성
    {
        questName = name;
        npcId = npc;
        questDescription = descript;
        questContent = content; ;
        questReward = reward;
    }
}
