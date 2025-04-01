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

    public void SetIcon(ScriptableObject obj)
    {
        if (obj is ItemSO item)
        {
            icon.sprite = item.sprite;
            icon.color = Color.white;
        }
        else if (obj is SkillSO skill)
        {
            icon.sprite = skill.sprite;
            icon.color = Color.white;
        }
        else
        {
            icon.sprite = null;
            icon.color = Color.clear;
            return;
        }

        
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
