using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SItem
{
    public List<ItemSO> ItemLists;
    public List<SkillSO> SkillLists;
}
[CreateAssetMenu (fileName = "ShopItemList", menuName = "ItemList/ new ShopItemList")]
public class ShopItem : ScriptableObject
{
    public List<SItem> ShopList;

    public object[] GetList(int index)
    {
        List<object> combinedList = new List<object>();

        if (index < 0 || index >= ShopList.Count)
        {
            return new object[0]; 
        }
        combinedList.AddRange(ShopList[index].ItemLists);
        combinedList.AddRange(ShopList[index].SkillLists);

        return combinedList.ToArray();
    }
}
