using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCanvas : CanvasBase<StartCanvas>
{
    private IEnumerator Start()
    {
        var oper = SceneManager.LoadSceneAsync("DontDestroy", LoadSceneMode.Additive);
        yield return oper;
        if (parents.Count > 0)
            UIManager.SetParents(parents);
        UIManager.Show<UIStart>();
        GameManager.Instance.isGameOver = false;
    }
}
