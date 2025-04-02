using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIStageShow : UIBase
{
    [SerializeField]
    private Transform cardListPosition;

    [SerializeField]
    private GameObject stageCardPrefab;

    public override void HideDirect()
    {

    }

    public override void Opened(params object[] param)
    {
        foreach (Transform child in cardListPosition)
        {
            Destroy(child.gameObject);
        }

        foreach (var obj in param)
        {
            
            if (obj != null && obj is List<E_StageType> stageType)
            {
                foreach (var stage in stageType)
                {
                    CardStage stageCard = Instantiate(stageCardPrefab, cardListPosition)
                        .GetComponentInChildren<CardStage>();
                    stageCard.SetData(stage);
                }
            }
        }

    }
    public void OnclickSkipButton()
    {
        StageManager.Instance.NextStage();
    }
}
