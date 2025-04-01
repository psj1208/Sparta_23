using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    private void Start()
    {
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
                Transform spawnPoint = spawnPoints[spawnIndex];
                GameObject newItem = Instantiate(itemData.itemPrefab, spawnPoint.position, Quaternion.identity);

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
