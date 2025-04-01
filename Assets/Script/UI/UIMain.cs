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

    public void AddSlot(ItemSO item, int count)
    {
        GameObject go = null;

        if (item.GetType() == typeof(ItemSO))
        {
            go = Instantiate(slotPrefab, itemSlotList); // Instantiate a new slot

            if (go != null)
            {
                ItemSlot slot = go.GetComponent<ItemSlot>();

                // Debugging the item and count
                Debug.Log("Adding item: " + item.name + " with count: " + count);

                slot.SetIcon(item); // Set the item sprite
                slot.SetItemCount(count); // Set the item count

            }
            else
            {
                Debug.LogError("Failed to instantiate slotPrefab!");
            }
        }
        else
        {
            Debug.LogError("The object is not of type ItemSO!");
        }

    }
}
