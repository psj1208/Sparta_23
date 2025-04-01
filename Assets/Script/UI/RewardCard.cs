using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardCard : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    private ItemSO item;

    public ItemSO Item { get { return item; } }

    public void SetItem(ItemSO cardItem)
    {
        item = cardItem;
        if (cardItem.sprite != null)
        {
            itemIcon.sprite = cardItem.sprite;
            itemIcon.color = Color.white;
        }
        itemName.text = cardItem.name;
        itemDescription.text = cardItem.Description;
    }

    public void OnClickCardItem()
    {
        ItemInventoryManager.Instance.AddItem(item);
    }
}
