using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    public void OnMousePointEnter()
    {
        GameManager.instance.SetGameStateToSetting();
        if (GameManager.instance.IsGameStatePlay())
        {

        }
    }

    public void OnMousePointExit()
    {
        if (GameManager.instance.IsGameStateSetting())
        {
            GameManager.instance.SetGameStateToPlay();
        }
    }
}
