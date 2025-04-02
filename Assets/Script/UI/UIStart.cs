using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : UIBase
{
    public override void HideDirect()
    {

    }

    public override void Opened(params object[] param)
    {

    }

    public void OnclickStart()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void OnclickExit()
    {
        Application.Quit();
    }
}
