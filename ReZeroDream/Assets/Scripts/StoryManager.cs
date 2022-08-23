using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private GameObject Zero;

    private QuestManager questManager;
    private DialogueManager dialogueManager;
    private CameraMovement camMovement;

    public GameObject HuiLand;
    public GameObject EnzoLand;
    public GameObject WantLand;

    public Material HuiSkyBox;
    public Material EnzoSkyBox;
    public Material WantSkyBox;

    public AudioSource Bgm;
    public AudioClip HuiBgm;
    public AudioClip EnzoBgm;
    public AudioClip WantBgm;

    public UIView OpeningWindow;
    public UIView OpeningText;

    public string[] openingMent;
    void Start()
    {
        OpeningWindow.gameObject.SetActive(false);
        Zero = FindObjectOfType<PlayerInput>().gameObject;
        dialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        camMovement = FindObjectOfType<CameraMovement>();
    }

    bool startHuiOpening = false;
    bool startEnzoOpening = false;
    bool startWantOpening = false;

    bool EndHuiOpening = false;
    bool EndEnzoOpening = false;
    bool EndWantOpening = false;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsStoryStateHui())
        {
            if (!startHuiOpening)
            {
                UIManager.instance.HideAllCanvasNoFade();
                OpeningWindow.gameObject.SetActive(true);

                Bgm.clip = HuiBgm;
                Bgm.Play();
                RenderSettings.skybox = HuiSkyBox;
                RenderSettings.fog = false;
                Camera.main.transform.position = Zero.transform.forward * -30 + Zero.transform.up * 40 + Zero.transform.right * -7;
                camMovement.SetCameraSetting(Zero.transform, 0.15f, Zero.transform.forward * -5 + Zero.transform.up * 3 + Zero.transform.right * 2);
                GameManager.instance.SetGameStateToStory();
                OpeningWindow.FadeIn(0.3f);
                OpeningText.UpdateTextMeshProUGUI(openingMent[0]);
                startHuiOpening = true;
                HuiLand.SetActive(true);
                EnzoLand.SetActive(false);
                WantLand.SetActive(false);
            }
            HuiOpeningScene();
        }
        else if (GameManager.instance.IsStoryStateEnjoy())
        {
            if (!startEnzoOpening)
            {
                RenderSettings.fog = false;
                OpeningWindow.gameObject.SetActive(true);
                Bgm.clip = EnzoBgm;
                Bgm.Play();
                RenderSettings.skybox = EnzoSkyBox;
                GameManager.instance.SetGameStateToStory();
                OpeningWindow.FadeIn(0.3f);
                OpeningText.UpdateTextMeshProUGUI(openingMent[1]);
                startEnzoOpening = true;
                EnzoLand.SetActive(true);
                HuiLand.SetActive(false);
                WantLand.SetActive(false);
            }

            EnzoOpeningScene();
        }
        else if (GameManager.instance.IsStoryStateWant())
        {
            if (!startWantOpening)
            {
                RenderSettings.fog = true;
                Camera.main.orthographic = false;
                OpeningWindow.gameObject.SetActive(true);
                Bgm.clip = WantBgm;
                Bgm.Play();
                RenderSettings.skybox = WantSkyBox;
                GameManager.instance.SetGameStateToStory();
                OpeningWindow.FadeIn(0.3f);
                OpeningText.UpdateTextMeshProUGUI(openingMent[2]);
                startWantOpening = true;
                WantLand.SetActive(true);
                EnzoLand.SetActive(false);
                HuiLand.SetActive(false);
            }
            WantOpeningScene();
        }

        if (questManager.questId == 70 && questManager.questAcitonIndex == 5)
        {
            Bgm.Stop();
        }
    }

    void HuiOpeningScene()
    {
        if (OpeningWindow.GetAlphaValue() >= 1.0f)
        {
            StartCoroutine(HoldAlpha());
        }
        if (OpeningWindow.GetAlphaValue() <= 0.0f && !EndHuiOpening)
        {
            UIManager.instance.ShowAllCanvas();
            //UIManager.instance.HideQuestWindow();
            camMovement.SetCameraSetting(Zero.transform, 3f, new Vector3(0, 5, -7));
            EndHuiOpening = true;
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
            OpeningWindow.gameObject.SetActive(false);
        }
    }

    void EnzoOpeningScene()
    {
        if (OpeningWindow.GetAlphaValue() >= 1.0f)
        {
            StartCoroutine(HoldAlpha());
        }
        if (OpeningWindow.GetAlphaValue() <= 0.0f && !EndEnzoOpening)
        {
            
            EndEnzoOpening = true;
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
            OpeningWindow.gameObject.SetActive(false);
            //GameManager.instance.SetUserStateToHear();
            //GameManager.instance.SetGameStateToStory();
            //GameManager.instance.SetStoryStateToEnjoy();
        }
    }
    void WantOpeningScene()
    {
        if (OpeningWindow.GetAlphaValue() >= 1.0f)
        {
            StartCoroutine(HoldAlpha());
        }
        if (OpeningWindow.GetAlphaValue() <= 0.0f && !EndWantOpening)
        {
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
            EndWantOpening = true;
            //dialogueManager.zeroTalk = true;
            //dialogueManager.Action(Zero);
            OpeningWindow.gameObject.SetActive(false);
            //GameManager.instance.SetUserStateToHear();
            
            //GameManager.instance.SetStoryStateToEnjoy();
        }
    }

    IEnumerator HoldAlpha()
    {
        yield return new WaitForSeconds(1.0f);
        OpeningWindow.FadeOut(0.3f);
    }
}
