using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PowerUP : ASkill
{
    public override void UseSkill(Player player)
    {
        player.StatHandler.ModifyStat(EStatType.Attack, 1, true, 0);
    }
}
