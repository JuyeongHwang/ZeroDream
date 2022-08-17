using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private GameObject Zero;

    private DialogueManager dialogueManager;

    public Material HuiSkyBox;
    public Material EnzoSkyBox;

    public AudioSource Bgm;
    public AudioClip HuiBgm;
    public AudioClip EnzoBgm;

    public UIView Opening;
    public UIView OpeningText;

    public string[] openingMent;
    void Start()
    {
        Zero = FindObjectOfType<PlayerInput>().gameObject;
        dialogueManager = FindObjectOfType<DialogueManager>();
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
                GameManager.instance.SetGameStateToStory();
                Opening.FadeIn(0.3f);
                OpeningText.UpdateTextMeshProUGUI(openingMent[0]);
                startHuiOpening = true;
            }
            HuiOpeningScene();
        }
        if (GameManager.instance.IsStoryStateEnjoy())
        {
            if (!startEnzoOpening)
            {
                Bgm.clip = EnzoBgm;
                Bgm.Play();
                RenderSettings.skybox = EnzoSkyBox;
                GameManager.instance.SetGameStateToStory();
                Opening.FadeIn(0.3f);
                OpeningText.UpdateTextMeshProUGUI(openingMent[1]);
                startEnzoOpening = true;
            }

            EnzoOpeningScene();
        }
    }

    void HuiOpeningScene()
    {
        if (Opening.GetAlphaValue() >= 1.0f)
        {
            StartCoroutine(HoldAlpha());
        }
        if (Opening.GetAlphaValue() <= 0.0f && !EndHuiOpening)
        {
            EndHuiOpening = true;
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }

    void EnzoOpeningScene()
    {
        if (Opening.GetAlphaValue() >= 1.0f)
        {
            StartCoroutine(HoldAlpha());
        }
        if (Opening.GetAlphaValue() <= 0.0f && !EndEnzoOpening)
        {
            
            EndEnzoOpening = true;
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);

            //GameManager.instance.SetUserStateToHear();
            //GameManager.instance.SetGameStateToStory();
            //GameManager.instance.SetStoryStateToEnjoy();
        }
    }

    IEnumerator HoldAlpha()
    {
        yield return new WaitForSeconds(1.5f);
        Opening.FadeOut(0.3f);
    }
}
