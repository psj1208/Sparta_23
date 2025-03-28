using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("UI Setting")]
    public Image image;
    public TextMeshProUGUI countText;

    [HideInInspector] public ItemSO item;
    [HideInInspector] public int count = 1;

    public void InitialiseItem(ItemSO newItem)
    {
        item = newItem;
        image.sprite = newItem.sprite;
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
}
