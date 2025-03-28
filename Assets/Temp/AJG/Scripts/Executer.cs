using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executer : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public LayerMask stageItemLayer;
    
    // 딕셔너리로 포인트별로 값을 저장해두고 StageManager에서 사용한다.
    private Dictionary<E_StageType, float> stagePoints = new Dictionary<E_StageType, float>();
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     sceneLoader.LoadStage(other.gameObject.GetComponent<StageItem>().data);
    //     Destroy(other.gameObject);
    // }
    //
    private void Start()
    {
        // 참조 복사
        StageManager.Instance.stagePoints = stagePoints;
    }

    public Dictionary<E_StageType, float> GetStagePoints()
    {
        return stagePoints;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & stageItemLayer) != 0)
        {
            StageItem stageItem = other.gameObject.GetComponent<StageItem>();
            if (stageItem != null)
            {
                // 스테이지 타입과 포인트 저장.
                if (stagePoints.ContainsKey(stageItem.data.stageType))
                {
                    stagePoints[stageItem.data.stageType] += stageItem.point;
                }
                else
                {
                    stagePoints.Add(stageItem.data.stageType, stageItem.point);
                }

                Destroy(other.gameObject);
            }
        }
    }
    
}
