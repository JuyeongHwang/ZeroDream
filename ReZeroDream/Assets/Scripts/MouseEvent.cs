using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
 
    public void OnMousePointExit()
    {
        Debug.Log("exit");
        
        GameManager.instance.playState = GameManager.PlayState.PLAY;
    }

    public void OnMousePointEnter()
    {
        Debug.Log("enter");
        GameManager.instance.playState = GameManager.PlayState.SETTING;
    }


}
