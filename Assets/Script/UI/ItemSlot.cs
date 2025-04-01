using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    Item,
    Skill
}

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text itemCount;

    [SerializeField]
    private SlotType slotType;

    [SerializeField]
    private ItemSO itemSO;

    public void SetIcon(ItemSO item)
    {
        icon.sprite = item.sprite;

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
