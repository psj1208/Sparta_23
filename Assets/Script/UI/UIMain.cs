using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMain : UIBase
{
    

    public override void Opened(params object[] param)
    {
        
    }

    public override void HideDirect()
    {
        UIManager.Hide<UIMain>();
    }

    public void OnClickPopupAlert()
    {
        UnityAction action = () =>
        {
            UIManager.Hide<PausePopup>();
        };
        UIManager.Show<PausePopup>(action);
    }

}
