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

        if (param[0] != null && param[0] is List<ItemSO> itemList)
        {
            foreach (ItemSO item in itemList)
            {
                Card rewardCard = Instantiate(card, cardList).GetComponentInChildren<Card>();
                rewardCard.SetItem(item);
            }
        }
    }

    public void OnclickSkipButton()
    {
        StageManager.Instance.NextStage();
    }
}
