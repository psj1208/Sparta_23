using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    public override void Opened(params object[] param)
    {
        Time.timeScale = 0f;
    }

    public override void HideDirect()
    {
        
    }

    public void OnClickStart()
    {
        Time.timeScale = 1f;
        UIManager.Hide<UIPopup>();
    }

    public void OnclickMain()
    {
        Time.timeScale = 1f;
        UIManager.Hide<UIPopup>();
        SceneManager.LoadScene("StartScene");
    }

    public void OnclickExit()
    {
        Application.Quit();
    }
}
