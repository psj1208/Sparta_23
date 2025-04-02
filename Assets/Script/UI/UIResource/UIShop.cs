using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UIBase
{
    [SerializeField]
    private Transform cardList;

    [SerializeField]
    private GameObject card;

    [SerializeField]
    private Button exitButton;

    public override void HideDirect()
    {
        exitButton.onClick.RemoveAllListeners();
    }

    public override void Opened(params object[] param)
    {
        exitButton.onClick.AddListener(OnclickSkipButton);

        foreach (Transform child in cardList)
        {
            Destroy(child.gameObject);
        }

        foreach (object obj in param)
        {
            if (obj is ItemSO item)
            {
                CreateItemCard(item);
            }
            else if (obj is SkillSO skill)
            {
                CreateSkillCard(skill);
            }
        }
    }

    private void CreateItemCard(ItemSO item)
    {
        Card rewardCard = Instantiate(card, cardList).GetComponentInChildren<Card>();
        rewardCard.SetItem(item);
        rewardCard.ShowGold(item.BuyPrice);

        Button cardButton = rewardCard.GetComponentInChildren<Button>();
        if (cardButton != null)
        {
            cardButton.onClick.AddListener(() =>
            {
                Destroy(rewardCard.gameObject);
            });
        }
    }

    private void CreateSkillCard(SkillSO skill)
    {
        Card rewardCard = Instantiate(card, cardList).GetComponentInChildren<Card>();
        rewardCard.SetSkill(skill);
        rewardCard.ShowGold(skill.BuyPrice);  

        Button cardButton = rewardCard.GetComponentInChildren<Button>();
        if (cardButton != null)
        {
            cardButton.onClick.AddListener(() =>
            {
                GameObject skillObject = Instantiate(skill.skillPrefab); 
                ASkill skillToAdd = skillObject.GetComponent<ASkill>();
                Destroy(rewardCard.gameObject);
            });
        }
    }

    public void OnclickSkipButton()
    {
        StageManager.Instance.NextStage();
    }
}
