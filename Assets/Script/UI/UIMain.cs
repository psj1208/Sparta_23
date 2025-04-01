using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMain : UIBase
{
    [SerializeField]
    private GameObject slotPrefab;

    [SerializeField]
    public Transform itemSlotList;

    [SerializeField]
    private Transform skillSlotList;

    public override void Opened(params object[] param)
    {

    }

    public override void HideDirect()
    {
        UIManager.Hide<UIMain>();
    }

    public void AddSlot(ScriptableObject obj, int count)
    {
        GameObject go = null;

        if (obj.GetType() == typeof(ItemSO))
        {
            go = Instantiate(slotPrefab, itemSlotList);
            ItemSlot slot = go.GetComponent<ItemSlot>();
            slot.SetIcon(obj);
            slot.SetItemCount(count);
        }
    }

    public void ClearSlots()
    {
        foreach (Transform child in itemSlotList)
        {
            Destroy(child.gameObject);
        }
    }
}
