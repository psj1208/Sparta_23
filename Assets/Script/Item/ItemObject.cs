using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private ItemSO itemData;

    public void SetItemData(ItemSO data)
    {
        itemData = data;
        Debug.Log($"ItemObject: {itemData.ItemName} 설정");
    }
}
