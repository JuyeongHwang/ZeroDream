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
        settingObj.Show();
        GameManager.instance.SetGameStateToSetting();
        if (GameManager.instance.IsGameStateSetting())
        {

        }
    }
    public void OffSettings()
    {
        settingObj.Hide();
        GameManager.instance.SetGameStateToPlay();
        if (GameManager.instance.IsGameStateSetting())
        {

        }
    }

    public void OnHelpGuide()
    {
        helpGuideObj.Show();
        if (GameManager.instance.IsGameStateSetting())
        {

        }
    }
    public void OffHelpGuide()
    {
        helpGuideObj.Hide();
        if (!GameManager.instance.IsGameStateSetting())
        {

        }
    }

    public void OnQuit()
    {
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
