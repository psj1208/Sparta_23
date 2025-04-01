using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HealPotion : MonoBehaviour, IItem
{
    public string ItemName { get; }
    private float baseDamage;

    public void UseItem(Player player)
    {
        foreach(Enemy enemy in player.PlayerStateMachine.curEnemies)
        {
            if(enemy != null)
            {
                player.PlayerStateMachine.StartAnimation(player.PlayerStateMachine.AttackAnimHash);
                enemy?.ResourceController.ChangeHealth(player.PlayerStateMachine.Player.GetAttackDamage() + baseDamage);
            }
        }
    }
}
