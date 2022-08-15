using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperSetting : MonoBehaviour
{
    public enum StoryState { HUI, ENJOY };
    public StoryState state = StoryState.HUI;
    public GameObject[] huis;
    public GameObject[] enjoys;
    public Transform[] startPosition;


    private void Start()
    {
        Transform player = FindObjectOfType<PlayerInput>().transform;
        QuestManager quest = FindObjectOfType<QuestManager>();
        if (state == StoryState.HUI)
        {
            quest.questId = 10;
            quest.questAcitonIndex = 0;

            GameManager.instance.SetStoryStateToHui();

            player.position = startPosition[0].position;
        }
        else if (state == StoryState.ENJOY)
        {

            for (int i = 0; i < huis.Length; i++)
            {
                huis[i].SetActive(false);
            }

            quest.questId = 40;
            quest.questAcitonIndex = 0;

            GameManager.instance.SetStoryStateToEnjoy();

            player.position = startPosition[1].position;

        }
    }
}
