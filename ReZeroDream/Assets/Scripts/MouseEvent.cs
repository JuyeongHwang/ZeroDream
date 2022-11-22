using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    public void OnMousePointEnter()
    {
        if(GameManager.instance.IsGameStateDialogue() || GameManager.instance.IsGameStateStory()) { return; }
        GameManager.instance.SetGameStateToSetting();
    }

    public void OnMousePointExit()
    {
        if (GameManager.instance.IsGameStateSetting())
        {
            GameManager.instance.SetGameStateToPlay();
        }
    }
}
