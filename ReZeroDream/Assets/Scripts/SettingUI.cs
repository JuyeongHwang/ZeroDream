using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private UIView settingObj;

    public void OnSettings()
    {
        settingObj.Show();
        ChangeSettingMode();
    }
    public void OffSettings()
    {
        settingObj.Hide();
        ChangePlayMode();
    }

    public void ChangePlayMode()
    {
        GameManager.instance.playState = GameManager.PlayState.PLAY;
    }
    public void ChangeSettingMode()
    {
        GameManager.instance.playState = GameManager.PlayState.SETTING;
    }


}
