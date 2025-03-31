using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageTestText : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = StageManager.Instance.currentRound.ToString();
    }
}
