using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearPanel : UIBase
{
    public GameObject panel;
    public Button nextStageButton;
    
    public override void Opened(params object[] param)
    {
        Debug.Log("opened");
        ShowPanel();
    }

    public override void HideDirect()
    {
    }
    
    void Start()
    {
        panel.SetActive(false);
        nextStageButton.onClick.AddListener(OnLoadNextStageClicked);
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    void OnLoadNextStageClicked()
    {
        // HidePanel();
        StageManager.Instance.NextStage();
    }
}