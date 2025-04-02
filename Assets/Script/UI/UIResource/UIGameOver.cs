using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : UIBase
{
    [SerializeField]
    private FadeInOut fadeInOut;

    public override void HideDirect()
    {

    }

    public override void Opened(params object[] param)
    {
        fadeInOut.FadeIn(1f);
    }
}
