using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    public Button[] buttons;
    public UnityAction[] okActions;

    public override void Opened(params object[] param)
    {
        okActions = new UnityAction[param.Length];
        for (int i = 0; i < okActions.Length; i++)
        {
            okActions[i] = (UnityAction)param[i];
        }
    }

    public override void HideDirect()
    {
        UIManager.Hide<UIPopup>();
    }
}
