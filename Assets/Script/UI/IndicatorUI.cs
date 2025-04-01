using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI damageText;

    public void SetDamageText(int damage)
    {
        damageText.text = damage.ToString();
    }
}
