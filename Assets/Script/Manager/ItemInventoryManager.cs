using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ItemInventoryManager : Singleton<ItemInventoryManager>
{
    public int maxStackedItems = 100;
    public ItemSlot[] itemSlots;
    public GameObject inventoryItemPrefab;

    [SerializeField] private List<ItemSO> startingItems;

    private void Start()
    {
        itemSlots = FindObjectsOfType<ItemSlot>(); // 🔹 자동으로 모든 ItemSlot 찾기
        AddStartingItems();
    }

    private void AddStartingItems()
    {
        UIMain uiMain = UIManager.Get<UIMain>();
        if (uiMain == null) return;

        foreach (var item in startingItems)
        {
            AddItem(item);
            uiMain.AddSlot(item, 1);
        }

        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        UIMain uiMain = UIManager.Get<UIMain>();
        if (uiMain == null) return;

        foreach (var slot in itemSlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                uiMain.AddSlot(itemInSlot.item, itemInSlot.count);
            }
        }
    }

    public void RegisterItemSlot(ItemSlot slot)
    {
        List<ItemSlot> slotList = itemSlots.ToList();
        slotList.Add(slot);
        itemSlots = slotList.ToArray();
    }

    public bool AddItem(ItemSO item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();

                UIMain uiMain = UIManager.Get<UIMain>();
                if (uiMain != null) uiMain.AddSlot(item, itemInSlot.count);
                return true;
            }
        }
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);

                // 🔹 UI 업데이트
                UIMain uiMain = UIManager.Get<UIMain>();
                if (uiMain != null) uiMain.AddSlot(item, 1);

                return true;
            }
        }
        return false;
    }

    void SpawnNewItem (ItemSO item, ItemSlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
