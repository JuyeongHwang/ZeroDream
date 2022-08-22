using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperSetting : MonoBehaviour
{
    public enum StoryState { HUI, ENJOY, WANT };
    public StoryState state = StoryState.HUI;
    public GameObject[] huis;
    public GameObject[] enjoys;
    public GameObject[] wants;
    public Transform[] startPosition;

    QuestManager quest;
    Transform player;
    private void Start()
    {
        player = FindObjectOfType<PlayerInput>().transform;
        quest = FindObjectOfType<QuestManager>();
        if (state == StoryState.HUI)
        {
            quest.questId = 10;
            quest.questAcitonIndex = 0;

            GameManager.instance.SetStoryStateToHui();

            player.position = startPosition[0].position;
        }
        else if (state == StoryState.ENJOY)
        {

            quest.questId = 40;
            quest.questAcitonIndex = 0;

            GameManager.instance.SetStoryStateToEnjoy();

            player.position = startPosition[1].position;

        }
        else if (state == StoryState.WANT)
        {

            quest.questId = 80;
            quest.questAcitonIndex = 0;

            GameManager.instance.SetStoryStateToWant();

            player.position = startPosition[2].position;

        }
    }

   
    private void Update()
    {
        if(quest.questId ==90 && quest.questAcitonIndex == 0 && !GameManager.instance.IsStoryStateWant())
        {
            GameManager.instance.SetStoryStateToWant();
            player.position = startPosition[2].position;
        }
    }
}
