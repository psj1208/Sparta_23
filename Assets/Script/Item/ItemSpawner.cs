using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Vector2> spawnPoints;

    private void Start()
    {
        spawnPoints = new List<Vector2>();
        spawnPoints.Add(new Vector3(-2.8f, 2.5f));
        ItemInventoryManager.Instance.OnInventoryInitialized += SpawnInventoryItems;
    }

    public void SpawnInventoryItems()
    {
        Dictionary<ItemSO, int> inventoryItems = ItemInventoryManager.Instance.GetInventoryItems();
        int spawnIndex = 0;

        foreach (var itemEntry in inventoryItems)
        {
            ItemSO itemData = itemEntry.Key;
            int itemCount = itemEntry.Value;

            for (int i = 0; i < itemCount; i++)
            {
                Vector2 spawnPoint = spawnPoints[spawnIndex];
                GameObject newItem = Instantiate(itemData.itemPrefab, spawnPoint, Quaternion.identity);

                if (newItem == null)
                {
                    Debug.LogError($"ItemSpawner: {itemData.ItemName} 아이템 생성 실패");
                    continue;
                }

                ItemObject itemObject = newItem.GetComponent<ItemObject>();
                if (itemObject == null)
                {
                    Debug.LogError($"ItemSpawner: {itemData.ItemName} 프리팹에 컴포넌트 없음");
                    continue;
                }

                itemObject.SetItemData(itemData);
                spawnIndex = (spawnIndex + 1) % spawnPoints.Count;
            }
        }
    }
}
