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

        if (!(param[0] is ItemSO))
        {
            Debug.Log("ItemSO 형식이 아님");
            return;
        }

        foreach (ItemSO item in param)
        {
            Debug.Log("생성 중");
            Card rewardCard = Instantiate(card, cardList).GetComponentInChildren<Card>();
            rewardCard.SetItem(item);
            rewardCard.ShowGold(item.BuyPrice);
            //rewardCard의 버튼 컴포넌트를 가져와서 클릭 시에 해당 카드가 파괴되도록
            Button cardButton = rewardCard.GetComponentInChildren<Button>();
            if (cardButton != null)
            {
                cardButton.onClick.AddListener(() =>
                {
                    ItemInventoryManager.Instance.AddItem(rewardCard.Item);
                    Destroy(rewardCard.gameObject);
                });
            }
            else
            {
                Debug.LogWarning("No Button");
            }
        }
    }

    public void OnclickSkipButton()
    {
        StageManager.Instance.NextStage();
    }
}
