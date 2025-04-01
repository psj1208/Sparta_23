using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventoryScript : MonoBehaviour
{
    [SerializeField] private ItemSO[] testItems;  // Array of ItemSO to test
    [SerializeField] private int initialCount = 1; // Initial count for items

    private void Start()
    {
        TestItemSlots();
    }

    private void TestItemSlots()
    {
        // Log a message to indicate the test has started
        Debug.Log("Testing item and item slot creation...");

        // Loop through each test item and add it to the UI
        foreach (ItemSO item in testItems)
        {
            Debug.Log($"Testing Item: {item.name}");

            // Call AddSlot in UIMain to create the item slot
            UIMain uiMain = UIManager.Get<UIMain>();
            if (uiMain != null)
            {
                uiMain.AddSlot(item, initialCount);
            }
            if (uiMain == null)
            {
                Debug.LogError("UIMain not found! Check if it's properly instantiated in the scene.");
            }
        }

        // Check if ItemSlots have been created
        ItemSlot[] createdSlots = FindObjectsOfType<ItemSlot>();
        if (createdSlots.Length > 0)
        {
            foreach (ItemSlot slot in createdSlots)
            {
                Debug.Log($"ItemSlot Created: {slot.gameObject.name}");
            }
        }
        else
        {
            Debug.LogWarning("No ItemSlots were created!");
        }
    }
}
