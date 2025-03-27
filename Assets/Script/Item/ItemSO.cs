using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Attack, Defense, Heal }

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object / New Item")]

public class ItemSO : ScriptableObject, IItem
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int value;
    [SerializeField] private GameObject itemPrefab;

    public string ItemName => itemName;
    public void UseItem()
    {
        switch (itemType)
        {
            case ItemType.Attack:
                //TODO: 플레이어 공격
                break;

            case ItemType.Defense:
                //TODO: 플레이어 방어
                break;

            case ItemType.Heal:
                //TODO: 플레이어 회복
                break;
        }
    }

}
