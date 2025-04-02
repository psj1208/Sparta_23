using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardStage : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    [SerializeField]
    private GameObject prefab;

    private StageData stageData;

    public StageData StageData { get { return stageData; } }

    public List<StageData> stageDataList = new List<StageData>();
    public void SetData(E_StageType stageType)
    {
        if (E_StageType.Battle == stageType)
        {
            stageData = stageDataList[0];
        }
        else if (E_StageType.Shop == stageType)
        {
            stageData = stageDataList[1];
        }
        if (stageData.stageSprite != null)
        {
            itemIcon.sprite = stageData.stageSprite;
            itemIcon.color = Color.white;
        }
        itemName.text = stageData.name;
        // itemDescription.text = stageData.description;
    }
}
