using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoryManager : MonoBehaviour
{

    private QuestManager questManager;
    private PlayerState playerState;




    private bool spawnedHuiEmotion = false;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        playerState = FindObjectOfType<PlayerState>();
    }


    void Update()
    {
        if(questManager.questId == 20 && questManager.questAcitonIndex ==1 && !spawnedHuiEmotion)
        {
            Debug.Log("spawn hui emotion");
            playerState.SetGoal("희 감정 구슬 줍기");
            spawnedHuiEmotion = true;
        }

    }






}
