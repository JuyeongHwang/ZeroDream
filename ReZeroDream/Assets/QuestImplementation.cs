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

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
    }


    void Update()
    {
        if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 0)
        {
            if (playerState.missionStart) return;
            SpawnHuiEmotion();
        }
        if (questManager.questId == 20 && questManager.questAcitonIndex == 1 && dialogueManager.talkIndex == 4)
        {
            if (UIManager.instance.catName != "") return;
            if (UIManager.instance.catNameImage.activeSelf) return;
            if (UIManager.instance.textEffect.isAnim) return;

            SetCatName();
        }
    }


    void SpawnHuiEmotion()
    {
        Debug.Log("spawn hui emotion");
        playerState.SetGoal("희 감정 구슬 줍기");
        playerState.missionStart = true;

        Instantiate(EmotionPrefabs, new Vector3(-10, 5, 3), Quaternion.identity);
        GameManager.instance.spawnEmotions[0] = true;

    }

    void SetCatName()
    {
        Debug.Log("고양이 이름 짓기 활성화");
        UIManager.instance.SetActiveCatNameImage(true);
        playerState.SetGoal("고양이 이름 짓기");
        playerState.missionStart = true;
    }

}
