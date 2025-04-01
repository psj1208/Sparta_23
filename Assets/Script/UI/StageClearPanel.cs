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
        nextStageButton.onClick.AddListener(OnLoadNextStageClicked);
    }

    public override void HideDirect()
    {
    }
    
    void OnLoadNextStageClicked()
    {
        StageManager.Instance.NextStage();
    }
}