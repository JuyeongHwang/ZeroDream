using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperSetting : MonoBehaviour
{
    public enum StoryState { HUI, ENJOY, WANT };
    public StoryState state = StoryState.HUI;
    public Transform burgerStore;
    public GameObject[] huis;
    public GameObject enjoys;
    public GameObject wants;
    public Transform[] startPosition;
    public Transform[] endPosition;
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

    bool endEnjoy = false;
   
    private void Update()
    {
        if (GameManager.instance.IsStoryStateEnjoy())
        {
            burgerStore.gameObject.SetActive(true);
        }
        else
        {
            burgerStore.gameObject.SetActive(false);
        }
        if(quest.questId ==80 && quest.questAcitonIndex == 0 && !endEnjoy)
        {
            enjoys.SetActive(false);
            wants.SetActive(true);
            //GameManager.instance.SetStoryStateToWant();
            //Vector3(41.6360016,-75.9846954,-3.86319184)
            player.position = new Vector3(41.6f, -75.9f, -3.8f);
            //player.eulerAngles = endPosition[0].eulerAngles;

            endEnjoy = true;
            //Vector3(41.6100006,-76.1100006,-12.4499998)
            burgerStore.position = new Vector3(38.7f, -76f, -1.65f);
            burgerStore.eulerAngles = new Vector3(0, 90, 0f);
        }
    }
}
