using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITop : UIBase
{
    [SerializeField]
    private Floor floor;

    [SerializeField]
    private GoldUI gold;

    public override void HideDirect()
    {
        
    }

    public override void Opened(params object[] param)
    {
        floor.SetCurFloor(StageManager.Instance.GetCurrentStageIndex());
        floor.SetTotalFloor(StageManager.Instance.selectedStages.Count);
        //gold.SetGoldText();
    }

    public void OnPauseClick()
    {
        UIManager.Show<UIPopup>();
    }
}
