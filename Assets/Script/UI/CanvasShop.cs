using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasShop : CanvasBase<CanvasShop>
{
    [SerializeField] ShopItem shop;
    private IEnumerator Start()
    {
        var oper = SceneManager.LoadSceneAsync("DontDestroy", LoadSceneMode.Additive);
        yield return oper;
        if (parents.Count > 0)
            UIManager.SetParents(parents);
        UIManager.Show<UIShop>(shop.GetList(0));
        ItemInventoryManager.Instance.InitializeInventory();
    }
}
