using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadStage(StageData stageData)
    {
        switch (stageData.stageType)
        {
            case E_StageType.Battle:
                SceneManager.LoadScene("MainScene");
                break;
            case E_StageType.Shop:
                SceneManager.LoadScene("ShopScene");
                break;
        }
    }

    public void LoadStage(E_StageType stageType)
    {
        DOTween.KillAll();
        switch (stageType)
        {
            case E_StageType.Battle:
                SceneManager.LoadScene("MainScene");
                break;
            case E_StageType.Shop:
                SceneManager.LoadScene("ShopScene");
                break;
        }
    }
    
    
    public void ReturnToStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
