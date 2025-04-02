using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Axe : MonoBehaviour, IItem
{
    public string ItemName { get; }
    public float baseDamage;

    public void UseItem(Player player)
    {
        Debug.Log(baseDamage);
        foreach(Enemy enemy in player.PlayerStateMachine.curEnemies)
        {
            if(enemy != null)
            {
                AudioManager.Instance.PlaySFX(ESFXType.Attack);
                player.PlayerStateMachine.StartAnimation(player.PlayerStateMachine.AttackAnimHash);
                enemy?.ResourceController.ChangeHealth(-(player.PlayerStateMachine.Player.StatHandler.GetTotalAttack() + baseDamage));
            }
        }
    }
}
