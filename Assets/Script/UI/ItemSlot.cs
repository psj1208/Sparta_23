using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI itemCount;

    public void SetIcon(Skill skill)
    {
        //icon.sprite       스프라이트 값 설정 필요
        icon.color = Color.white;           //아이템 이미지 부분의 알파값을 0으로 설정하여 알파값을 255로 되돌려줌
    }

    public void SetItemCount(int count)
    {
        if (count == 0)
        {
            itemCount.text = string.Empty;
        }
        else
        {
            itemCount.text = count.ToString();
        }
    }
}
