using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSample : CanvasBase<CanvasSample>
{
    private IEnumerator Start()
    {
        var oper = SceneManager.LoadSceneAsync("DontDestroy", LoadSceneMode.Additive);
        yield return oper;
        if (parents.Count > 0)
            UIManager.SetParents(parents);
        UIManager.Show<UIMain>();
        UIManager.Show<UITop>();
    }
}
