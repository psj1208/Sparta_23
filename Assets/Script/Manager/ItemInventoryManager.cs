using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ItemInventoryManager : Singleton<ItemInventoryManager>
{
    public Dictionary<ItemSO, int> itemDeck = new Dictionary<ItemSO, int>(); 
    public ItemSpawner itemSpawner;
    [SerializeField] private List<ItemSO> startingItems;
    public UIMain uiMain;
    public event Action OnInventoryInitialized;

    private void Start()
    {
        foreach (var item in startingItems)
        {
            AddItemMultipleTimes(item, 3);
        }
    }
    
    public void InitializeInventory()
    {
        uiMain = UIManager.Get<UIMain>();
        UpdateInventoryUI();
        OnInventoryInitialized?.Invoke();
    }

    public Dictionary<ItemSO, int> GetInventoryItems()
    {
        return new Dictionary<ItemSO, int>(itemDeck);
    }

    private void AddItemMultipleTimes(ItemSO item, int multiplier)
    {
        if (item == null) return;

        for (int i = 0; i < multiplier; i++)
        {
            AddItem(item); 
        }
    }

    public void AddItem(ItemSO item)
    {
        if (item == null) return;

        if (item.stackable)
        {
            if (itemDeck.ContainsKey(item))
                itemDeck[item]++;
            else
                itemDeck[item] = 1;
        }
        else
        {
            itemDeck[item] = 1;
        }

        UpdateInventoryUI();
    }

    public void RemoveItem(ItemSO item, int amount = 1)
    {
        if (item == null || !itemDeck.ContainsKey(item)) return;

        itemDeck[item] -= amount;

        if (itemDeck[item] <= 0)
            itemDeck.Remove(item);

        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        if (uiMain == null) return;
        uiMain.ClearSlots();

        foreach (var item in itemDeck)
        {
            uiMain.AddSlot(item.Key, item.Value);
        }
    }

    public int GetItemCount(ItemSO item)
    {
        return itemDeck.ContainsKey(item) ? itemDeck[item] : 0;
    }

    public List<ItemSO> GetAllItems()
    {
        return new List<ItemSO>(itemDeck.Keys);
    }
}
