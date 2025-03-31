using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class RoundTextUI : UIBase
{
    public RectTransform roundText;
    public TextMeshProUGUI roundTextValue;
    public Vector2 targetPosition = new Vector2(100, -50);
    public float Amplitude = 2f;
    public float duration = 1.5f;
    public float targetScale = 0.5f;
    
    public override void HideDirect()
    {
        
    }

    public override void Opened(params object[] param)
    {
        
    }
    
    private void Start()
    {
        
        AnimateRoundText();
    }

    private void AnimateRoundText()
    {
        roundText.localScale = Vector3.one * Amplitude;
        roundText.anchoredPosition = Vector2.zero;
        
        roundText.DOAnchorPos(targetPosition, duration)
            .SetEase(Ease.InOutQuad)
            .SetDelay(1f);

        roundText.DOScale(targetScale, duration)
            .SetEase(Ease.InOutQuad)
            .SetDelay(1f);
        
        roundTextValue.text = StageManager.Instance.currentRound.ToString();
    }
}