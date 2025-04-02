using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GoldUI goldArea;

    private ItemSO item;

    private SkillSO skill;

    public ItemSO Item { get { return item; } }

    public SkillSO Skill { get { return skill; } }

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

    public void SetSkill(SkillSO cardSkill)
    {
        skill = cardSkill;
        if (cardSkill.sprite != null)
        {
            itemIcon.sprite = cardSkill.sprite;
            itemIcon.color = Color.white;
        }
        itemName.text = cardSkill.name;
    }

    public void OnClickCardItem()
    {
        if (item is ItemSO itemData)
        {
            ItemInventoryManager.Instance.AddItem(item);
        }
        else if (skill is SkillSO skillData)
        {
            GameObject skillObject = Instantiate(skillData.skillPrefab);
            ASkill skillToAdd = skillObject.GetComponent<ASkill>();

            if (skillToAdd != null)
            {
                ItemInventoryManager.Instance.AddSkill(skillToAdd);
            }
        }
        StageManager.Instance.NextStage();
    }

    public void ShowGold(int gold)
    {
        goldArea.gameObject.SetActive(true);
        goldArea.SetGoldText(gold);
    }
}
