using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ItemInventoryManager : Singleton<ItemInventoryManager>
{
    public List<ItemSO> itemDeck = new List<ItemSO>();  // The deck of items the player holds
    [SerializeField] private List<ItemSO> startingItems; // The default items the player starts with

    private void Start()
    {
        StartCoroutine(InitializeInventory());
    }

    private IEnumerator InitializeInventory()
    {
        // Wait until UIMain is available (instantiated)
        while (UIManager.Get<UIMain>() == null)
        {
            yield return null; // Wait until UIMain is instantiated
        }

        // UIMain is now ready, initialize the inventory
        UIMain uiMain = UIManager.Get<UIMain>();

        // Add default starting items to the inventory
        foreach (var item in startingItems)
        {
            AddItem(item);
        }

        // Update the UI to reflect the items in the inventory
        UpdateInventoryUI();
    }

    public void AddItem(ItemSO item)
    {
        // Check if the item is already in the deck (if stackable, increase count)
        if (item.stackable && itemDeck.Contains(item))
        {
            // Increase the count if the item is stackable
            GetItemCount(item);
        }
        else
        {
            // Add the item to the deck if not already there
            itemDeck.Add(item);
        }

        // Update the UI
        UIMain uiMain = UIManager.Get<UIMain>();
        if (uiMain != null)
        {
            uiMain.AddSlot(item, GetItemCount(item));  // Add slot for this item with its current count
        }
    }


    private int GetItemCount(ItemSO item)
    {
        int count = 0;

        // Loop through the deck and count how many of the same item there are
        foreach (var deckItem in itemDeck)
        {
            if (deckItem == item)
            {
                count++;
            }
        }

        return count;
    }

    private void UpdateInventoryUI()
    {
        UIMain uiMain = UIManager.Get<UIMain>();
        if (uiMain != null)
        {
            // Clear all existing slots in the UI first
            foreach (Transform child in uiMain.itemSlotList)
            {
                Destroy(child.gameObject);
            }

            // Now add all items from the deck to the UI
            foreach (var item in itemDeck)
            {
                uiMain.AddSlot(item, GetItemCount(item));
            }
        }
    }

    public int GetItemCount()
    {
        return itemDeck.Count;
    }

    public ItemSO GetItemAtIndex(int index)
    {
        if (index >= 0 && index < itemDeck.Count)
        {
            return itemDeck[index];
        }

        Debug.LogError("Item index out of range");
        return null;
    }

}
