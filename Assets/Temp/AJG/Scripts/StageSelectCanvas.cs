using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectCanvas : CanvasBase<StageSelectCanvas>
{
    private IEnumerator Start()
    {
        var oper = SceneManager.LoadSceneAsync("DontDestroy", LoadSceneMode.Additive);
        yield return oper;
        Instance.isDontDestroy = false;
        if (parents.Count > 0)
            UIManager.SetParents(parents);
        UIManager.Show<RoundTextUI>();
    }
}
