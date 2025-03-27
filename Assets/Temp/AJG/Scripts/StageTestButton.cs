using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTestButton : MonoBehaviour
{
    public Button testButton;

    private void Start()
    {
        testButton.onClick.AddListener(() => StageManager.Instance.ClearStage());
    }
}
