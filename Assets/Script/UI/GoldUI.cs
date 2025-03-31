using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI goldText;

    public void SetGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }
}
