using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_StageType
{
    Battle,
    Shop,
    
}


[CreateAssetMenu(fileName = "NewStageData", menuName = "Stage/Stage Data")]
public class StageData : ScriptableObject
{
    public E_StageType stageType;
    public Sprite stageSprite;
    public string stageName;
    public string stageDescription;
}