using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private GameObject Zero;

    private DialogueManager dialogueManager;
    private CameraMovement camMovement;

    public GameObject HuiLand;
    public GameObject EnzoLand;

    public Material HuiSkyBox;
    public Material EnzoSkyBox;

    public AudioSource Bgm;
    public AudioClip HuiBgm;
    public AudioClip EnzoBgm;

    public UIView OpeningWindow;
    public UIView OpeningText;

    public string[] openingMent;
    void Start()
    {
        OpeningWindow.gameObject.SetActive(false);
        Zero = FindObjectOfType<PlayerInput>().gameObject;
        dialogueManager = FindObjectOfType<DialogueManager>();
        camMovement = FindObjectOfType<CameraMovement>();
    }

    bool startHuiOpening = false;
    bool startEnzoOpening = false;
    bool EndHuiOpening = false;
    bool EndEnzoOpening = false;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsStoryStateHui())
        {
            if (!startHuiOpening)
            {
                OpeningWindow.gameObject.SetActive(true);
                Camera.main.transform.position = Zero.transform.forward * -30 + Zero.transform.up * 40 + Zero.transform.right * -7;
                camMovement.SetCameraSetting(Zero.transform, 0.15f, Zero.transform.forward * -5 + Zero.transform.up * 3 + Zero.transform.right * 2);
                GameManager.instance.SetGameStateToStory();
                OpeningWindow.FadeIn(0.3f);
                OpeningText.UpdateTextMeshProUGUI(openingMent[0]);
                startHuiOpening = true;
                EnzoLand.SetActive(false);
            }
            HuiOpeningScene();
        }
        if (GameManager.instance.IsStoryStateEnjoy())
        {
            if (!startEnzoOpening)
            {
                OpeningWindow.gameObject.SetActive(true);
                Bgm.clip = EnzoBgm;
                Bgm.Play();
                RenderSettings.skybox = EnzoSkyBox;
                GameManager.instance.SetGameStateToStory();
                OpeningWindow.FadeIn(0.3f);
                OpeningText.UpdateTextMeshProUGUI(openingMent[1]);
                startEnzoOpening = true;
                HuiLand.SetActive(false);
            }

            EnzoOpeningScene();
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

    IEnumerator HoldAlpha()
    {
        yield return new WaitForSeconds(1.0f);
        OpeningWindow.FadeOut(0.3f);
    }
}
