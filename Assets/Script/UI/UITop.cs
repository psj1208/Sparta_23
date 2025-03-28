using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITop : UIBase
{


    public override void HideDirect()
    {
        
    }

    public override void Opened(params object[] param)
    {
        
    }

    public void OnPauseClick()
    {
        UIManager.Show<PausePopup>();
    }
}
