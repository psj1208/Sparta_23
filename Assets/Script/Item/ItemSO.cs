using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object / New Item")]

public class ItemSO : ScriptableObject, IItem
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private EItemType itemType;
    [SerializeField] private int value;
    [SerializeField] private GameObject itemPrefab;

    public string ItemName => itemName;
    public void UseItem()
    {
        switch (itemType)
        {
            case EItemType.Attack:
                //TODO: 플레이어 공격
                break;

            case EItemType.Defense:
                //TODO: 플레이어 방어
                break;

            case EItemType.Heal:
                //TODO: 플레이어 회복
                break;
        }
    }

}
