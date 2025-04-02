using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIReward : UIBase
{
    [SerializeField]
    private Transform cardList;

    [SerializeField]
    private GameObject card;

    public override void HideDirect()
    {

    }

    public override void Opened(params object[] param)
    {
        foreach (Transform child in cardList)
        {
            Destroy(child.gameObject);
        }

        if (param[0] != null && param[0] is List<ScriptableObject> objectList)
        {
            foreach (ScriptableObject obj in objectList)
            {
                Card rewardCard = Instantiate(card, cardList).GetComponentInChildren<Card>();

                if (obj is ItemSO item)
                {
                    rewardCard.SetItem(item); 
                }
                else if (obj is SkillSO skill)
                {
                    rewardCard.SetSkill(skill); 
                }
            }
        }
    }

    public void OnclickSkipButton()
    {
        StageManager.Instance.NextStage();
    }
}
