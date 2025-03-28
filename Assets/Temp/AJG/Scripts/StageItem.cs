using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageItem : MonoBehaviour
{
    public StageData data;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer.sprite = data.stageSprite;
    }
}
