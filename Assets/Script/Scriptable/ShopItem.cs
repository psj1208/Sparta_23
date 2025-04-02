using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SItem
{
    public List<ItemSO> ItemLists;
}
[CreateAssetMenu (fileName = "ShopItemList", menuName = "ItemList/ new ShopItemList")]
public class ShopItem : ScriptableObject
{
    public List<SItem> ShopList;

    public ItemSO[] GetList(int index)
    {
        return ShopList[index].ItemLists.ToArray();
    }
}
