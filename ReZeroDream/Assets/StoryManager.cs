using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private GameObject Zero;
    private DialogueManager dialogueManager;
    public UIView HuiOpening;

    void Start()
    {

        Zero = FindObjectOfType<PlayerInput>().gameObject;
        dialogueManager = FindObjectOfType<DialogueManager>();

        GameManager.instance.SetGameStateToStory();
        HuiOpening.FadeIn(0.3f);

    }

    bool EndHuiOpening = false;
    // Update is called once per frame
    void Update()
    {
        if (HuiOpening.GetAlphaValue() >= 1.0f)
        {
            StartCoroutine(HoldAlpha());
        }
        if (HuiOpening.GetAlphaValue()<=0.0f && !EndHuiOpening)
        {
            EndHuiOpening = true;
            dialogueManager.zeroTalk = true;
            dialogueManager.Action(Zero);
        }
    }

    IEnumerator HoldAlpha()
    {
        yield return new WaitForSeconds(1.5f);
        HuiOpening.FadeOut(0.3f);
    }
}
