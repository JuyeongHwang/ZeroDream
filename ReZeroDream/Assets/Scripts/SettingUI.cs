using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private UIView settingObj;
    [SerializeField] private UIView quitObj;
    [SerializeField] private UIView helpGuideObj;
    public void OnSettings()
    {
        if (!GameManager.instance.IsGameStateSetting())
        {
            return;
        }
        settingObj.Show();
        GameManager.instance.SetGameStateToSetting();

    }
    public void OffSettings()
    {
        settingObj.Hide();
        GameManager.instance.SetGameStateToPlay();
        if (GameManager.instance.IsGameStateSetting())
        {

        }
    }

    bool isActivateHelpGuide = false;
    public void OnHelpGuide()
    {
        if (!GameManager.instance.IsGameStateSetting())
        {
            return;
        }
        helpGuideObj.Show();
        GameManager.instance.SetGameStateToSetting();
    }
    public void OffHelpGuide()
    {
        helpGuideObj.Hide();
        GameManager.instance.SetGameStateToPlay();
        if (!GameManager.instance.IsGameStateSetting())
        {

        }
    }

    public void OnQuit()
    {
        if (!GameManager.instance.IsGameStateSetting())
        {
            return;
        }
        quitObj.Show();
    }
    public void OffQuit()
    {
        quitObj.Hide();
    }
    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

}
