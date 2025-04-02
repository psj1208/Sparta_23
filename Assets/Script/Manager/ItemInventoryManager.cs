using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ItemInventoryManager : Singleton<ItemInventoryManager>
{
    public Dictionary<ItemSO, int> itemDeck = new Dictionary<ItemSO, int>(); 
    public List<ASkill> skills = new List<ASkill>();
    public ItemSpawner itemSpawner;
    [SerializeField] private List<ItemSO> startingItems;
    [SerializeField] private List<ASkill> startingSkills;
    public UIMain uiMain;
    public event Action OnInventoryInitialized;

    private void Start()
    {
        foreach (var item in startingItems)
        {
            AddItemMultipleTimes(item, 3);
        }
        foreach (var skill in startingSkills)
        {
            AddSkill(skill);
        }
    }
    
    public void InitializeInventory()
    {
        uiMain = UIManager.Get<UIMain>();
        UpdateInventoryUI();
        UpdateSkillUI();
        OnInventoryInitialized?.Invoke();
    }

    public Dictionary<ItemSO, int> GetInventoryItems()
    {
        return new Dictionary<ItemSO, int>(itemDeck);
    }

    private void AddItemMultipleTimes(ItemSO item, int multiplier)
    {
        if (item == null) return;

        for (int i = 0; i < multiplier; i++)
        {
            AddItem(item); 
        }
    }

    public void AddItem(ItemSO item)
    {
        if (item == null) return;

        if (item.stackable)
        {
            if (itemDeck.ContainsKey(item))
                itemDeck[item]++;
            else
                itemDeck[item] = 1;
        }
        else
        {
            itemDeck[item] = 1;
        }
        UpdateInventoryUI();
    }

    public void AddSkill(ASkill skill)
    {
        if (skill == null || skills.Contains(skill)) return;
        skills.Add(skill);
        UpdateSkillUI();
    }

    public void RemoveItem(ItemSO item, int amount = 1)
    {
        if (item == null || !itemDeck.ContainsKey(item)) return;

        itemDeck[item] -= amount;

        if (itemDeck[item] <= 0)
            itemDeck.Remove(item);

        UpdateInventoryUI();
    }

    public void RemoveSkill(ASkill skill)
    {
        if (skill == null || !skills.Contains(skill)) return;
        skills.Remove(skill);
        UpdateSkillUI();
    }

    private void UpdateInventoryUI()
    {
        if (uiMain == null) return;
        uiMain.ClearItemSlots();

        foreach (var item in itemDeck)
        {
            uiMain.AddSlot(item.Key, item.Value);
        }
    }

    private void UpdateSkillUI()
    {
        if (uiMain == null) return;
        uiMain.ClearSKillSlot();
        foreach (var skill in skills)
        {
            uiMain.AddSkillSlot(skill.skillData);
        }
    }
}
