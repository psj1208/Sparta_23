using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    Image curHealthBar;
    [SerializeField]
    TextMeshProUGUI curHealthText;
    [SerializeField]
    TextMeshProUGUI maxHealthText;
    [SerializeField]
    TextMeshProUGUI additionalDefText;
    [SerializeField]
    TextMeshProUGUI additionalAtkText;

    public void UpdateHealth(float curHealth, float maxHealth)
    {

        curHealthText.text = Mathf.Min((int)curHealth, maxHealth).ToString();
        maxHealthText.text = maxHealth.ToString();
        if (maxHealth > 0f)
        {
            curHealthBar.fillAmount = Mathf.Clamp01(curHealth / maxHealth);
        }
        else
        {
            curHealthBar.fillAmount = 0f;
        }
    }

    public void UpdateShield(int shieldAmount)
    {
        if (shieldAmount == 0)
        {
            additionalDefText.text = string.Empty;
        }
        else
        {
            additionalDefText.text = shieldAmount.ToString();
        }
    }

    public void UpdateAdditionalAtk(int atkAmount)
    {
        if (atkAmount == 0)
        {
            additionalDefText.text = string.Empty;
        }
        else
        {
            additionalAtkText.text = atkAmount.ToString();
        }
    }
}
